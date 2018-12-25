using System;

namespace OpenSkyNet
{
    class OpenSkyFlight : IOpenSkyFlight
    {
        public string Icao24 { get; set; }

        public DateTime FirstSeen { get; set; }

        public string EstDepartureAirport { get; set; }

        public DateTime LastSeen { get; set; }

        public string EstArrivalAirport { get; set; }

        public string CallSign { get; set; }

        public int EstDepartureAirportHorizDistance { get; set; }

        public int EstDepartureAirportVertDistance { get; set; }

        public int EstArrivalAirportHorizDistance { get; set; }

        public int EstArrivalAirportVertDistance { get; set; }

        public int DepartureAirportCandidatesCount { get; set; }

        public int ArrivalAirportCandidatesCount { get; set; }
    }
}