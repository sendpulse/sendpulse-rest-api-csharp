using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Sendpulse_rest_api.restapi.Converters;

namespace Sendpulse_rest_api.restapi
{

    public class ViberCampaignButton
    {
        [JsonProperty("text")]
        public string Text;
        [JsonProperty("link")]
        public string Link;
    }

    public class ViberCampaignImage
    {
        [JsonProperty("link")]
        public string Link;
    }

    public class ViberCampaignResendSms
    {
        [JsonProperty("status")]
        public bool Status;

        [JsonProperty("sms_text")]
        public string Text;

        [JsonProperty("sms_sender_name")]
        public string SenderName;
    }

    public enum ViberCampaignMessageType
    {
        Marketing = 2,
        Transactional = 3
    }

    public class ViberCampaignAdditional
    {
        [JsonProperty("button")]
        public ViberCampaignButton Button = null;

        [JsonProperty("image")]
        public ViberCampaignImage Image = null;

        [JsonProperty("resend_sms")]
        public ViberCampaignResendSms ResendSms = null;
    }
    
    public class ViberCampaign
    {
        [JsonProperty("task_name")]
        public string Name;
        
        [JsonProperty("recipients")]
        public string[] Recipients = new string[] { };

        [JsonProperty("address_book")]
        public uint AddressBook = 0;

        [JsonProperty("message")]
        public string Message = "";

        [JsonProperty("message_live_time")]
        public uint MessageLiveTime = 60;

        [JsonProperty("sender_id")]
        public uint SenderId = 0;

        [JsonProperty("send_date")]
        [JsonConverter(typeof(ViberDateTimeConverter))]
        public DateTime SendDate = DateTime.Now;

        [JsonProperty("message_type")]
        public ViberCampaignMessageType MessageType = ViberCampaignMessageType.Marketing;

        [JsonProperty("additional")]
        public ViberCampaignAdditional Additional = null;
    }
    
}