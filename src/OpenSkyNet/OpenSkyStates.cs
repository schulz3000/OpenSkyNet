using Newtonsoft.Json;

namespace OpenSkyNet
{
    class OpenSkyStates : IOpenSkyStates
    {
        [JsonProperty(PropertyName = "time")]
        public int TimeStamp { get; set; }
        [JsonConverter(typeof(StateVectorArrayJsonConverter))]
        public IStateVector[] States { get; set; }
    }
}
