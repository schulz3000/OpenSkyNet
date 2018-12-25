using System;

namespace OpenSkyNet
{
    /// <summary>
    /// Aircraft Track
    /// </summary>
    public interface IOpenSkyTrack
    {
        /// <summary>
        /// Unique ICAO 24-bit address of the transponder in lower case hex string representation.
        /// </summary>
        string Icao24 { get; }

        /// <summary>
        /// Time of the first waypoint in seconds since epoch (Unix time).
        /// </summary>
        DateTime StartTime { get; }

        /// <summary>
        /// Time of the last waypoint in seconds since epoch (Unix time).
        /// </summary>
        DateTime EndTime { get; }

        /// <summary>
        /// Callsign (8 characters) that holds for the whole track. Can be null.
        /// </summary>
        string CalllSign { get; }

        /// <summary>
        /// Waypoints of the trajectory
        /// </summary>
        IOpenSkyTrackPath[] Path { get; }
    }
}