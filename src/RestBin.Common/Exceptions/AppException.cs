using System;

namespace RestBin.Common.Exceptions
{
    public class AppException : Exception
    {
        /// <summary>
        /// init base for empty throwing 
        /// </summary>
        public AppException() : base()
        {

        }

        /// <summary>
        /// send message to base ctor
        /// </summary>
        /// <param name="message">Exp message</param>
        public AppException(string message) : base(message)
        {

        }

        /// <summary>
        /// Send message include with inner excetion
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public AppException(string message, Exception innerException) : base(message, innerException)
        {

        }

        public AppException(Exception exception) : base(String.Empty, exception)
        {

        }
    }
}
