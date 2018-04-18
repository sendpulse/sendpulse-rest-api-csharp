using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sendpulse_rest_api.restapi
{
    interface SendpulseInterface
    {
        /// <summary>
        /// Get list of address books
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        Dictionary<string, object> listAddressBooks(int limit, int offset);
        /// <summary>
        /// Get book info
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Dictionary<string, object> getBookInfo(int id);
        /// <summary>
        /// Get list pf emails from book
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Dictionary<string, object> getEmailsFromBook(int id);
        /// <summary>
        /// Remove address book
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Dictionary<string, object> removeAddressBook(int id);
        /// <summary>
        /// Edit address book name
        /// </summary>
        /// <param name="id">String book id</param>
        /// <param name="newname">String book new name</param>
        /// <returns></returns>
        Dictionary<string, object> editAddressBook(int id, string newname);
        /// <summary>
        /// Create new address book
        /// </summary>
        /// <param name="bookName"></param>
        /// <returns></returns>
        Dictionary<string, object> createAddressBook(string bookName);
        /// <summary>
        /// Add new emails to book
        /// </summary>
        /// <param name="bookId">int book id</param>
        /// <param name="emails">A serialized array of emails</param>
        /// <returns></returns>
        Dictionary<string, object> addEmails(int bookId, string emails);
        /// <summary>
        /// Remove emails from book
        /// </summary>
        /// <param name="bookId">int book id</param>
        /// <param name="emails">String A serialized array of emails</param>
        /// <returns></returns>
        Dictionary<string, object> removeEmails(int bookId, string emails);
        /// <summary>
        /// Get information about email from book
        /// </summary>
        /// <param name="bookId"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        Dictionary<string, object> getEmailInfo(int bookId, string email);
        /// <summary>
        /// Calculate cost of the campaign based on address book
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        Dictionary<string, object> campaignCost(int bookId);
        /// <summary>
        /// Get list of campaigns
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        Dictionary<string, object> listCampaigns(int limit, int offset);
        /// <summary>
        /// Get information about campaign
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Dictionary<string, object> getCampaignInfo(int id);
        /// <summary>
        /// Get campaign statistic by countries
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Dictionary<string, object> campaignStatByCountries(int id);
        /// <summary>
        /// Get campaign statistic by referrals
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Dictionary<string, object> campaignStatByReferrals(int id);
        /// <summary>
        /// Create new campaign
        /// </summary>
        /// <param name="senderName"></param>
        /// <param name="senderEmail"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="bookId"></param>
        /// <param name="name"></param>
        /// <param name="attachments"></param>
        /// <returns></returns>
        Dictionary<string, object> createCampaign(string senderName, string senderEmail, string subject, string body, int bookId, string name, string send_date = "", string attachments = "");
        /// <summary>
        /// Cancel campaign
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Dictionary<string, object> cancelCampaign(int id);
        /// <summary>
        /// Get list of allowed senders
        /// </summary>
        /// <returns></returns>
        Dictionary<string, object> listSenders();
        /// <summary>
        /// Add new sender
        /// </summary>
        /// <param name="senderName"></param>
        /// <param name="senderEmail"></param>
        /// <returns></returns>
        Dictionary<string, object> addSender(string senderName, string senderEmail);
        /// <summary>
        /// Remove sender
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Dictionary<string, object> removeSender(string email);
        /// <summary>
        /// Activate sender using code from mail
        /// </summary>
        /// <param name="email"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        Dictionary<string, object> activateSender(string email, string code);
        /// <summary>
        /// Send mail with activation code on sender email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Dictionary<string, object> getSenderActivationMail(string email);
        /// <summary>
        /// Get global information about email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Dictionary<string, object> getEmailGlobalInfo(string email);
        /// <summary>
        /// Remove email address from all books
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Dictionary<string, object> removeEmailFromAllBooks(string email);
        /// <summary>
        /// Get statistic for email by all campaigns
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Dictionary<string, object> emailStatByCampaigns(string email);
        /// <summary>
        /// Show emails from blacklist
        /// </summary>
        /// <returns></returns>
        Dictionary<string, object> getBlackList();
        /// <summary>
        /// Add email address to blacklist
        /// </summary>
        /// <param name="emails"></param>
        /// <returns></returns>
        Dictionary<string, object> addToBlackList(string emails);
        /// <summary>
        /// Remove email address from blacklist
        /// </summary>
        /// <param name="emails"></param>
        /// <returns></returns>
        Dictionary<string, object> removeFromBlackList(string emails);
        /// <summary>
        /// Return user balance
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        Dictionary<string, object> getBalance(string currency);
        /// <summary>
        /// Send mail using SMTP
        /// </summary>
        /// <param name="emaildata"></param>
        /// <returns></returns>
        Dictionary<string, object> smtpSendMail(Dictionary<string, object> emaildata);
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
        Dictionary<string, object> smtpListEmails(int limit, int offset, string fromDate, string toDate, string sender, string recipient);
        /// <summary>
        /// Get information about email by his id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Dictionary<string, object> smtpGetEmailInfoById(string id);
        /// <summary>
        /// Unsubscribe emails using SMTP
        /// </summary>
        /// <param name="emails"></param>
        /// <returns></returns>
        Dictionary<string, object> smtpUnsubscribeEmails(string emails);
        /// <summary>
        /// Remove emails from unsubscribe list using SMTP
        /// </summary>
        /// <param name="emails"></param>
        /// <returns></returns>
        Dictionary<string, object> smtpRemoveFromUnsubscribe(string emails);
        /// <summary>
        /// Get list of allowed IPs using SMTP
        /// </summary>
        /// <returns></returns>
        Dictionary<string, object> smtpListIP();
        /// <summary>
        /// Get list of allowed domains using SMTP
        /// </summary>
        /// <returns></returns>
        Dictionary<string, object> smtpListAllowedDomains();
        /// <summary>
        /// Add domain using SMTP
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Dictionary<string, object> smtpAddDomain(string email);
        /// <summary>
        /// Send confirm mail to verify new domain
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Dictionary<string, object> smtpVerifyDomain(string email);
        /// <summary>
        /// Get list of push campaigns
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        Dictionary<string, object> pushListCampaigns(int limit, int offset);
        /// <summary>
        /// Get push campaigns info
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Dictionary<string, object> pushCampaignInfo(int id);
        /// <summary>
        /// Get amount of websites
        /// </summary>
        /// <returns></returns>
        Dictionary<string, object> pushCountWebsites();
        /// <summary>
        /// Get list of websites
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        Dictionary<string, object> pushListWebsites(int limit, int offset);
        /// <summary>
        /// Get list of all variables for website
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Dictionary<string, object> pushListWebsiteVariables(int id);
        /// <summary>
        /// Get list of subscriptions for the website
        /// </summary>
        /// <param name="id"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        Dictionary<string, object> pushListWebsiteSubscriptions(int id, int limit, int offset);
        /// <summary>
        /// Get amount of subscriptions for the site
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Dictionary<string, object> pushCountWebsiteSubscriptions(int id);
        /// <summary>
        /// Set state for subscription
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        Dictionary<string, object> pushSetSubscriptionState(int id, int state);
        /// <summary>
        /// Create new push campaign
        /// </summary>
        /// <param name="taskinfo"></param>
        /// <param name="additionalParams"></param>
        /// <returns></returns>
        Dictionary<string, object> createPushTask(Dictionary<string, object> taskinfo, Dictionary<string, object> additionalParams);
        /// <summary>
        /// Add phones to address book.
        /// </summary>
        /// <returns>The phones.</returns>
        /// <param name="bookId">Book identifier.</param>
        /// <param name="phones">Phones.</param>
        Dictionary<string, object> addPhones(int bookId, string phones);
        /// <summary>
        /// Remove phones from address book.
        /// </summary>
        /// <returns>The phones.</returns>
        /// <param name="bookId">Book identifier.</param>
        /// <param name="phones">Phones.</param>
        Dictionary<string, object> removePhones(int bookId, string phones);
        /// <summary>
        /// Update phones.
        /// </summary>
        /// <returns>The phones.</returns>
        /// <param name="bookId">Book identifier.</param>
        /// <param name="phones">Phones.</param>
        /// <param name="variables">Variables.</param>
        Dictionary<string, object> updatePhones(int bookId, string phones, string variables);
        /// <summary>
        /// Get the phone number info.
        /// </summary>
        /// <returns>The phone info.</returns>
        /// <param name="bookId">Book identifier.</param>
        /// <param name="phoneNumber">Phone number.</param>
        Dictionary<string, object> getPhoneInfo(int bookId, string phoneNumber);
        /// <summary>
        /// Add phones to black list.
        /// </summary>
        /// <returns>The phone to black list.</returns>
        /// <param name="phones">Phones.</param>
        /// <param name="description">Description.</param>
        Dictionary<string, object> addPhonesToBlackList(string phones, string description);
        /// <summary>
        /// Remove phones from black list.
        /// </summary>
        /// <returns>The phones from black list.</returns>
        /// <param name="phones">Phones.</param>
        Dictionary<string, object> removePhonesFromBlackList(string phones);
        /// <summary>
        /// Get black list of phone numbers.
        /// </summary>
        /// <returns>The black list phones.</returns>
        Dictionary<string, object> getBlackListPhones();
        /// <summary>
        /// Retrieving information of telephone numbers in the blacklist
        /// </summary>
        /// <returns>The phones info in black list.</returns>
        Dictionary<string, object> getPhonesInfoInBlackList(string phones);
        /// <summary>
        /// Send the sms campaign.
        /// </summary>
        /// <returns>The sms campaign.</returns>
        /// <param name="bookId">Book identifier.</param>
        /// <param name="body">Body.</param>
        /// <param name="transliterate">Transliterate.</param>
        /// <param name="sender">Sender.</param>
        /// <param name="date">Date.</param>
        Dictionary<string, object> sendSmsCampaign(int bookId, string body, int transliterate = 1, string sender = "", string date = "");
        /// <summary>
        /// Send sms campaign by phones list.
        /// </summary>
        /// <returns>The sms campaign by phones.</returns>
        /// <param name="phones">Phones.</param>
        /// <param name="body">Body.</param>
        /// <param name="transliterate">Transliterate.</param>
        /// <param name="sender">Sender.</param>
        /// <param name="date">Date.</param>
        Dictionary<string, object> sendSmsCampaignByPhones(string phones, string body, int transliterate = 1, string sender = "", string date = "");
        /// <summary>
        /// Get sms campaigns list.
        /// </summary>
        /// <returns>The sms campaigns list.</returns>
        /// <param name="dateFrom">Date from.</param>
        /// <param name="dateTo">Date to.</param>
        Dictionary<string, object> getSmsCampaignsList(string dateFrom, string dateTo);
        /// <summary>
        /// Get sms campaign info.
        /// </summary>
        /// <returns>The sms campaign info.</returns>
        /// <param name="id">Identifier.</param>
        Dictionary<string, object> getSmsCampaignInfo(int id);
        /// <summary>
        /// Cancel sms campaign.
        /// </summary>
        /// <returns>The sms campaign.</returns>
        /// <param name="id">Identifier.</param>
        Dictionary<string, object> cancelSmsCampaign(int id);
        /// <summary>
        /// Get sms campaign cost.
        /// </summary>
        /// <returns>The sms campaign cost.</returns>
        /// <param name="body">Body.</param>
        /// <param name="sender">Sender.</param>
        /// <param name="addressBookId">Address book identifier.</param>
        /// <param name="phones">Phones.</param>
        Dictionary<string, object> getSmsCampaignCost(string body, string sender, int addressBookId = 0, string phones = "");
        /// <summary>
        /// Delete sms campaign.
        /// </summary>
        /// <returns>The sms campaign.</returns>
        /// <param name="id">Identifier.</param>
        Dictionary<string, object> deleteSmsCampaign(int id);
        /// <summary>
        /// Add phones to addreess book.
        /// </summary>
        /// <returns>The phones to addreess book.</returns>
        /// <param name="addressBookId">Address book identifier.</param>
        /// <param name="phones">Phones.</param>
        Dictionary<string, object> addPhonesToAddreessBook(int addressBookId, string phones);
        /// <summary>
        /// Send viber campaign.
        /// </summary>
        /// <returns>The viber campaign.</returns>
        /// <param name="recipients">Recipients.</param>
        /// <param name="addressBookId">Address book identifier.</param>
        /// <param name="message">Message.</param>
        /// <param name="senderId">Sender identifier.</param>
        /// <param name="additional">Additional identifier.</param>
        /// <param name="messageLiveTime">Message live time.</param>
        /// <param name="sendDate">Send date.</param>
        Dictionary<string, object> sendViberCampaign(string recipients,int addressBookId,string message,int senderId,string additional,int messageLiveTime = 60,string sendDate = "now");
        /// <summary>
        /// Get viber senders list.
        /// </summary>
        /// <returns>The viber senders.</returns>
        Dictionary<string, object> getViberSenders();
        /// <summary>
        /// Get viber tasks list.
        /// </summary>
        /// <returns>The viber tasks list.</returns>
        /// <param name="limit">Limit.</param>
        /// <param name="offset">Offset.</param>
        Dictionary<string, object> getViberTasksList(int limit = 100, int offset = 0);
        /// <summary>
        /// Get viber campaign statistic.
        /// </summary>
        /// <returns>The viber campaign stat.</returns>
        /// <param name="id">Identifier.</param>
        Dictionary<string, object> getViberCampaignStat(int id);
        /// <summary>
        /// Get the viber sender info.
        /// </summary>
        /// <returns>The viber sender.</returns>
        /// <param name="id">Identifier.</param>
        Dictionary<string, object> getViberSender(int id);
        /// <summary>
        /// Get viber task recipients.
        /// </summary>
        /// <returns>The viber task recipients.</returns>
        /// <param name="id">Identifier.</param>
        Dictionary<string, object> getViberTaskRecipients(int id);
    }
}
