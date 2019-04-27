using System;
using System.Data.Entity;
using System.Linq;
using Checkout.CrossCutting.Core.ServiceLocator;
using Checkout.CrossCutting.Framework.Resources;
using CheckoutCart.Data.Model.Core.Abstraction;
using CheckoutCart.Data.Model.Core.Implementation;
using CheckoutCart.Data.Model.ShoppingCartModels;
using CheckoutCart.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;
using Unity.Lifetime;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var x = 10;
            x -= -2;
            var work = new UnitOfWork(new CheckOutCartEntities());
            var xx = work.RepositoryFactory<ShoppingCart>().Get().FirstOrDefault();
            var tt = MapperHelper.Map(xx);
//            Container.Init();
//            Container.Current.RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager())
//                .RegisterType<BaseProcessor, CheckAvailability>();
//            Container.Current.RegisterType<DbContext, CheckOutCartEntities>(new HierarchicalLifetimeManager());
//
//
//            var step = ServiceLocatorFactory.CurrentFactory.Create().GetService<BaseProcessor>();

        }

        
        
    }

    public class TT { }
}
