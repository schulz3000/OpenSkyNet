using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace OpenSkyNet
{
    class OpenSkyTrack : IOpenSkyTrack
    {
        [JsonProperty("icao24")]
        public string Icao24 { get; set; }

        [JsonConverter(typeof(FloatUnixTimeConverter))]
        [JsonProperty("startTime")]
        public DateTime StartTime { get; set; }

        [JsonConverter(typeof(FloatUnixTimeConverter))]
        [JsonProperty("endTime")]
        public DateTime EndTime { get; set; }

        [JsonProperty("callsign")]
        public string CalllSign { get; set; }

        [JsonConverter(typeof(TrackPathArrayConverter))]
        [JsonProperty("path")]
        public IOpenSkyTrackPath[] Path { get; set; }
    }
}