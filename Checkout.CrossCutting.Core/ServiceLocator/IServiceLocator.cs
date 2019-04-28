using System;
using System.Collections.Generic;

namespace Checkout.CrossCutting.Core.ServiceLocator
{
    /// <summary>
    ///     Defines the methods that simplify service location and dependency resolution.
    /// </summary>
    public interface IServiceLocator
    {
        /// <summary>
        ///     Resolves registered service.
        /// </summary>
        /// <returns>
        ///     The requested service or object.
        /// </returns>
        /// <param name="serviceType">The type of the requested service or object.</param>
        object GetService(Type serviceType);

        /// <summary>
        ///     Resolves registered service.
        /// </summary>
        /// <param name="serviceType">The type of the requested service or object.</param>
        /// <param name="name">name of dependency</param>
        /// <returns> The requested service or object.</returns>
        object GetService(Type serviceType, string name);

        /// <summary>
        ///     Resolves multiply registered services.
        /// </summary>
        /// <returns>
        ///     The requested services.
        /// </returns>
        /// <param name="serviceType">The type of the requested services.</param>
        IEnumerable<object> GetServices(Type serviceType);
    }
}