using System;
using System.IO;
using Checkout.CrossCutting.Framework.Resources;
using log4net;
using log4net.Config;

namespace Checkout.CrossCutting.Framework.Logging
{
    public class Log4NetConfigurationLoader
    {
        /// <summary>
        ///     Loads the logger from specified configuration file.
        /// </summary>
        /// <param name="configurationFilename">The configuration filename.</param>
        /// <exception cref="System.ArgumentNullException">Configuration File Name</exception>
        /// <exception cref="System.ArgumentException">Invalid log4net configuration file name. File Name can't be empty.</exception>
        public void Load(string configurationFilename)
        {
            if (null == configurationFilename)
                throw new ArgumentNullException(nameof(configurationFilename));
            if (string.IsNullOrEmpty(configurationFilename))
                throw new ArgumentException("Invalid log4net configuration file name. File Name can't be empty.");
            Configure(configurationFilename);
        }


        /// <summary>
        ///     Loads this logger from default configuration file.
        /// </summary>
        public void Load()
        {
            Configure(StringResources.LogFileName);
        }


        private void Configure(string configurationFilename)
        {
            if (LogManager.GetRepository().Configured == false)
            {
                // Gets directory path of the calling application
                // RelativeSearchPath is null if the executing assembly i.e. calling assembly is a
                // stand alone exe file (Console, WinForm, etc). 
                // RelativeSearchPath is not null if the calling assembly is a web hosted application i.e. a web site
                string log4NetConfigDirectory = AppDomain.CurrentDomain.RelativeSearchPath ??
                                                AppDomain.CurrentDomain.BaseDirectory;
                string log4NetConfigFilePath = Path.Combine(log4NetConfigDirectory, configurationFilename);
                if (!File.Exists(log4NetConfigFilePath))
                    throw new FileNotFoundException(log4NetConfigFilePath);
                XmlConfigurator.ConfigureAndWatch(new FileInfo(log4NetConfigFilePath));
            }
        }
    }
}