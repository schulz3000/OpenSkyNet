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
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="time">The time to get data from. Current time will be used if omitted.</param>
        /// <param name="icao24">One or more ICAO24 transponder addresses represented by a hex string (e.g. abc9f3). To filter multiple ICAO24 add them as array.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task<IOpenSkyStates> GetStatesAsync(DateTime time = default, string[] icao24 = null, CancellationToken token = default)
        {
            var timestamp = time.ToUnixTimestamp();

            return GetStatesBasicAsync("states/all", timestamp, icao24, null, token);
        }

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
        /// <param name="icao24"></param>
        /// <param name="time"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<IOpenSkyTrack> GetTrackByAircraft(string icao24, DateTime time = default, CancellationToken token = default)
        {
            if (string.IsNullOrWhiteSpace(icao24))
                throw new ArgumentNullException(nameof(icao24));

            var timestamp = time.ToUnixTimestamp();

            if (time < DateTime.Now.AddDays(-30) || time > DateTime.Now)
                timestamp = 0;

            var query = $"?icao24={icao24}&time={timestamp}";

            return await GetAsync<OpenSkyTrack>(query, token).ConfigureAwait(false) as IOpenSkyTrack;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="icao24"></param>
        /// <returns></returns>
        public IObservable<IStateVector> TrackFlight(string icao24) => manager.GetObservableFor(icao24);       

        async Task<IOpenSkyStates> GetStatesBasicAsync(string query, int timestamp, string[] icao24, int[] serials, CancellationToken token = default)
        {
            var dict = new Dictionary<string, string>();

            if (timestamp > -1)
                dict.Add("time", timestamp.ToString());

            if (icao24?.Length > 0)
            {
                for (int i = 0; i < icao24.Length; i++)
                {
                    dict.Add("icao24", icao24[i]);
                }
            }

            if (serials?.Length > 0)
            {
                for (int i = 0; i < icao24.Length; i++)
                {
                    dict.Add("icao24", icao24[i]);
                }
            }

            if (dict.Count > 0)
            {
                query += "?";
                foreach (var key in dict.Keys)
                {
                    query += $"{key}={dict[key]}&";
                }

                query.TrimEnd('&');
            }

            return await GetAsync<OpenSkyStates>(query, token).ConfigureAwait(false) as IOpenSkyStates;
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnDispose() => cts.Cancel();
    }
}
