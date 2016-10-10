namespace OpenSkyNet
{
    /// <summary>
    /// 
    /// </summary>
    public interface IStateVector
    {
        /// <summary>
        /// Unique ICAO 24-bit address of the transponder in hex string representation.
        /// </summary>
        string Icao24 { get; }
        /// <summary>
        /// Callsign of the vehicle (8 chars). Can be null if no callsign has been received.
        /// </summary>
        string CallSign { get; }
        /// <summary>
        /// Country name inferred from the ICAO 24-bit address.
        /// </summary>
        string OriginCountry { get; }
        /// <summary>
        /// Unix timestamp (seconds) for the last position update. Can be null if no position report was received by OpenSky within the past 15s.
        /// </summary>
        float? TimePosition { get; }
        /// <summary>
        /// Unix timestamp (seconds) for the last velocity update. Can be null if no velocity report was received by OpenSky within the past 15s.
        /// </summary>
        float? TimeVelocity { get; }
        /// <summary>
        /// WGS-84 longitude in decimal degrees. Can be null.
        /// </summary>
        float? Longitude { get; }
        /// <summary>
        /// WGS-84 latitude in decimal degrees. Can be null.
        /// </summary>
        float? Latitude { get; }
        /// <summary>
        /// Barometric or geometric altitude in meters. Can be null.
        /// </summary>
        float? Altitude { get; }
        /// <summary>
        /// Boolean value which indicates if the position was retrieved from a surface position report.
        /// </summary>
        bool OnGround { get; }
        /// <summary>
        /// Velocity over ground in m/s. Can be null.
        /// </summary>
        float? Velocity { get; }
        /// <summary>
        /// Heading in decimal degrees clockwise from north (i.e. north=0°). Can be null.
        /// </summary>
        float? Heading { get; }
        /// <summary>
        /// Vertical rate in m/s. A positive value indicates that the airplane is climbing, a negative value indicates that it descends. Can be null.
        /// </summary>
        float? VerticalRate { get; }
        /// <summary>
        /// IDs of the receivers which contributed to this state vector. Is null if no filtering for sensor was used in the request.
        /// </summary>
        int[] Sensors { get; }
    }
}
