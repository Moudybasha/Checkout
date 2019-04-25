using System;
using System.Diagnostics;
using Checkout.CrossCutting.Framework.Logging.Common;

namespace Checkout.CrossCutting.Framework.Logging
{
    public sealed class TraceSourceLogger
        : AbstractLogger
    {
        #region Members

        private readonly TraceSource _source;

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the source.
        /// </summary>
        /// <value>
        ///     The source.
        /// </value>
        public TraceSource Source => _source;

        #endregion

        #region  Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="TraceSourceLogger" /> class.
        /// </summary>
        public TraceSourceLogger(TraceSource source)
        {
            // Create default source
            _source = source ?? throw new ArgumentNullException(nameof(source));
        }

        #endregion

        #region Private Methods

        internal override void AddLog(LogLevel level, string message)
        {
            try
            {
                switch (level)
                {
                    case LogLevel.Debug:
                        _source.TraceData(TraceEventType.Verbose, (int)TraceEventType.Verbose, message);
                        break;
                    case LogLevel.Info:
                        _source.TraceData(TraceEventType.Information, (int)TraceEventType.Information, message);
                        break;
                    case LogLevel.Fatal:
                        _source.TraceData(TraceEventType.Critical, (int)TraceEventType.Critical, message);
                        break;
                    case LogLevel.Error:
                        _source.TraceData(TraceEventType.Error, (int)TraceEventType.Error, message);
                        break;
                    case LogLevel.Warning:
                        _source.TraceData(TraceEventType.Warning, (int)TraceEventType.Warning, message);
                        break;
                }
            }
            catch (Exception exp)
            {
                throw new LoggingException($"Failed to log {Enum.GetName(typeof(LogLevel), level)}.",
                    exp);
            }
        }

        internal override void AddLog(LogLevel level, string message, Exception exp)
        {
            AddLog(level, string.Format("{0}{1}{2}{3}", message, Environment.NewLine, Environment.NewLine, exp));
        }

        #endregion
    }
}