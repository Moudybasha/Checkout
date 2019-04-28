using System;

namespace Checkout.CrossCutting.Core.Logging
{
    /// <summary>
    ///     Log Factory
    /// </summary>
    public static class LoggerFactory
    {
        #region Members

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the current log factory.
        /// </summary>
        /// <value>
        ///     The current log factory.
        /// </value>
        public static ILoggerFactory CurrentLogFactory { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Sets the current.
        /// </summary>
        /// <param name="logFactory">The log factory.</param>
        /// <exception cref="System.ArgumentNullException">logFactory</exception>
        public static void SetCurrent(ILoggerFactory logFactory)
        {
            CurrentLogFactory = logFactory ?? throw new ArgumentNullException(nameof(logFactory));
        }

        /// <summary>
        ///     Creates the logger.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException">
        ///     CurrentLogFactory is null, You must first set current logger
        ///     factory.
        /// </exception>
        public static ILogger CreateLogger()
        {
            if (null == CurrentLogFactory)
                throw new InvalidOperationException(
                    "CurrentLogFactory is null, You must first set current logger factory.");
            return CurrentLogFactory.Create();
        }

        #endregion
    }
}