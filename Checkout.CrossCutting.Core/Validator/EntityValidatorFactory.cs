using System;

namespace Checkout.CrossCutting.Core.Validator
{
    /// <summary>
    ///     EntityValidatorFactory creates EntityValidator
    /// </summary>
    public static class EntityValidatorFactory
    {
        #region Members

        /// CodeReview: change to private static property to improve unit test compliance.
        private static IEntityValidatorFactory _currentFactory;

        #endregion

        #region public members

        public static IEntityValidatorFactory CurrentFactory => _currentFactory;

        #endregion

        #region Public Methods

        /// <summary>
        ///     Sets the current.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <exception cref="System.ArgumentNullException">EntityValidatorFactory</exception>
        public static void SetCurrent(IEntityValidatorFactory factory)
        {
            _currentFactory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        /// <summary>
        ///     Creates the validator.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException">Factory is null, You must first set current factory.</exception>
        public static IEntityValidator CreateValidator()
        {
            if (null == CurrentFactory)
                throw new InvalidOperationException("Factory is null, You must first set current factory.");
            return CurrentFactory.Create();
        }

        #endregion
    }
}