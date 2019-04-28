using System;
using Checkout.CrossCutting.Core.ServiceLocator;
using Unity;

namespace Checkout.CrossCutting.Framework.ServiceLocator
{
    public class UnityServiceLocatorFactory : IServiceLocatorFactory
    {
        private readonly IUnityContainer _unityContainer;

        public UnityServiceLocatorFactory(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer ?? throw new ArgumentNullException(nameof(unityContainer));
        }

        #region IServiceLocatorFactory Members

        public IServiceLocator Create()
        {
            return new UnityServiceLocator(_unityContainer);
        }

        #endregion
    }
}