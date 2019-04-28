using System;

namespace Checkout.CrossCutting.Core.Configuration
{
    public static class ConfigurationFactory
    {
        #region Members

        #endregion

        #region public members

        public static IConfigurationFactory CurrentFactory { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Sets the current.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <exception cref="System.ArgumentNullException">ConfigurationFactory</exception>
        public static void SetCurrent(IConfigurationFactory factory)
        {
            CurrentFactory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        /// <summary>
        ///     Creates the validator.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException">Factory is null, You must first set current factory.</exception>
        public static IConfiguration CreateConfiguration()
        {
            if (null == CurrentFactory)
                throw new InvalidOperationException("Factory is null, You must first set current factory.");
            return CurrentFactory.Create();
        }

        #endregion
    }
}