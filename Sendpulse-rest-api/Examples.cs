using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sendpulse_rest_api.restapi;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Sendpulse_rest_api
{
    class Examples {
        //https://login.sendpulse.com/settings/#api
        private static string userId = "";
        private static string secret = "";
        static void Main(string[] args)
        {
            Sendpulse sp = new Sendpulse(userId, secret);
            getlistAddressBooks(sp, 10,0);
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
        /// Sending an email
        /// </summary>
        /// <param name="sp"></param>
        static void smtpSendMail(Sendpulse sp)
        {
            Dictionary<string, object> from = new Dictionary<string, object>();
            from.Add("name", "SENDER_NAME");
            from.Add("email", "SENDER_EMAIL@domain.com");
            ArrayList to = new ArrayList();
            Dictionary<string, object> elementto = new Dictionary<string, object>();
            elementto.Add("name", "Test email");
            elementto.Add("email", "test@test.com");
            to.Add(elementto);
            Dictionary<string, object> emaildata = new Dictionary<string, object>();
            emaildata.Add("html", "<b>Hello</b>");
            emaildata.Add("text", "Hello!");
            emaildata.Add("subject", "Send SMTP email");
            emaildata.Add("from", from);
            emaildata.Add("to", to);
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
    }
}
