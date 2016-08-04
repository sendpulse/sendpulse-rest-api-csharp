using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web;

namespace Sendpulse_rest_api.restapi
{
    class Sendpulse :SendpulseInterface
    {
        private string apiurl = "https://api.sendpulse.com";
        private string userId = null;
        private string secret = null;
        private string tokenName = null;
        private int refreshToken = 0;

        public Sendpulse(string _userId, string _secret)
        {
            if (_userId == null || _secret == null)
            {
                Console.WriteLine("Empty ID or SECRET");
            }
            this.userId = _userId;
            this.secret = _secret;
            this.tokenName = md5(this.userId + "::" + this.secret);
            if (this.tokenName != null) {
                if (!this.getToken()) {
                    Console.WriteLine("Could not connect to api, check your ID and SECRET");
                }
            }
        }
        public string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string md5(string input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
        /// <summary>
        /// Form and send request to API service
        /// </summary>
        /// <param name="path">string path</param>
        /// <param name="method">string method</param>
        /// <param name="data"><string, object> data</param>
        /// <param name="useToken">Boolean useToken</param>
        /// <returns>Dictionary<string, object> result data</returns>
        public Dictionary<string, object> sendRequest(string path, string method, Dictionary<string, object> data , bool useToken = true)
        {
            string strReturn = null;
            Dictionary<string, object> response = new Dictionary<string, object>();
            try {
                string stringdata = "";
                if(data!=null && data.Count>0)
                    stringdata = this.makeRequestString(data);
                method = method.ToUpper();
                if (method == "GET" && stringdata.Length>0) {
                    path = path + "?" + stringdata;
                }
                HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(this.apiurl+"/"+path);
                WebReq.Method = method;
                if(useToken && this.tokenName!=null)
                    WebReq.Headers.Add("Authorization", "Bearer " + this.tokenName);
                if (method != "GET") {
                    byte[] buffer = Encoding.ASCII.GetBytes(stringdata);
                    WebReq.ContentType = "application/x-www-form-urlencoded";
                    WebReq.ContentLength = buffer.Length;
                    Stream PostData = WebReq.GetRequestStream();
                    PostData.Write(buffer, 0, buffer.Length);
                    PostData.Close();
                }
                try
                {
                    HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();
                    HttpStatusCode status = WebResp.StatusCode;
                    response.Add("http_code", (int)status);
                    if ((int)status == 401 && this.refreshToken == 0)
                    {
                        this.refreshToken += 1;
                        this.getToken();
                        response = this.sendRequest(path, method, data, false);
                    }
                    else
                    {
                        Stream WebResponse = WebResp.GetResponseStream();
                        StreamReader _response = new StreamReader(WebResponse);
                        strReturn = _response.ReadToEnd();
                        if (strReturn.Length > 0)
                        {
                            Object jo = null;
                            try
                            {
                                jo = JsonConvert.DeserializeObject<Object>(strReturn.Trim());
                                if (jo.GetType() == typeof(JObject))
                                    jo = (JObject)jo;
                                else if (jo.GetType() == typeof(JArray))
                                    jo = (JArray)jo;
                            }
                            catch (JsonException jex)
                            {
                                Console.WriteLine(jex.Message);
                            }
                            response.Add("data", jo);
                        }
                    }
                }
                catch (WebException we)
                {
                    HttpStatusCode wRespStatusCode = ((HttpWebResponse)we.Response).StatusCode;
                    response.Add("http_code", (int)wRespStatusCode);
                    Stream WebResponse = ((HttpWebResponse)we.Response).GetResponseStream();
                    StreamReader _response = new StreamReader(WebResponse);
                    strReturn = _response.ReadToEnd();
                    response.Add("data", strReturn);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
              
            }
            return response;
        }
        /// <summary>
        /// Make post data string
        /// </summary>
        /// <param name="data">Dictionary<string, object> params</param>
        /// <returns>string urlstring</returns>
        private string makeRequestString(Dictionary<string, object> data)
        {
            string requeststring = "";
            foreach (var item in data)
            {
                if (requeststring.Length != 0) requeststring = requeststring+"&";
                requeststring= requeststring+ HttpUtility.UrlEncode(item.Key, Encoding.UTF8) +"="+ HttpUtility.UrlEncode(item.Value.ToString(), Encoding.UTF8);
                
            }
            return requeststring;
        }
        /// <summary>
        /// Get token and store it
        /// </summary>
        /// <returns>bool</returns>
        private bool getToken()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("grant_type", "client_credentials");
            data.Add("client_id", this.userId);
            data.Add("client_secret", this.secret);
            Dictionary<string, object> requestResult = null;
            try
            {
                requestResult = this.sendRequest("oauth/access_token", "POST", data, false);
            }
            catch (IOException) { }
            if (requestResult == null) return false;
            if ((int)requestResult["http_code"] != 200)
                return false;
            this.refreshToken = 0;
            JObject jdata = (JObject)requestResult["data"];
            if (jdata.GetType() == typeof(JObject)){
                this.tokenName = jdata["access_token"].ToString();
            }
            return true;
        }
        /// <summary>
        /// Process results
        /// </summary>
        /// <param name="data">Dictionary<string, object> data</param>
        /// <returns>Dictionary<string, object> data</returns>
        private Dictionary<string, object> handleResult(Dictionary<string, object> data) {
            if (!data.ContainsKey("data") || data.Count==0)
            {
                data.Add("data", null);
            }
            if ((int)data["http_code"] != 200)
            {
                data.Add("is_error", true);
            }
            return data;
        }
        /// <summary>
        /// Process errors
        /// </summary>
        /// <param name="customMessage">String Error message</param>
        /// <returns>Dictionary<string, object> data</returns>
        private Dictionary<string, object> handleError(string customMessage)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("is_error", true);
            if (customMessage != null && customMessage.Length > 0)
            {
                data.Add("message", customMessage);
            }
            return data;
        }
        /// <summary>
        /// Get list of address books
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public Dictionary<string, object> listAddressBooks(int limit, int offset)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            if (limit > 0) data.Add("limit", limit);
            if (offset > 0) data.Add("offset", offset);
            Dictionary<string, object> result = null;
            try
            {
                result = this.sendRequest("addressbooks", "GET", data);
            }
            catch (IOException) { }
            return this.handleResult(result);
        }
        /// <summary>
        /// Get book info
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Dictionary<string, object> getBookInfo(int id)
        {
            if (id <= 0) return this.handleError("Empty book id");
            Dictionary<string, object> result = null;
            try
            {
                result = this.sendRequest("addressbooks/" + id, "GET", null);
            }
            catch (IOException) { }
            return this.handleResult(result);
        }
        /// <summary>
        /// Get list emails from book
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Dictionary<string, object> getEmailsFromBook(int id)
        {
            if (id <= 0) return this.handleError("Empty book id");
            Dictionary<string, object> result = null;
            try
            {
                result = this.sendRequest("addressbooks/" + id + "/emails", "GET", null);
            }
            catch (IOException) { }
            return this.handleResult(result);
        }
        /// <summary>
        /// Remove address book
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Dictionary<string, object> removeAddressBook(int id)
        {
            if (id <= 0) return this.handleError("Empty book id");
            Dictionary<string, object> result = null;
            try
            {
                result = this.sendRequest("addressbooks/" + id, "DELETE", null);
            }
            catch (IOException) { }
            return this.handleResult(result);
        }
        /// <summary>
        /// Edit address book name
        /// </summary>
        /// <param name="id">String book id</param>
        /// <param name="newname">String book new name</param>
        /// <returns></returns>
        public Dictionary<string, object> editAddressBook(int id, string newname)
        {
            if (id <= 0 || newname.Length == 0) return this.handleError("Empty new name or book id");
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("name", newname);
            Dictionary<string, object> result = null;
            try
            {
                result = this.sendRequest("addressbooks/" + id, "PUT", data);
            }
            catch (IOException) { }
            return this.handleResult(result);
        }
        /// <summary>
        /// Create new address book
        /// </summary>
        /// <param name="bookName"></param>
        /// <returns></returns>
        public Dictionary<string, object> createAddressBook(string bookName)
        {
            if (bookName.Length == 0) return this.handleError("Empty book name");
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("bookName", bookName);
            Dictionary<string, object> result = null;
            try
            {
                result = this.sendRequest("addressbooks", "POST", data);
            }
            catch (IOException) { }
            return this.handleResult(result);
        }
        /// <summary>
        /// Add new emails to book
        /// </summary>
        /// <param name="bookId">int book id</param>
        /// <param name="emails">String A serialized array of emails</param>
        /// <returns></returns>
        public Dictionary<string, object> addEmails(int bookId, string emails)
        {
            if (bookId <= 0 || emails.Length == 0) return this.handleError("Empty book id or emails");
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("emails", emails);
            Dictionary<string, object> result = null;
            try
            {
                result = this.sendRequest("addressbooks/" + bookId + "/emails", "POST", data);
            }
            catch (IOException) { }
            return this.handleResult(result);
        }
        /// <summary>
        /// Remove emails from book
        /// </summary>
        /// <param name="bookId">int book id</param>
        /// <param name="emails">String A serialized array of emails</param>
        /// <returns></returns>
        public Dictionary<string, object> removeEmails(int bookId, string emails)
        {
            if (bookId <= 0 || emails.Length == 0) return this.handleError("Empty book id or emails");
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("emails", emails);
            Dictionary<string, object> result = null;
            try
            {
                result = this.sendRequest("addressbooks/" + bookId + "/emails", "DELETE", data);
            }
            catch (IOException) { }
            return this.handleResult(result);
        }
        /// <summary>
        /// Get information about email from book
        /// </summary>
        /// <param name="bookId"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public Dictionary<string, object> getEmailInfo(int bookId, string email)
        {
            if (bookId <= 0 || email.Length == 0) return this.handleError("Empty book id or email");
            Dictionary<string, object> result = null;
            try
            {
                result = this.sendRequest("addressbooks/" + bookId + "/emails/" + email, "GET", null);
            }
            catch (IOException) { }
            return this.handleResult(result);
        }
        /// <summary>
        /// Calculate cost of the campaign based on address book
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        public Dictionary<string, object> campaignCost(int bookId)
        {
            if (bookId <= 0) return this.handleError("Empty book id");
            Dictionary<string, object> result = null;
            try
            {
                result = this.sendRequest("addressbooks/" + bookId + "/cost", "GET", null);
            }
            catch (IOException) { }
            return this.handleResult(result);
        }
        /// <summary>
        /// Get list of campaigns
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public Dictionary<string, object> listCampaigns(int limit, int offset)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            if (limit > 0) data.Add("limit", limit);
            if (offset > 0) data.Add("offset", offset);
            Dictionary<string, object> result = null;
            try
            {
                result = this.sendRequest("campaigns", "GET", data);
            }
            catch (IOException) { }
            return this.handleResult(result);
        }
        /// <summary>
        /// Get information about campaign
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Dictionary<string, object> getCampaignInfo(int id)
        {
            Dictionary<string, object> result = null;
            try
            {
                result = this.sendRequest("campaigns/" + id, "GET", null);
            }
            catch (IOException) { }
            return this.handleResult(result);
        }
        /// <summary>
        /// Get campaign statistic by countries
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Dictionary<string, object> campaignStatByCountries(int id)
        {
            if (id <= 0) return this.handleError("Empty campaign id");
            Dictionary<string, object> result = null;
            try
            {
                result = this.sendRequest("campaigns/" + id + "/countries", "GET", null);
            }
            catch (IOException) { }
            return this.handleResult(result);
        }
        /// <summary>
        /// Get campaign statistic by referrals
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Dictionary<string, object> campaignStatByReferrals(int id)
        {
            if (id <= 0) return this.handleError("Empty campaign id");
            Dictionary<string, object> result = null;
            try
            {
                result = this.sendRequest("campaigns/" + id + "/referrals", "GET", null);
            }
            catch (IOException) { }
            return this.handleResult(result);
        }

        public Dictionary<string, object> createCampaign(string senderName, string senderEmail, string subject, string body, int bookId, string name,string send_date="", string attachments="")
        {
            if (senderName.Length == 0 || senderEmail.Length == 0 || subject.Length == 0 || body.Length == 0 || bookId <= 0)
                return this.handleError("Not all data.");
            string encodedBody = this.Base64Encode(body);
            Dictionary<string, object> data = new Dictionary<string, object>();
            if (attachments.Length > 0) data.Add("attachments", attachments);
            if (send_date.Length > 0) data.Add("send_date", send_date);
            data.Add("sender_name", senderName);
            data.Add("sender_email", senderEmail);
            data.Add("subject", subject);
            if (encodedBody.Length > 0) data.Add("body", encodedBody.ToString());
            data.Add("list_id", bookId);
            if (name.Length > 0) data.Add("name", name);
            Dictionary<string, object> result = null;
            try
            {
                result = this.sendRequest("campaigns", "POST", data);
            }
            catch (IOException) { }
            return this.handleResult(result);
        }
        /// <summary>
        /// Cancel campaign
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Dictionary<string, object> cancelCampaign(int id)
        {
            if (id <= 0) return this.handleError("Empty campaign id");
            Dictionary<string, object> result = null;
            try
            {
                result = this.sendRequest("campaigns/" + id, "DELETE", null);
            }
            catch (IOException) { }
            return this.handleResult(result);
        }
        /// <summary>
        /// Get list of allowed senders
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> listSenders()
        {
            Dictionary<string, object> result = null;
            try
            {
                result = this.sendRequest("senders", "GET", null);
            }
            catch (IOException) { }
            return this.handleResult(result);
        }
        /// <summary>
        /// Add new sender
        /// </summary>
        /// <param name="senderName"></param>
        /// <param name="senderEmail"></param>
        /// <returns></returns>
        public Dictionary<string, object> addSender(string senderName, string senderEmail)
        {
            if (senderName.Length == 0 || senderEmail.Length == 0) return this.handleError("Empty sender name or email");
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("name", senderName);
            data.Add("email", senderEmail);
            Dictionary<string, object> result = null;
            try
            {
                result = this.sendRequest("senders", "POST", data);
            }
            catch (IOException) { }
            return this.handleResult(result);
        }
        /// <summary>
        /// Remove sender
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Dictionary<string, object> removeSender(string email)
        {
            if (email.Length == 0) return this.handleError("Empty email");
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("email", email);
            Dictionary<string, object> result = null;
            try
            {
                result = this.sendRequest("senders", "DELETE", data);
            }
            catch (IOException) { }
            return this.handleResult(result);
        }
        /// <summary>
        /// Activate sender using code from mail
        /// </summary>
        /// <param name="email"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public Dictionary<string, object> activateSender(string email, string code)
        {
            if (email.Length == 0 || code.Length == 0) return this.handleError("Empty email or activation code");
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("code", code);
            Dictionary<string, object> result = null;
            try
            {
                result = this.sendRequest("senders/" + email + "/code", "POST", data);
            }
            catch (IOException) { }
            return this.handleResult(result);
        }
        /// <summary>
        /// Send mail with activation code on sender email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Dictionary<string, object> getSenderActivationMail(string email)
        {
            if (email.Length == 0) return this.handleError("Empty email");
            Dictionary<string, object> result = null;
            try
            {
                result = this.sendRequest("senders/" + email + "/code", "GET", null);
            }
            catch (IOException) { }
            return this.handleResult(result);
        }
        /// <summary>
        /// Get global information about email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Dictionary<string, object> getEmailGlobalInfo(string email)
        {
            if (email.Length == 0) return this.handleError("Empty email");
            Dictionary<string, object> result = null;
            try
            {
                result = this.sendRequest("emails/" + email, "GET", null);
            }
            catch (IOException) { }
            return this.handleResult(result);
        }
        /// <summary>
        /// Remove email address from all books
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Dictionary<string, object> removeEmailFromAllBooks(string email)
        {
            if (email.Length == 0) return this.handleError("Empty email");
            Dictionary<string, object> result = null;
            try
            {
                result = this.sendRequest("emails/" + email, "DELETE", null);
            }
            catch (IOException) { }
            return this.handleResult(result);
        }
        /// <summary>
        /// Get statistic for email by all campaigns
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Dictionary<string, object> emailStatByCampaigns(string email)
        {
            if (email.Length == 0) return this.handleError("Empty email");
            Dictionary<string, object> result = null;
            try
            {
                result = this.sendRequest("emails/" + email + "/campaigns", "GET", null);
            }
            catch (IOException) { }
            return this.handleResult(result);
        }
        /// <summary>
        /// Show emails from blacklist
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> getBlackList()
        {
            Dictionary<string, object> result = null;
            try
            {
                result = this.sendRequest("blacklist", "GET", null);
            }
            catch (IOException) { }
            return this.handleResult(result);
        }
        /// <summary>
        /// Add email address to blacklist
        /// </summary>
        /// <param name="emails"></param>
        /// <returns></returns>
        public Dictionary<string, object> addToBlackList(string emails)
        {
            if (emails.Length == 0) return this.handleError("Empty emails");
            Dictionary<string, object> data = new Dictionary<string, object>();
            string encodedemails = this.Base64Encode(emails);
            data.Add("emails", encodedemails);
            Dictionary<string, object> result = null;
            try
            {
                result = this.sendRequest("blacklist", "POST", data);
            }
            catch (IOException) { }
            return this.handleResult(result);
        }
        /// <summary>
        /// Remove email address from blacklist
        /// </summary>
        /// <param name="emails"></param>
        /// <returns></returns>
        public Dictionary<string, object> removeFromBlackList(string emails)
        {
            if (emails.Length == 0) return this.handleError("Empty emails");
            Dictionary<string, object> data = new Dictionary<string, object>();
            string encodedemails = this.Base64Encode(emails);
            data.Add("emails", encodedemails);
            Dictionary<string, object> result = null;
            try
            {
                result = this.sendRequest("blacklist", "DELETE", data);
            }
            catch (IOException) { }
            return this.handleResult(result);
        }
        /// <summary>
        /// Return user balance
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        public Dictionary<string, object> getBalance(string currency)
        {
            string url = "balance";
            if (currency.Length > 0)
            {
                currency = currency.ToUpper();
                url = url + "/" + currency;
            }
            Dictionary<string, object> result = null;
            try
            {
                result = this.sendRequest(url, "GET", null);
            }
            catch (IOException) { }
            return this.handleResult(result);
        }
        /// <summary>
        /// Send mail using SMTP
        /// </summary>
        /// <param name="emaildata"></param>
        /// <returns></returns>
        public Dictionary<string, object> smtpSendMail(Dictionary<string, object> emaildata)
        {
            if (emaildata.Count == 0) return this.handleError("Empty email data");
            string html = emaildata["html"].ToString();
            emaildata.Remove("html");
            emaildata.Add("html", this.Base64Encode(html));
            Dictionary<string, object> data = new Dictionary<string, object>();
            string serialized = JsonConvert.SerializeObject(emaildata);
            data.Add("email", serialized);
            Dictionary<string, object> result = null;
            try
            {
                result = this.sendRequest("smtp/emails", "POST", data);
            }
            catch (IOException) { }
            return this.handleResult(result);
        }
        /// <summary>
        /// Get list of emails that was sent by SMTP 
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="sender"></param>
        /// <param name="recipient"></param>
        /// <returns></returns>
        public Dictionary<string, object> smtpListEmails(int limit, int offset, string fromDate, string toDate, string sender, string recipient)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("limit", limit);
            data.Add("offset", offset);
            if (fromDate.Length > 0) data.Add("fromDate", fromDate);
            if (toDate.Length > 0) data.Add("toDate", toDate);
            if (sender.Length > 0) data.Add("sender", sender);
            if (recipient.Length > 0) data.Add("recipient", recipient);
            Dictionary<string, object> result = null;
            try
            {
                result = this.sendRequest("smtp/emails", "GET", data);
            }
            catch (IOException) { }
            return this.handleResult(result);
        }
        /// <summary>
        /// Get information about email by his id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Dictionary<string, object> smtpGetEmailInfoById(string id)
        {
            if (id.Length == 0) return this.handleError("Empty id");
            Dictionary<string, object> result = null;
            try
            {
                result = this.sendRequest("smtp/emails/" + id, "GET", null);
            }
            catch (IOException) { }
            return this.handleResult(result);
        }
        /// <summary>
        /// Unsubscribe emails using SMTP
        /// </summary>
        /// <param name="emails"></param>
        /// <returns></returns>
        public Dictionary<string, object> smtpUnsubscribeEmails(string emails)
        {
            if (emails.Length == 0) return this.handleError("Empty emails");
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("emails", emails);
            Dictionary<string, object> result = null;
            try
            {
                result = this.sendRequest("/smtp/unsubscribe", "POST", data);
            }
            catch (IOException) { }
            return this.handleResult(result);
        }
        /// <summary>
        /// Remove emails from unsubscribe list using SMTP
        /// </summary>
        /// <param name="emails"></param>
        /// <returns></returns>
        public Dictionary<string, object> smtpRemoveFromUnsubscribe(string emails)
        {
            if (emails.Length == 0) return this.handleError("Empty emails");
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("emails", emails);
            Dictionary<string, object> result = null;
            try
            {
                result = this.sendRequest("/smtp/unsubscribe", "DELETE", data);
            }
            catch (IOException) { }
            return this.handleResult(result);
        }
        /// <summary>
        /// Get list of allowed IPs using SMTP
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> smtpListIP()
        {
            Dictionary<string, object> result = null;
            try
            {
                result = this.sendRequest("smtp/ips", "GET", null);
            }
            catch (IOException) { }
            return this.handleResult(result);
        }
        /// <summary>
        /// Get list of allowed domains using SMTP
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> smtpListAllowedDomains()
        {
            Dictionary<string, object> result = null;
            try
            {
                result = this.sendRequest("smtp/domains", "GET", null);
            }
            catch (IOException) { }
            return this.handleResult(result);
        }
        /// <summary>
        /// Add domain using SMTP
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Dictionary<string, object> smtpAddDomain(string email)
        {
            if (email.Length == 0) return this.handleError("Empty email");
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("email", email);
            Dictionary<string, object> result = null;
            try
            {
                result = this.sendRequest("smtp/domains", "POST", data);
            }
            catch (IOException) { }
            return this.handleResult(result);
        }
        /// <summary>
        /// Send confirm mail to verify new domain
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Dictionary<string, object> smtpVerifyDomain(string email)
        {
            if (email.Length == 0) return this.handleError("Empty email");
            Dictionary<string, object> result = null;
            try
            {
                result = this.sendRequest("smtp/domains/" + email, "GET", null);
            }
            catch (IOException) { }
            return this.handleResult(result);
        }
        /// <summary>
        /// Get list of push campaigns
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public Dictionary<string, object> pushListCampaigns(int limit, int offset)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            if (limit > 0) data.Add("limit", limit);
            if (offset > 0) data.Add("offset", offset);
            Dictionary<string, object> result = null;
            try
            {
                result = this.sendRequest("push/tasks", "GET", data);
            }
            catch (IOException) { }
            return this.handleResult(result);
        }
        /// <summary>
        /// Get amount of websites
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> pushCountWebsites()
        {
            Dictionary<string, object> result = null;
            try
            {
                result = this.sendRequest("push/websites/total", "GET", null);
            }
            catch (IOException) { }
            return this.handleResult(result);
        }
        /// <summary>
        /// Get list of websites
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public Dictionary<string, object> pushListWebsites(int limit, int offset)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            if (limit > 0) data.Add("limit", limit);
            if (offset > 0) data.Add("offset", offset);
            Dictionary<string, object> result = null;
            try
            {
                result = this.sendRequest("push/websites", "GET", data);
            }
            catch (IOException) { }
            return this.handleResult(result);
        }
        /// <summary>
        /// Get list of all variables for website
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Dictionary<string, object> pushListWebsiteVariables(int id)
        {
            Dictionary<string, object> result = null;
            string url = "";
            if (id > 0)
            {
                url = "push/websites/" + id + "/variables";
                try
                {
                    result = this.sendRequest(url, "GET", null);
                }
                catch (IOException) { }
            }
            return this.handleResult(result);
        }
        /// <summary>
        /// Get list of subscriptions for the website
        /// </summary>
        /// <param name="id"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public Dictionary<string, object> pushListWebsiteSubscriptions(int id, int limit, int offset)
        {
            Dictionary<string, object> result = null;
            string url = "";
            if (id > 0)
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                if (limit > 0) data.Add("limit", limit);
                if (offset > 0) data.Add("offset", offset);
                url = "push/websites/" + id + "/subscriptions";
                try
                {
                    result = this.sendRequest(url, "GET", data);
                }
                catch (IOException) { }
                return this.handleResult(result);
            }
            else
            {
                return this.handleError("Empty ID");
            }
        }
        /// <summary>
        /// Get amount of subscriptions for the site
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Dictionary<string, object> pushCountWebsiteSubscriptions(int id)
        {
            Dictionary<string, object> result = null;
            string url = "";
            if (id > 0)
            {
                url = "push/websites/" + id + "/subscriptions/total";
                try
                {
                    result = this.sendRequest(url, "GET", null);
                }
                catch (IOException) { }
                return this.handleResult(result);
            }
            else
            {
                return this.handleError("Empty ID");
            }
        }
        /// <summary>
        /// Set state for subscription
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public Dictionary<string, object> pushSetSubscriptionState(int id, int state)
        {
            if (id > 0)
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("id", id);
                data.Add("state", state);
                Dictionary<string, object> result = null;
                try
                {
                    result = this.sendRequest("push/subscriptions/state", "POST", data);
                }
                catch (IOException) { }
                return this.handleResult(result);
            }
            else
            {
                return this.handleError("Empty ID");
            }
        }
        /// <summary>
        /// Create new push campaign
        /// </summary>
        /// <param name="taskinfo"></param>
        /// <param name="additionalParams"></param>
        /// <returns></returns>
        public Dictionary<string, object> createPushTask(Dictionary<string, object> taskinfo, Dictionary<string, object> additionalParams)
        {
            Dictionary<string, object> data = taskinfo;
            if (!data.ContainsKey("ttl")) data.Add("ttl", 0);
            if (!data.ContainsKey("title") || !data.ContainsKey("website_id") || !data.ContainsKey("body"))
            {
                return this.handleError("Not all data");
            }
            if (additionalParams != null && additionalParams.Count > 0)
            {
                foreach (var item in additionalParams)
                {
                    data.Add(item.Key, item.Value);
                }
            }
            Dictionary<string, object> result = null;
            try
            {
                result = this.sendRequest("push/tasks", "POST", data);
            }
            catch (IOException) { }
            return this.handleResult(result);
        }
    }
}
