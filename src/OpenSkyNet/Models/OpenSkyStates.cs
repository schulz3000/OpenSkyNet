using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace OpenSkyNet
{
    class OpenSkyStates : IOpenSkyStates
    {        
        public DateTime Time { get; set; }
        
        public IOpenSkyStateVector[] States { get; set; }
    }
}
