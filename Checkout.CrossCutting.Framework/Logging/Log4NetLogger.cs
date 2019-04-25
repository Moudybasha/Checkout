using System;
using Checkout.CrossCutting.Framework.Logging.Common;
using log4net;

namespace Checkout.CrossCutting.Framework.Logging
{
    public sealed class Log4NetLogger : AbstractLogger
    {
        #region Private Members

        private readonly ILog _logger;

        #endregion

        #region  Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="Log4NetLogger" /> class.
        /// </summary>
        /// <param name="log">The _log.</param>
        public Log4NetLogger(ILog log)
        {
            _logger = log ?? throw new ArgumentNullException(nameof(log));
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the log.
        /// </summary>
        /// <value>
        ///     The log.
        /// </value>
        public ILog Log => _logger;

        #endregion

        #region Private Methods

        internal override void AddLog(LogLevel level, string message)
        {
            try
            {
                switch (level)
                {
                    case LogLevel.Debug:
                        if (_logger.IsDebugEnabled)
                            _logger.Debug(message);
                        break;
                    case LogLevel.Info:
                        if (_logger.IsInfoEnabled)
                            _logger.Info(message);
                        break;
                    case LogLevel.Fatal:
                        if (_logger.IsFatalEnabled)
                            _logger.Fatal(message);
                        break;
                    case LogLevel.Error:
                        if (_logger.IsErrorEnabled)
                            _logger.Error(message);
                        break;
                    case LogLevel.Warning:
                        if (_logger.IsWarnEnabled)
                            _logger.Warn(message);
                        break;
                }
            }
            catch (Exception exp)
            {
                throw new LoggingException(string.Format("Failed to log {0}.", Enum.GetName(typeof (LogLevel), level)),
                    exp);
            }
        }


        internal override void AddLog(LogLevel level, string message, Exception exception)
        {
            try
            {
                switch (level)
                {
                    case LogLevel.Debug:
                        if (_logger.IsDebugEnabled)
                            _logger.Debug(message, exception);
                        break;
                    case LogLevel.Info:
                        if (_logger.IsInfoEnabled)
                            _logger.Info(message, exception);
                        break;
                    case LogLevel.Fatal:
                        if (_logger.IsFatalEnabled)
                            _logger.Fatal(message, exception);
                        break;
                    case LogLevel.Error:
                        if (_logger.IsErrorEnabled)
                            _logger.Error(message, exception);
                        break;
                    case LogLevel.Warning:
                        if (_logger.IsWarnEnabled)
                            _logger.Warn(message, exception);
                        break;
                }
            }
            catch (Exception exp)
            {
                throw new LoggingException(string.Format("Failed to log {0}.", Enum.GetName(typeof(LogLevel), level)),
                    exp);
            }
        }

        #endregion
    }
}