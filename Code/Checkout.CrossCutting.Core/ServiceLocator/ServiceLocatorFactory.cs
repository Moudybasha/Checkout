using System;

namespace Checkout.CrossCutting.Core.ServiceLocator
{
    /// <summary>
    ///     ServiceLocatorFactory creates ServiceLocator
    /// </summary>
    public static class ServiceLocatorFactory
    {
        #region Members

        #endregion

        #region public members

        public static IServiceLocatorFactory CurrentFactory { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Sets the current.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <exception cref="System.ArgumentNullException">factory</exception>
        public static void SetCurrent(IServiceLocatorFactory factory)
        {
            CurrentFactory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        /// <summary>
        ///     Creates the service locator.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException">Factory is null, You must first set current factory.</exception>
        public static IServiceLocator CreateServiceLocator()
        {
            if (null == CurrentFactory)
                throw new InvalidOperationException("Factory is null, You must first set current factory.");
            return CurrentFactory.Create();
        }

        #endregion
    }
}