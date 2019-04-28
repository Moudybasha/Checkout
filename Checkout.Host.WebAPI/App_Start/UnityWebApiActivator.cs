using System.Web.Http;
using CheckoutCart.Host.WebAPI.InstanceProviders;
using Unity.AspNet.WebApi;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(CheckoutCart.Host.WebAPI.UnityWebApiActivator), nameof(CheckoutCart.Host.WebAPI.UnityWebApiActivator.Start))]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(CheckoutCart.Host.WebAPI.UnityWebApiActivator), nameof(CheckoutCart.Host.WebAPI.UnityWebApiActivator.Shutdown))]

namespace CheckoutCart.Host.WebAPI
{
    /// <summary>
    /// Provides the bootstrapping for integrating Unity with WebApi when it is hosted in ASP.NET.
    /// </summary>
    public static class UnityWebApiActivator
    {
        /// <summary>
        /// Integrates Unity when the application starts.
        /// </summary>
        public static void Start() 
        {
            Container.Init();
           
            var resolver = new UnityDependencyResolver(Container.Current);
             
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }

        /// <summary>
        /// Disposes the Unity container when the application is shut down.
        /// </summary>
        public static void Shutdown()
        {
            Container.Current.Dispose();
        }
    }
}