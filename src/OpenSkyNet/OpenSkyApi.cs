using System.Threading.Tasks;
using System.Linq;

namespace OpenSkyNet
{
    /// <summary>
    /// 
    /// </summary>
    public class OpenSkyApi : Connection
    {
        /// <summary>
        /// 
        /// </summary>
        public OpenSkyApi()            
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public OpenSkyApi(string username, string password)
                    : base(username, password)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timestamp">The time in seconds since epoch (Unix time stamp to retrieve states for. Current time will be used if omitted.</param>
        /// <param name="icao24">One or more ICAO24 transponder addresses represented by a hex string (e.g. abc9f3). To filter multiple ICAO24 add them as array.</param>
        /// <returns></returns>
        public async Task<IOpenSkyStates> GetStatesAsync(int timestamp=-1, string[] icao24=null)
        {
           return await GetStatesBasicAsync("states/all", timestamp,icao24,null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timestamp">The time in seconds since epoch (Unix time stamp to retrieve states for. Current time will be used if omitted.</param>
        /// <param name="icao24">One or more ICAO24 transponder addresses represented by a hex string (e.g. abc9f3). To filter multiple ICAO24 add them as array.</param>
        /// <param name="serials">Retrieve only states of a subset of your receivers. You can pass this argument several time to filter state of more than one of your receivers. In this case, the API returns all states of aircraft that are visible to at least one of the given receivers.</param>
        /// <exception cref="OpenSkyNetException"></exception>
        /// <returns></returns>
        public async Task<IOpenSkyStates> GetMyStatesAsync(int timestamp=-1, string[] icao24=null, int[] serials=null)
        {
            if (!HasCredentials)
                throw new OpenSkyNetException("Anonymous access of 'GetMyStatesAsync' not allowed");

            return await GetStatesBasicAsync("states/own", timestamp, icao24,serials);
        }

        async Task<IOpenSkyStates> GetStatesBasicAsync(string query, int timestamp, string[] icao24, int[] serials)
        {
            if (timestamp > -1)
                query += "?time=" + timestamp + "&";

            if (icao24!=null && icao24.Any())
            {
                if (query[query.Length - 1] != '&')
                    query += "?";

                foreach (var item in icao24)
                    query += "icao24=" + item + "&";
            }

            if(serials!=null && serials.Any())
            {
                if (query[query.Length - 1] != '&')
                    query += "?";

                foreach (var item in icao24)
                    query += "icao24=" + item + "&";
            }

            return await GetAsync<OpenSkyStates>(query);
        }        
    }
}
