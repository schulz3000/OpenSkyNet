using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace OpenSkyNet
{
    /// <summary>
    /// Response
    /// </summary>
    [JsonConverter(typeof(InterfaceConverter<IOpenSkyStates, OpenSkyStates>))]
    public interface IOpenSkyStates
    {
        /// <summary>
        /// The time which the state vectors in this response are associated with. All vectors represent the state of a vehicle with the interval [time−1,time][time−1,time].
        /// </summary>  
        [JsonConverter(typeof(UnixDateTimeConverter))]
        [JsonProperty(PropertyName = "time")]
        DateTime Time { get; }

        /// <summary>
        /// The state vectors.
        /// </summary>  
        [JsonConverter(typeof(StateVectorArrayConverter))]
        IOpenSkyStateVector[] States { get; }
    }
}
