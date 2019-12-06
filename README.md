# sendpulse-rest-api-csharp
A simple SendPulse REST client library and example for C#. 

### Examples

##### sendViberCampaign
- Method accepts one argument of type ```ViberCampaign```.

Example of viber campaign:

```c#
sendpulse.sendViberCampaign(new ViberCampaign(){
    Name = "Campaign name",
    Message = "Hello, World!",
    Recipients = new []{"+000123456789", "+000123498765"}, // By default string[]{}
    // Recipients OR AddressBook
    AddressBook = 1, // By default 0
    MessageType = ViberCampaignMessageType.Transactional, // By default - ViberCampaigMessageType.Marketing
    SenderId = 1, // By default 0
    SendDate = DateTime.Parse("2020-01-01 00:00:00"), // By default null. If specified time is less than now or null specified - it will be assumed as now
    MessageLiveTime = 120, // By default 60
    Additional = new ViberCampaignAdditional() // By default null
    {
        Button = new ViberCampaignButton() // By default null
        {
            Text = "Button",
            Link = "https://example.com"
        },
        Image =  new ViberCampaignImage() // By default null
        {
            Link = "https://example.com/image.jpg"
        },
        ResendSms = new ViberCampaignResendSms() // By default null
        {
            Status = true,
            Text = "Text",
            SenderName = "Sender name"
        }
    }
});
```

In request it will appear as:
```json
{
  "task_name":"Campaign name",
  "recipients":["+000123456789","+000123498765"],
  "address_book":1,
  "message":"Hello, World!",
  "message_live_time":120,
  "sender_id":1,
  "send_date":"2020-01-01 00:00:00",
  "message_type":3,
  "additional": {
    "button": {
      "text":"Button",
      "link":"https://example.com"
    },
    "image": {
      "link":"https://example.com/image.jpg"
    },
    "resend_sms": {
      "status":true,
      "sms_text":"Text",
      "sms_sender_name":"Sender name"
    }
  }
}
```
