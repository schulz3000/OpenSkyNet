using System;

namespace OpenSkyNet
{
    class OpenSkyTrack : IOpenSkyTrack
    {
        public string Icao24 { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string CalllSign { get; set; }

        public IOpenSkyTrackPath[] Path { get; set; }
    }
}