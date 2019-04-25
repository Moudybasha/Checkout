using System;

namespace Checkout.CrossCutting.Framework.Logging.Common
{
    /// <summary>
    ///     Generic Logging Exception Class.
    /// </summary>
    public class LoggingException : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="LoggingException" /> class.
        /// </summary>
        public LoggingException()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="LoggingException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public LoggingException(string message)
            : base(message)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="LoggingException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        public LoggingException(string message, Exception ex)
            : base(message, ex)
        {
        }
    }
}