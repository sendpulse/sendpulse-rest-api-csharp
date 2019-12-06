using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Sendpulse_rest_api.restapi.Converters
{
    public class ViberDateTimeConverter: DateTimeConverterBase
    {

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            DateTime dt = (DateTime) value;

            if (dt == DateTime.Now || dt < DateTime.Now)
            {
                writer.WriteToken(JsonToken.String, "now");
            }
            else
            {
                writer.WriteToken(JsonToken.String, dt.ToString("yyyy-MM-dd HH:mm:ss"));
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}