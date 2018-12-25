using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace OpenSkyNet
{
    class FloatUnixTimeConverter : DateTimeConverterBase
    {
        public override bool CanWrite => false;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var parsed = (double)reader.Value;
            var second = (int)parsed;
            return second.FromUnixTimestamp();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            => throw new NotImplementedException();
    }
}
