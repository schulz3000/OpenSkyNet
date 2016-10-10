using System;

namespace OpenSkyNet
{
    /// <summary>
    /// 
    /// </summary>
    public class OpenSkyNetException : Exception
    {       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public OpenSkyNetException(string message) 
            : base(message)
        {
           
        }
    }
}
