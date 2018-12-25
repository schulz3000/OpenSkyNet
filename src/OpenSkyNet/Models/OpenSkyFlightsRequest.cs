using System;

namespace OpenSkyNet
{
    class OpenSkyFlightsRequest
    {
        public virtual string UrlPart => "all";

        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
    }

    class OpenSkyAircraftFlightsRequest: OpenSkyFlightsRequest
    {
        public override string UrlPart => "aircraft";

        public string Icao24 { get; set; }
    }

    abstract class OpenSkyAirportFlightsRequest : OpenSkyFlightsRequest
    {
        public string Airport { get; set; }
    }

    class OpenSkyArrivalFlightsRequest : OpenSkyAirportFlightsRequest
    {
        public override string UrlPart => "arrival";
    }

    class OpenSkyDepartureFlightsRequest : OpenSkyAirportFlightsRequest
    {
        public override string UrlPart => "departure";
    }
}
