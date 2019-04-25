using System;
using Checkout.CrossCutting.Core.Logging;

namespace Checkout.CrossCutting.Framework.Logging.Common
{
    public abstract class AbstractLogger : ILogger
    {
        #region abstract protected methods

        internal abstract void AddLog(LogLevel level, string message);

        internal abstract void AddLog(LogLevel level, string message, Exception exception);

        #endregion

        #region ILogger Members

        #region Logging Debug

        /// <summary>
        ///     Debugs the to string value of the specified object.
        /// </summary>
        /// <param name="obj">The object to log (to sting).</param>
        /// <exception cref="LoggingException">Failed to log debug.</exception>
        public void Debug(object obj)
        {
            if (obj != null)
                AddLog(LogLevel.Debug, obj.ToString());
        }


        /// <summary>
        ///     Debugs the specified message.
        /// </summary>
        /// <param name="message">The object to log (to sting).</param>
        /// <exception cref="LoggingException">Failed to log debug.</exception>
        public void Debug(string message)
        {
            AddLog(LogLevel.Debug, message);
        }

        /// <summary>
        ///     Log debug message
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="exception">Exception to write in debug message</param>
        /// <exception cref="LoggingException">Failed to log debug.</exception>
        public void Debug(string message, Exception exception)
        {
            AddLog(LogLevel.Debug, message, exception);
        }

        #endregion

        #region Logging Info

        /// <summary>
        ///     Log message information
        /// </summary>
        /// <param name="message">The information message to write</param>
        /// <exception cref="LoggingException">Failed to log info.</exception>
        public void Info(string message)
        {
            AddLog(LogLevel.Info, message);
        }

        #endregion

        #region Logging Warning

        /// <summary>
        ///     Log warning message
        /// </summary>
        /// <param name="message">The warning message to write</param>
        /// <exception cref="LoggingException">Failed to log warn.</exception>
        public void Warning(string message)
        {
            AddLog(LogLevel.Warning, message);
        }

        #endregion

        #region Logging Error

        /// <summary>
        ///     Log error message
        /// </summary>
        /// <param name="message">The error message to write</param>
        /// <exception cref="LoggingException">Failed to log error.</exception>
        public void Error(string message)
        {
            AddLog(LogLevel.Error, message);
        }

        /// <summary>
        ///     Log error message
        /// </summary>
        /// <param name="message">The error message to write</param>
        /// <param name="exception">The exception associated with this error</param>
        /// <exception cref="LoggingException">Failed to log error.</exception>
        public void Error(string message, Exception exception)
        {
            AddLog(LogLevel.Error, message, exception);
        }

        #endregion

        #region Logging Fatal

        /// <summary>
        ///     Log FATAL error
        /// </summary>
        /// <param name="message">The message of fatal error</param>
        /// <exception cref="LoggingException">Failed to log fatal.</exception>
        public void Fatal(string message)
        {
            AddLog(LogLevel.Fatal, message);
        }

        /// <summary>
        ///     log FATAL error
        /// </summary>
        /// <param name="message">The message of fatal error</param>
        /// <param name="exception">The exception to write in this fatal message</param>
        /// <exception cref="LoggingException">Failed to log fatal.</exception>
        public void Fatal(string message, Exception exception)
        {
            AddLog(LogLevel.Fatal, message, exception);
        }

        #endregion

        #endregion
    }
}