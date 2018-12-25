using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace OpenSkyNet
{
    /// <summary>
    /// Flight Response
    /// </summary>
    [JsonConverter(typeof(InterfaceConverter<IOpenSkyFlight, OpenSkyFlight>))]
    public interface IOpenSkyFlight
    {
        /// <summary>
        /// Unique ICAO 24-bit address of the transponder in hex string representation. All letters are lower case.
        /// </summary>
        string Icao24 { get; }

        /// <summary>
        /// Estimated time of departure for the flight as Unix time (seconds since epoch).
        /// </summary>
        [JsonConverter(typeof(UnixDateTimeConverter))]
        DateTime FirstSeen { get; }

        /// <summary>
        /// ICAO code of the estimated departure airport. Can be null if the airport could not be identified.
        /// </summary>
        string EstDepartureAirport { get; }

        /// <summary>
        /// Estimated time of arrival for the flight as Unix time (seconds since epoch)
        /// </summary>
        [JsonConverter(typeof(UnixDateTimeConverter))]
        DateTime LastSeen { get; }

        /// <summary>
        /// ICAO code of the estimated arrival airport. Can be null if the airport could not be identified.
        /// </summary>
        string EstArrivalAirport { get; }

        /// <summary>
        /// Callsign of the vehicle (8 chars). Can be null if no callsign has been received. If the vehicle transmits multiple callsigns during the flight, we take the one seen most frequently
        /// </summary>
        string CallSign { get; }

        /// <summary>
        /// Horizontal distance of the last received airborne position to the estimated departure airport in meters
        /// </summary>
        int EstDepartureAirportHorizDistance {get;}

        /// <summary>
        /// Vertical distance of the last received airborne position to the estimated departure airport in meters
        /// </summary>
        int EstDepartureAirportVertDistance { get; }

        /// <summary>
        /// Horizontal distance of the last received airborne position to the estimated arrival airport in meters
        /// </summary>
        int EstArrivalAirportHorizDistance { get; }

        /// <summary>
        /// Vertical distance of the last received airborne position to the estimated arrival airport in meters
        /// </summary>
        int EstArrivalAirportVertDistance { get; }

        /// <summary>
        /// Number of other possible departure airports. These are airports in short distance to estDepartureAirport.
        /// </summary>
        int DepartureAirportCandidatesCount { get; }

        /// <summary>
        /// Number of other possible departure airports. These are airports in short distance to estArrivalAirport.
        /// </summary>
        int ArrivalAirportCandidatesCount { get; }
    }
}