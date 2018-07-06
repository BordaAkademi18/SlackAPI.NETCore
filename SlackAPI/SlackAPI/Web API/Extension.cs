using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Newtonsoft.Json;

namespace SlackAPI
{
    public class JavascriptDateTimeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            decimal value = decimal.Parse(reader.Value.ToString(), CultureInfo.InvariantCulture);
            DateTime res = new DateTime(621355968000000000 + (long)(value * 10000000m)).ToLocalTime();
            System.Diagnostics.Debug.Assert(
                Decimal.Equals(
                    Decimal.Parse(res.ToProperTimeStamp()),
                    Decimal.Parse(reader.Value.ToString(), CultureInfo.InvariantCulture)),
                "Precision loss :(");
            return res;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((DateTime)value).Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
        }
    }

    public static class Extension
    {
        public static string ToProperTimeStamp(this DateTime that, bool toUTC = true)
        {
            if (toUTC)
            {
                return ((that.ToUniversalTime().Ticks - 621355968000000000m) / 10000000m).ToString("F6");
            }
            else
                return that.Subtract(new DateTime(1970, 1, 1)).TotalSeconds.ToString();
        }

        public static string BuildString(List<string> items)
        {
            return String.Join(",", items);
        }        
    }
}
