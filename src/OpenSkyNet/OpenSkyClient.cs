using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OpenSkyNet
{
    /// <summary>
    /// Client for OpenSky-Network API
    /// </summary>
    public class OpenSkyClient : Connection
    {
        readonly OpenSkyObservableManager manager;
        readonly CancellationTokenSource cts = new CancellationTokenSource();

        /// <summary>
        /// 
        /// </summary>
        public OpenSkyClient()
        {
            manager = new OpenSkyObservableManager(this, cts.Token);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public OpenSkyClient(string username, string password)
                    : base(username, password)
        {
            manager = new OpenSkyObservableManager(this, cts.Token);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="time">The time to get data from. Current time will be used if omitted.</param>
        /// <param name="icao24">One or more ICAO24 transponder addresses represented by a hex string (e.g. abc9f3). To filter multiple ICAO24 add them as array.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task<IOpenSkyStates> GetStatesAsync(DateTime time = default, string[] icao24 = null, CancellationToken token = default)
            => GetStatesBasicAsync("states/all", time.ToUnixTimestamp(), icao24, null, token);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="time">The time to get data from. Current time will be used if omitted.</param>
        /// <param name="icao24">One or more ICAO24 transponder addresses represented by a hex string (e.g. abc9f3). To filter multiple ICAO24 add them as array.</param>
        /// <param name="serials">Retrieve only states of a subset of your receivers. You can pass this argument several time to filter state of more than one of your receivers. In this case, the API returns all states of aircraft that are visible to at least one of the given receivers.</param>
        /// <param name="token"></param>
        /// <exception cref="OpenSkyNetException"></exception>
        /// <returns></returns>
        public Task<IOpenSkyStates> GetMyStatesAsync(DateTime time = default, string[] icao24 = null, int[] serials = null, CancellationToken token = default)
        {
            if (!HasCredentials)
                throw new OpenSkyNetException("Anonymous access of 'GetMyStatesAsync' not allowed");

            var timestamp = time.ToUnixTimestamp();

            return GetStatesBasicAsync("states/own", timestamp, icao24, serials, token);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task<IOpenSkyFlight[]> GetFlights(DateTime begin, DateTime end, CancellationToken token = default)
            => GetFlightsBasic(new OpenSkyFlightsRequest { Begin = begin, End = end }, token);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="icao24"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task<IOpenSkyFlight[]> GetFlightsByAircraft(string icao24, DateTime begin, DateTime end, CancellationToken token = default)
        {
            if (string.IsNullOrWhiteSpace(icao24))
                throw new ArgumentNullException(nameof(icao24));

            return GetFlightsBasic(new OpenSkyAircraftFlightsRequest { Icao24 = icao24, Begin = begin, End = end }, token);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="airportIcao24"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task<IOpenSkyFlight[]> GetArrivalsByAirport(string airportIcao24, DateTime begin, DateTime end, CancellationToken token = default)
            => GetFlightsByAirport(new OpenSkyArrivalFlightsRequest { Airport = airportIcao24, Begin = begin, End = end }, token);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="airportIcao24"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task<IOpenSkyFlight[]> GetDeparturesByAirport(string airportIcao24, DateTime begin, DateTime end, CancellationToken token = default)
            => GetFlightsByAirport(new OpenSkyDepartureFlightsRequest { Airport = airportIcao24, Begin = begin, End = end }, token);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="icao24"></param>
        /// <param name="time"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task<IOpenSkyTrack> GetTrackByAircraftAsync(string icao24, DateTime time = default, CancellationToken token = default)
        {
            if (string.IsNullOrWhiteSpace(icao24))
                throw new ArgumentNullException(nameof(icao24));

            var timestamp = time.ToUnixTimestamp();

            if (time < DateTime.Now.AddDays(-30) || time > DateTime.Now)
                timestamp = 0;

            var query = $"tracks/all?icao24={icao24}&time={timestamp}";

            return GetAsync<IOpenSkyTrack>(query, token);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="icao24"></param>
        /// <returns></returns>
        public IObservable<IOpenSkyStateVector> TrackFlight(string icao24) => manager.GetObservableFor(icao24);

        Task<IOpenSkyStates> GetStatesBasicAsync(string query, int timestamp, string[] icao24, int[] serials, CancellationToken token = default)
        {
            var dict = new Dictionary<string, object>();

            if (timestamp > -1)
                dict.Add("time", timestamp);

            if (icao24?.Length > 0)
            {
                for (int i = 0; i < icao24.Length; i++)
                {
                    dict.Add("icao24", icao24[i]);
                }
            }

            if (serials?.Length > 0)
            {
                for (int i = 0; i < serials.Length; i++)
                {
                    dict.Add("serials", serials[i]);
                }
            }

            query += QueryStringBuilder.Create(dict);

            return GetAsync<IOpenSkyStates>(query, token);
        }

        Task<IOpenSkyFlight[]> GetFlightsByAirport(OpenSkyAirportFlightsRequest request, CancellationToken token = default)
        {
            if (string.IsNullOrWhiteSpace(request.Airport))
                throw new ArgumentNullException(nameof(request.Airport));

            return GetFlightsBasic(request, token);
        }

        Task<IOpenSkyFlight[]> GetFlightsBasic(OpenSkyFlightsRequest request, CancellationToken token = default)
        {
            if (request.Begin > request.End)
                throw new OpenSkyNetException("end is before begin");

            if ((request.End - request.Begin).TotalHours > 2)
                throw new OpenSkyNetException("time range must not larger then two hours");

            var query = "flights/" + request.UrlPart;

            var dict = new Dictionary<string, object>();

            dict.Add("begin", request.Begin.ToUnixTimestamp());
            dict.Add("end", request.End.ToUnixTimestamp());

            if (request is OpenSkyAircraftFlightsRequest aircraftRequest)
            {
                dict.Add("icao24", aircraftRequest.Icao24.ToLower());
            }
            else if (request is OpenSkyAirportFlightsRequest airportRequest)
            {
                dict.Add("airport", airportRequest.Airport.ToUpper());
            }

            query += QueryStringBuilder.Create(dict);

            return GetAsync<IOpenSkyFlight[]>(query, token);
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnDispose() => cts.Cancel();
    }
}
