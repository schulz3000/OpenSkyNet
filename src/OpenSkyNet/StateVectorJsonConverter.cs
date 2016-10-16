using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace OpenSkyNet
{
    class StateVectorJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jsonArray = JArray.Load(reader);

            return new StateVector
            {
                Icao24 = jsonArray[0].Value<string>(),
                CallSign = jsonArray[1].Value<string>(),
                OriginCountry = jsonArray[2].Value<string>(),
                TimePosition = jsonArray[3].Value<float?>(),
                TimeVelocity = jsonArray[4].Value<float?>(),
                Longitude = jsonArray[5].Value<float?>(),
                Latitude = jsonArray[6].Value<float?>(),
                Altitude = jsonArray[7].Value<float?>(),
                OnGround = jsonArray[8].Value<bool>(),
                Velocity = jsonArray[9].Value<float?>(),
                Heading = jsonArray[10].Value<float?>(),
                VerticalRate = jsonArray[11].Value<float?>(),
                Sensors = jsonArray[12].Value<int[]>()
            };
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
