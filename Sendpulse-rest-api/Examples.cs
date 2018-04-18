using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sendpulse_rest_api.restapi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sendpulse_rest_api
{
    class Examples {
        //https://login.sendpulse.com/settings/#api
        private static string userId = "";
        private static string secret = "";
        static void Main(string[] args)
        {
            Sendpulse sp = new Sendpulse(userId, secret);
            Dictionary<string, string> attachment = new Dictionary<string, string>();
            var content_Bytes = File.ReadAllBytes("example_file_path/example_file_name.pdf");
            String base64_file_content = System.Convert.ToBase64String(content_Bytes);
            attachment.Add("example_file_name.pdf", base64_file_content);
            //How to attach files
            smtpSendMail(sp, "From Name", "fromemail@example.com", "Recipient Name", "recipient@example.com", "<b>HTML BODY</b>", "Text body", "Subject", attachment);
        }
        /// <summary>
        /// Retrieving a list of address books
        /// </summary>
        /// <param name="sp"></param>
        static void getlistAddressBooks(Sendpulse sp, int limit, int offset)
        {
            Dictionary<string, object> result = sp.listAddressBooks(limit, offset);
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }
        /// <summary>
        /// Retrieving address book information
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="BookId"></param>
        static void getBookInfo(Sendpulse sp,int BookId)
        {
            Dictionary<string, object> result = sp.getBookInfo(BookId); //BOOKID
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }
        /// <summary>
        /// Retrieving a list of emails from an address book
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="BookId"></param>
        static void getEmailsFromBook(Sendpulse sp, int BookId)
        {
            Dictionary<string, object> result = sp.getBookInfo(BookId); //BOOKID
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }
        /// <summary>
        /// Editing an address book
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="BookId"></param>
        static void editAddressBook(Sendpulse sp, int BookId)
        {
            Dictionary<string, object> result = sp.editAddressBook(BookId, "NEW NAME");
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }
        /// <summary>
        /// Creating an address book
        /// </summary>
        /// <param name="sp"></param>
        static void createAddressBook(Sendpulse sp)
        {
            Dictionary<string, object> result = sp.createAddressBook("NEW NAME"); 
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }
        /// <summary>
        /// Adding emails to an address book
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="BookId"></param>
        static void addEmailsToAddressBook(Sendpulse sp, int BookId)
        {
            JObject email = new JObject();
            email.Add("email", "test@example.com");
            JObject variables = new JObject();
            variables.Add("Name", "Alex");
            email.Add("variables", variables);
            JArray emails = new JArray();
            emails.Add(email);
            Dictionary<string, object> result = sp.addEmails(BookId, JsonConvert.SerializeObject(emails));
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }
        /// <summary>
        /// Deleting emails from an address book
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="BookId"></param>
        static void removeEmailsFromAddressBook(Sendpulse sp, int BookId)
        {
            List<string> keys = new List<string>() { };
            string[] arr1 = new string[] { "test2@test.com", "test@test.com" };
            string emails = JsonConvert.SerializeObject(arr1);
            Dictionary<string, object> result = sp.removeEmails(BookId, emails);
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }
        /// <summary>
        /// Retrieving information for specific email address from an address book
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="BookId"></param>
        static void getEmailInfo(Sendpulse sp, int BookId)
        {
            Dictionary<string, object> result = sp.getEmailInfo(BookId, "test.com");
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }
        /// <summary>
        /// Erasing an address book
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="BookId"></param>
        static void removeAddressBook(Sendpulse sp, int BookId)
        {
            Dictionary<string, object> result = sp.removeAddressBook(BookId);
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }
        /// <summary>
        /// Calculating the cost of a campaign carried out by an address book
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="BookId"></param>
        static void campaignCost(Sendpulse sp, int BookId)
        {
            Dictionary<string, object> result = sp.campaignCost(BookId);
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }
        /// <summary>
        /// Retrieving the list of campaigns
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        static void getlistCampaigns(Sendpulse sp, int limit, int offset)
        {
            Dictionary<string, object> result = sp.listCampaigns(limit, offset);
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }
        /// <summary>
        /// Campaign information
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="TaskId"></param>
        static void getCampaignInfo(Sendpulse sp, int TaskId)
        {
            Dictionary<string, object> result = sp.getCampaignInfo(TaskId);
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }
        /// <summary>
        /// Get campaign statistic by countries
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="TaskId"></param>
        static void campaignStatByCountries(Sendpulse sp, int TaskId)
        {
            Dictionary<string, object> result = sp.campaignStatByCountries(TaskId);
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }
        /// <summary>
        /// Get campaign statistic by referrals
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="TaskId"></param>
        static void campaignStatByReferrals(Sendpulse sp, int TaskId)
        {
            Dictionary<string, object> result = sp.campaignStatByReferrals(TaskId);
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }
        /// <summary>
        /// Creating a campaign
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="BookId"></param>
        static void createEmailCampaign(Sendpulse sp, int BookId)
        {
            Dictionary<string, object> result = sp.createCampaign("test1@example.com", "test@example.com", "Test from C# api", "testing body", BookId, "Test", "2061-08-09 23:00:00");
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }
        /// <summary>
        /// Cancel campaign
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="TaskId"></param>
        static void cancelEmailCampaign(Sendpulse sp, int TaskId)
        {
            Dictionary<string, object> result = sp.cancelCampaign(TaskId);
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }
        /// <summary>
        /// Retrieving a list of all of the senders
        /// </summary>
        /// <param name="sp"></param>
        static void listSenders(Sendpulse sp)
        {
            Dictionary<string, object> result = sp.listSenders();
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }
        /// <summary>
        /// Adding a sender
        /// </summary>
        /// <param name="sp"></param>
        static void addSender(Sendpulse sp)
        {
            Dictionary<string, object> result = sp.addSender("test", "test@example.com");
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }
        /// <summary>
        /// Deleting a sender
        /// </summary>
        /// <param name="sp"></param>
        static void removeSender(Sendpulse sp)
        {
            Dictionary<string, object> result = sp.removeSender("test@example.com");
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }
        /// <summary>
        /// Activating a sender
        /// </summary>
        /// <param name="sp"></param>
        static void activateSender(Sendpulse sp)
        {
            Dictionary<string, object> result = sp.activateSender("test@example.com", "9b11a172b89c52b63c7b06c61ffa4113");
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }
        /// <summary>
        /// Receiving the activation code at the sender’s email address
        /// </summary>
        /// <param name="sp"></param>
        static void getSenderActivationMail(Sendpulse sp)
        {
            Dictionary<string, object> result = sp.getSenderActivationMail("test@example.com");
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }
        /// <summary>
        /// Retrieve general information for a specific email address
        /// </summary>
        /// <param name="sp"></param>
        static void getEmailGlobalInfo(Sendpulse sp)
        {
            Dictionary<string, object> result = sp.getEmailGlobalInfo("test@example.com");
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }
        /// <summary>
        /// Erasing an email address from all address books
        /// </summary>
        /// <param name="sp"></param>
        static void removeEmailFromAllBooks(Sendpulse sp)
        {
            Dictionary<string, object> result = sp.removeEmailFromAllBooks("test@example.com");
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }
        /// <summary>
        /// Retrieving statistics for an email address by campaigns
        /// </summary>
        /// <param name="sp"></param>
        static void getemailStatByCampaigns(Sendpulse sp)
        {
            Dictionary<string, object> result = sp.emailStatByCampaigns("test@example.com");
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }
        /// <summary>
        /// Viewing the blacklist
        /// </summary>
        /// <param name="sp"></param>
        static void getBlackList(Sendpulse sp)
        {
            Dictionary<string, object> result = sp.getBlackList();
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }
        /// <summary>
        /// Blacklisting an email address
        /// </summary>
        /// <param name="sp"></param>
        static void addToBlackList(Sendpulse sp)
        {
            Dictionary<string, object> result = sp.addToBlackList("test@example.com");
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }
        /// <summary>
        /// Erasing an email address from the blacklist
        /// </summary>
        /// <param name="sp"></param>
        static void removeFromBlackList(Sendpulse sp)
        {
            Dictionary<string, object> result = sp.removeFromBlackList("test@example.com");
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }
        /// <summary>
        /// Checking the user’s balance
        /// </summary>
        /// <param name="sp"></param>
        static void getBalance(Sendpulse sp)
        {
            Dictionary<string, object> result = sp.getBalance("USD");
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }
        
        /// <summary>
        /// Sending email via SendPulse SMTP
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="from_name"></param>
        /// <param name="from_email"></param>
        /// <param name="name_to"></param>
        /// <param name="email_to"></param>
        /// <param name="html"></param>
        /// <param name="text"></param>
        /// <param name="subject"></param>
        /// <param name="attachments"></param>
        static void smtpSendMail(Sendpulse sp, String from_name, String from_email, String name_to, String email_to, String html, String text, String subject, Dictionary<string, string> attachments)
        {
            Dictionary<string, object> from = new Dictionary<string, object>();
            from.Add("name", from_name);
            from.Add("email", from_email);
            ArrayList to = new ArrayList();
            Dictionary<string, object> elementto = new Dictionary<string, object>();
            elementto.Add("name", name_to);
            elementto.Add("email", email_to);
            to.Add(elementto);
            Dictionary<string, object> emaildata = new Dictionary<string, object>();
            emaildata.Add("html", html);
            emaildata.Add("text", text);
            emaildata.Add("subject", subject);
            emaildata.Add("from", from);
            emaildata.Add("to", to);
            if (attachments.Count > 0)
            {
                emaildata.Add("attachments_binary", attachments);
            }
            Dictionary<string, object> result = sp.smtpSendMail(emaildata);
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }

        /// <summary>
        /// Retrieving a list of emails
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        static void smtpListEmails(Sendpulse sp, int limit, int offset)
        {
            Dictionary<string, object> result = sp.smtpListEmails(limit, offset, "", "", "", "");
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }
        /// <summary>
        /// Retrieving information for a specific email
        /// </summary>
        /// <param name="sp"></param>
        static void smtpGetEmailInfoById(Sendpulse sp)
        {
            Dictionary<string, object> result = sp.smtpGetEmailInfoById("d55e8d794cfe6d15a883df3905e52f2e");
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }
        /// <summary>
        /// Unsubscribing a recipient
        /// </summary>
        /// <param name="sp"></param>
        static void smtpUnsubscribeEmails(Sendpulse sp)
        {
            ArrayList emails = new ArrayList();
            Dictionary<string, object> email = new Dictionary<string, object>();
            email.Add("email", "test@example.com");
            email.Add("comment", "123");
            emails.Add(email);
            string uemails = JsonConvert.SerializeObject(emails);
            Dictionary<string, object> result = sp.smtpUnsubscribeEmails(uemails);
        }
        /// <summary>
        /// Erasing from the unsubscribed list
        /// </summary>
        /// <param name="sp"></param>
        static void smtpRemoveFromUnsubscribe(Sendpulse sp)
        {
            ArrayList emails = new ArrayList();
            emails.Add("test@example.com");
            string uemails = JsonConvert.SerializeObject(emails);
            Dictionary<string, object> result = sp.smtpRemoveFromUnsubscribe(uemails);
        }
        /// <summary>
        /// Retrieving the sender’s IP address
        /// </summary>
        /// <param name="sp"></param>
        static void getsmtpSendersListIP(Sendpulse sp)
        {
            Dictionary<string, object> result = sp.smtpListIP();
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }
        /// <summary>
        /// Retrieving a list of allowed domains
        /// </summary>
        /// <param name="sp"></param>
        static void smtpListAllowedDomains(Sendpulse sp)
        {
            Dictionary<string, object> result = sp.smtpListAllowedDomains();
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }
        /// <summary>
        /// Adding a domain
        /// </summary>
        /// <param name="sp"></param>
        static void smtpAddDomain(Sendpulse sp)
        {
            Dictionary<string, object> result = sp.smtpAddDomain("test@example.com");
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }
        /// <summary>
        /// New domain verification
        /// </summary>
        /// <param name="sp"></param>
        static void smtpVerifyDomain(Sendpulse sp)
        {
            Dictionary<string, object> result = sp.smtpVerifyDomain("test@example.com");
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }
        /// <summary>
        /// Retrieving a list of sent push campaigns
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        static void pushListCampaigns(Sendpulse sp, int limit, int offset)
        {
            Dictionary<string, object> result = sp.pushListCampaigns(limit, offset);
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }
        /// <summary>
        /// Get push campaign info
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="id"></param>
        static void getpushCampaignInfo(Sendpulse sp,int id)
        {
            Dictionary<string, object> result = sp.pushCampaignInfo(id);
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }
        /// <summary>
        /// Retrieving the total number of websites
        /// </summary>
        /// <param name="sp"></param>
        static void pushCountWebsites(Sendpulse sp)
        {
            Dictionary<string, object> result = sp.pushCountWebsites();
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }
        /// <summary>
        /// Retrieving a list of websites
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        static void pushListWebsites(Sendpulse sp, int limit, int offset)
        {
            Dictionary<string, object> result = sp.pushListWebsites(limit, offset);
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }
        /// <summary>
        /// Retrieving a list of variables for a website
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="siteId"></param
        static void pushListWebsiteVariables(Sendpulse sp, int siteId)
        {
            Dictionary<string, object> result = sp.pushListWebsiteVariables(siteId);
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }
        /// <summary>
        /// Retrieving a list of website subscribers
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="siteId"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        static void pushListWebsiteSubscriptions(Sendpulse sp, int siteId,int limit, int offset)
        {
            Dictionary<string, object> result = sp.pushListWebsiteSubscriptions(siteId, limit, offset);
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }
        /// <summary>
        /// Retrieving the number of website subscribers
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="siteId"></param>
        static void pushCountWebsiteSubscriptions(Sendpulse sp, int siteId)
        {
            Dictionary<string, object> result = sp.pushCountWebsiteSubscriptions(siteId);
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }
        /// <summary>
        /// Activating/Deactivating a subscriber
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="siteId"></param>
        /// <param name="state"></param>
        static void pushSetSubscriptionState(Sendpulse sp,int siteId,int state)
        {
            Dictionary<string, object> result = sp.pushSetSubscriptionState(siteId, state);
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }
        /// <summary>
        /// Creating a new Push campaign
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="siteId"></param>
        static void createPushTask(Sendpulse sp, int siteId)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            Dictionary<string, object> additionaldata = new Dictionary<string, object>();
            data.Add("title", "test push");
            data.Add("website_id", siteId); 
            data.Add("body", "test push body");
            data.Add("ttl", 300);
            additionaldata.Add("filter_browser", "Chrome");
            JObject filter = new JObject();
            filter.Add("variable_name", "user_email");
            filter.Add("operator", "or");
            JArray conditions = new JArray();
            JObject condition = new JObject();
            condition.Add("condition", "likewith");
            condition.Add("value", "test");
            conditions.Add(condition);
            filter.Add("conditions", conditions);
            additionaldata.Add("filter", filter);
            Dictionary<string, object> result = sp.createPushTask(data, additionaldata);
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }
        /// <summary>
        /// Adding telephone numbers to a mailing list
        /// </summary>
        /// <param name="sp">Sp.</param>
        static void addPhones(Sendpulse sp){
            string[] phones = new string[] { "+380500000000", "+380500000001" };
            int bookId = 1111111;
            string data = JsonConvert.SerializeObject(phones);
            Dictionary<string, object> result = sp.addPhones(bookId, data);
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }
        /// <summary>
        /// Deleting telephone numbers from a mailing list
        /// </summary>
        /// <param name="sp">Sp.</param>
        static void removePhones(Sendpulse sp)
        {
            string[] phones = new string[] { "+380500000000", "+380500000001" };
            int bookId = 1111111;
            string data = JsonConvert.SerializeObject(phones);
            Dictionary<string, object> result = sp.removePhones(bookId, data);
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }
        /// <summary>
        /// Updating the list of variables by a phone number
        /// </summary>
        /// <param name="sp">Sp.</param>
        static void updatePhones(Sendpulse sp){
            string[] phones = new string[] { "+380500000000", "+380500000001" };
            List<object> variables = new List<object>();
            Dictionary<string,object> variable = new Dictionary<string,object>();
            variable.Add("name", "var_name");
            variable.Add("type", "string");
            variable.Add("value", "value");
            variables.Add(variable);
            string variablesData = JsonConvert.SerializeObject(variables);
            int bookId = 1111111;
            string phonesData = JsonConvert.SerializeObject(phones);
            Dictionary<string, object> result = sp.updatePhones(bookId, phonesData,variablesData);
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();   
        }
        /// <summary>
        /// Retrieving information for specific phone number
        /// </summary>
        /// <param name="sp">Sp.</param>
        static void getPhoneInfo(Sendpulse sp)
        {
            int bookId = 1111111;
            string phone = "+380500000000";
            Dictionary<string, object> result = sp.getPhoneInfo(bookId, phone);
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }
        /// <summary>
        /// Adding telephone number to the blacklist
        /// </summary>
        /// <param name="sp">Sp.</param>
        static void addPhonesToBlackList(Sendpulse sp){
            string[] phones = new string[] { "+380500000000", "+380500000001" };
            string description = "Description";
            string phonesData = JsonConvert.SerializeObject(phones);
            Dictionary<string, object> result = sp.addPhonesToBlackList(phonesData, description);
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }
        /// <summary>
        /// Deleting a phone number from the blacklist
        /// </summary>
        /// <param name="sp">Sp.</param>
        static void removePhonesFromBlackList(Sendpulse sp)
        {
            string[] phones = new string[] { "+380500000000", "+380500000001" };
            string phonesData = JsonConvert.SerializeObject(phones);
            Dictionary<string, object> result = sp.removePhonesFromBlackList(phonesData);
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }
        /// <summary>
        /// Viewing the blacklist
        /// </summary>
        /// <param name="sp">Sp.</param>
        static void getBlackListPhones(Sendpulse sp){
            Dictionary<string, object> result = sp.getBlackListPhones();
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }
        /// <summary>
        /// Retrieving information of telephone numbers in the blacklist
        /// </summary>
        /// <param name="sp">Sp.</param>
        static void getPhonesInfoInBlackList(Sendpulse sp)
        {
            string[] phones = new string[] { "+380500000000", "+380500000001" };
            string phonesData = JsonConvert.SerializeObject(phones);
            Dictionary<string, object> result = sp.getPhonesInfoInBlackList(phonesData);
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }
        /// <summary>
        /// Creating of a campaign
        /// </summary>
        /// <param name="sp">Sp.</param>
        static void sendSmsCampaign(Sendpulse sp){
            int bookId = 1111111;
            Dictionary<string, object> result = sp.sendSmsCampaign(bookId, "test");
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }
        /// <summary>
        /// Creating a campaign to a list of phone numbers
        /// </summary>
        /// <param name="sp">Sp.</param>
        static void sendSmsCampaignByPhones(Sendpulse sp){
            string[] phones = new string[] { "+380500000000", "+380500000001" };
            string phonesData = JsonConvert.SerializeObject(phones);
            Dictionary<string, object> result = sp.sendSmsCampaignByPhones(phonesData, "test");
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }
        /// <summary>
        /// Retrieving a list of campaigns by date
        /// </summary>
        /// <param name="sp">Sp.</param>
        static void getSmsCampaignsList(Sendpulse sp)
        {
            string dateFrom = "2018-01-01 00:00:00";
            string dateTo = "2018-05-01 00:00:00";
            Dictionary<string, object> result = sp.getSmsCampaignsList(dateFrom, dateTo);
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }
        /// <summary>
        /// Retrieving a campaign information
        /// </summary>
        /// <param name="sp">Sp.</param>
        static void getSmsCampaignInfo(Sendpulse sp)
        {
            int id = 1111111;
            Dictionary<string, object> result = sp.getSmsCampaignInfo(id);
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }
        /// <summary>
        /// Cancelling a campaign in case when the sending has not started
        /// </summary>
        /// <param name="sp">Sp.</param>
        static void cancelSmsCampaign(Sendpulse sp)
        {
            int id = 1111111;
            Dictionary<string, object> result = sp.cancelSmsCampaign(id);
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }
        /// <summary>
        /// Calculating the cost of a campaign
        /// </summary>
        /// <param name="sp">Sp.</param>
        static void getSmsCampaignCost(Sendpulse sp)
        {
            int bookId = 1111111;
            string sender = "sender_name";
            Dictionary<string, object> result = sp.getSmsCampaignCost("test",sender,bookId);
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }
        /// <summary>
        /// Deleting a campaign
        /// </summary>
        /// <param name="sp">Sp.</param>
        static void deleteSmsCampaign(Sendpulse sp)
        {
            int id = 1111111;
            Dictionary<string, object> result = sp.deleteSmsCampaign(id);
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }
        /// <summary>
        /// Adding telephone numbers to a mailing list with variables
        /// </summary>
        /// <param name="sp">Sp.</param>
        static void addPhonesToAddreessBook(Sendpulse sp)
        {
            Dictionary<string, object> phones = new Dictionary<string, object>();
            JArray variablesData = new JArray();
            JArray variables = new JArray();
            JObject variable = new JObject();
            variable.Add("name", "var");
            variable.Add("type", "string");
            variable.Add("value", "value");
            variables.Add(variable);
            variablesData.Add(variables);
            phones.Add("+380500000000", variablesData);
            phones.Add("+380500000001", variablesData);
            int bookId = 1111111;
            string phonesData = JsonConvert.SerializeObject(phones);
            Console.WriteLine("Phones {0}", phonesData);
            Dictionary<string, object> result = sp.addPhonesToAddreessBook(bookId, phonesData);
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }

        /// <summary>
        /// Create viber campaign.
        /// </summary>
        /// <param name="sp">Sp.</param>
        static void sendViberCampaign(Sendpulse sp)
        {
            Dictionary<string, object> additional = new Dictionary<string, object>();
            JObject button = new JObject();
            button.Add("text", "test");
            button.Add("link", "https://test.com");
            additional.Add("button", button);
            int bookId = 1111111;
            string additionalData = JsonConvert.SerializeObject(additional);
            Dictionary<string, object> result = sp.sendViberCampaign("", bookId, "test", 1, additionalData);
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
        }
        /// <summary>
        /// Gets viber campaigns list.
        /// </summary>
        /// <param name="sp">Sp.</param>
        static void getViberTasksList(Sendpulse sp)
        {
            Dictionary<string, object> result = sp.getViberTasksList();
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
        }
        /// <summary>
        /// Retrieving a list of viber senders.
        /// </summary>
        /// <param name="sp">Sp.</param>
        static void getViberSenders(Sendpulse sp)
        {
            Dictionary<string, object> result = sp.getViberSenders();
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
        }
        /// <summary>
        /// Retrieving viber campaign stat.
        /// </summary>
        /// <param name="sp">Sp.</param>
        static void getViberCampaignStat(Sendpulse sp)
        {
            int campaignId = 1111111;
            Dictionary<string, object> result = sp.getViberCampaignStat(campaignId);
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
        }
        /// <summary>
        /// Get viber sender info.
        /// </summary>
        /// <param name="sp">Sp.</param>
        static void getViberSender(Sendpulse sp)
        {
            int senderId = 1;
            Dictionary<string, object> result = sp.getViberSender(senderId);
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
        }
        /// <summary>
        /// Get viber task recipients.
        /// </summary>
        /// <param name="sp">Sp.</param>
        static void getViberTaskRecipients(Sendpulse sp)
        {
            int taskId = 1111111;
            Dictionary<string, object> result = sp.getViberTaskRecipients(taskId);
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
        }
    }
}
