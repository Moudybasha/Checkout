using System.Collections.Generic;
using System.Linq;

namespace Checkout.CrossCutting.Core.ServiceLocator
{
    /// <summary>
    ///     Provides a type-safe implementation of
    /// </summary>
    public static class ServiceLocatorExtensions
    {
        public static TService GetService<TService>(this IServiceLocator serviceLocator)
        {
            return (TService) serviceLocator.GetService(typeof(TService));
        }

        public static TService GetService<TService>(this IServiceLocator serviceLocator, string name)
        {
            return (TService) serviceLocator.GetService(typeof(TService), name);
        }

        public static IEnumerable<TService> GetServices<TService>(this IServiceLocator serviceLocator)
        {
            return serviceLocator.GetServices(typeof(TService)).Cast<TService>();
        }
    }
}