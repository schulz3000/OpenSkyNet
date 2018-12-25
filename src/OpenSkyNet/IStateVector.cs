namespace OpenSkyNet
{
    /// <summary>
    /// state vector
    /// </summary>
    public interface IStateVector
    {
        /// <summary>
        /// Unique ICAO 24-bit address of the transponder in hex string representation.
        /// </summary>
        string Icao24 { get; }

        /// <summary>
        /// Callsign of the vehicle. Can be null if no callsign has been received.
        /// </summary>
        string CallSign { get; }

        /// <summary>
        /// Country name inferred from the ICAO 24-bit address.
        /// </summary>
        string OriginCountry { get; }

        /// <summary>
        /// Unix timestamp (seconds) for the last position update. Can be null if no position report was received by OpenSky within the past 15s.
        /// </summary>
        int? TimePosition { get; }

        /// <summary>
        /// Unix timestamp (seconds) for the last update in general. This field is updated for any new, valid message received from the transponder.
        /// </summary>
        int LastContact { get; }

        /// <summary>
        /// WGS-84 longitude in decimal degrees. Can be null.
        /// </summary>
        float? Longitude { get; }

        /// <summary>
        /// WGS-84 latitude in decimal degrees. Can be null.
        /// </summary>
        float? Latitude { get; }

        /// <summary>
        /// Barometric altitude in meters. Can be null.
        /// </summary>
        float? BaroAltitude { get; }

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
        float? TrueTrack { get; }

        /// <summary>
        /// Vertical rate in m/s. A positive value indicates that the airplane is climbing, a negative value indicates that it descends. Can be null.
        /// </summary>
        float? VerticalRate { get; }

        /// <summary>
        /// IDs of the receivers which contributed to this state vector. Is null if no filtering for sensor was used in the request.
        /// </summary>
        int[] Sensors { get; }

        /// <summary>
        /// Barometric or geometric altitude in meters. Can be null.
        /// </summary>
        float? GeoAltitude { get; }

        /// <summary>
        /// The transponder code aka Squawk. Can be null.
        /// </summary>
        string Squawk { get; }

        /// <summary>
        /// Whether flight status indicates special purpose indicator.
        /// </summary>
        bool Spi { get; }

        /// <summary>
        /// Origin of this state’s position: 0 = ADS-B, 1 = ASTERIX, 2 = MLAT
        /// </summary>
        int PositionSource { get; }
    }
}
