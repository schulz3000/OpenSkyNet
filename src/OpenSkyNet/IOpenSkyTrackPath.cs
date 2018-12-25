using System;

namespace OpenSkyNet
{
    /// <summary>
    /// Track Path
    /// </summary>
    public interface IOpenSkyTrackPath
    {
        /// <summary>
        /// Time which the given waypoint is associated with in seconds since epoch (Unix time).
        /// </summary>
        DateTime Time { get; }

        /// <summary>
        /// WGS-84 latitude in decimal degrees. Can be null.
        /// </summary>
        float? Latitude { get; }

        /// <summary>
        /// WGS-84 longitude in decimal degrees. Can be null.
        /// </summary>
        float? Longitude { get; }

        /// <summary>
        /// Barometric altitude in meters. Can be null.
        /// </summary>
        float? BaroAltitude { get; }

        /// <summary>
        /// True track in decimal degrees clockwise from north (north=0°). Can be null.
        /// </summary>
        float? TrueTrack { get; }

        /// <summary>
        /// Boolean value which indicates if the position was retrieved from a surface position report.
        /// </summary>
        bool OnGround { get; }
    }
}