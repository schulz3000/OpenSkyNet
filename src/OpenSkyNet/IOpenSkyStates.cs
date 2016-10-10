namespace OpenSkyNet
{
    /// <summary>
    /// Response
    /// </summary>
    public interface IOpenSkyStates
    {
        /// <summary>
        /// The time which the state vectors in this response are associated with. All vectors represent the state of a vehicle with the interval [time−1,time][time−1,time].
        /// </summary>
        int TimeStamp { get; }
        /// <summary>
        /// The state vectors.
        /// </summary>    
        IStateVector[] States { get; }
    }
}
