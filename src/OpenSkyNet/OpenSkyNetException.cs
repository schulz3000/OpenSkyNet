using System;
using System.Runtime.Serialization;

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
        public OpenSkyNetException()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public OpenSkyNetException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public OpenSkyNetException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }

#if !COREFX
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected OpenSkyNetException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
#endif
    }
}
