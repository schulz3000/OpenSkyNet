using Newtonsoft.Json;
using System;

namespace OpenSkyNet
{
    class StateVectorArrayJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
           if(reader.TokenType== JsonToken.StartArray)
            {
                return serializer.Deserialize<StateVector[]>(reader);
            }

           return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
