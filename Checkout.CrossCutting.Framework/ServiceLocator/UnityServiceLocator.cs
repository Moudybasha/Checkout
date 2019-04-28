using System;
using System.Collections.Generic;
using Checkout.CrossCutting.Core.ServiceLocator;
using Unity;

namespace Checkout.CrossCutting.Framework.ServiceLocator
{
    public class UnityServiceLocator : IServiceLocator
    {
        private readonly IUnityContainer _unitContainer;

        public UnityServiceLocator(IUnityContainer unityContainer)
        {
            _unitContainer = unityContainer ?? throw new ArgumentNullException(nameof(unityContainer));
        }

        public object GetService(Type serviceType)
        {
            return _unitContainer.Resolve(serviceType);
        }

        public object GetService(Type serviceType, string name)
        {
            return _unitContainer.Resolve(serviceType, name);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _unitContainer.ResolveAll(serviceType);
        }
    }
}