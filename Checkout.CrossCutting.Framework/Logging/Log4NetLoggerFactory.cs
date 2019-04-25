using Checkout.CrossCutting.Core.Logging;
using log4net;

namespace Checkout.CrossCutting.Framework.Logging
{
    /// <summary>
    ///     Used to inialize, load configuration and create the objects used for the Log4net module
    /// </summary>
    public class Log4NetLoggerFactory
        : ILoggerFactory
    {
        #region Private Fields

        private static bool _configurationLoaded;
        private static ILog _log;

        #endregion

        #region Pulic properties

        /// <summary>
        ///     Gets a value indicating whether [configuration loaded].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [configuration loaded]; otherwise, <c>false</c>.
        /// </value>
        public static bool ConfigurationLoaded => _configurationLoaded;

        #endregion

        /// <summary>
        ///     Create a new ILogger
        /// </summary>
        /// <returns>
        ///     The ILogger created
        /// </returns>
        public ILogger Create()
        {
            if (!_configurationLoaded)
            {
                var configurationLoader = new Log4NetConfigurationLoader();
                configurationLoader.Load();
                _log = LogManager.GetLogger(typeof (Log4NetLogger));
                _configurationLoaded = true;
            }
            return new Log4NetLogger(_log);
        }
    }
}