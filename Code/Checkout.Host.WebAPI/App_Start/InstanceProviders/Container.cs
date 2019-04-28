using System.Configuration;
using System.Data.Entity;
using Checkout.CrossCutting.Core.Configuration;
using Checkout.CrossCutting.Core.ServiceLocator;
using Checkout.CrossCutting.Framework.Configuration;
using Checkout.CrossCutting.Framework.ServiceLocator;
using CheckoutCart.Data.Model.Core.Abstraction;
using CheckoutCart.Data.Model.Core.Implementation;
using CheckoutCart.Data.Model.ShoppingCartModels;
using CheckoutCart.Services.Implementation.ClearCart;
using CheckoutCart.Services.Implementation.DeleteItem;
using CheckoutCart.Services.Implementation.NewItem;
using CheckoutCart.Services.Implementation.UpdateItem;
using Unity;
using Unity.Lifetime;

namespace CheckoutCart.Host.WebAPI.InstanceProviders
{
    public static class Container
    {
        #region Properties

        private static IUnityContainer _currentContainer;

        public static IUnityContainer Current => _currentContainer;

        #endregion

        #region Constructor

        public static void Init()
        {
            ConfigureContainer();

            ConfigureFactories();
        }

        #endregion

        #region Methods

        private static void ConfigureContainer()
        {
            var container = new UnityContainer();
            _currentContainer = container;
            RegisterTypes();
        }

        private static void ConfigureFactories()
        {
            //LoggerFactory.SetCurrent(new Log4NetLoggerFactory());
            ServiceLocatorFactory.SetCurrent(new UnityServiceLocatorFactory(_currentContainer));
            ConfigurationFactory.SetCurrent(new AppSettingConfigurationFactory(ConfigurationManager.AppSettings));
        }


        public static void RegisterTypes()
        {
            Current.RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager()).RegisterType<DbContext, CheckOutCartEntities>(new HierarchicalLifetimeManager())
                .RegisterType<NewItemBaseProcessor, NewItemProductAvailabilityProcessor>()
                .RegisterType<UpdateItemBaseProcessor, UpdateItemProductAvailabilityProcessor>()
                .RegisterType<ClearCartBaseProcessor, UpdateCartProductsProcessor>()
                .RegisterType<DeleteItemBaseProcessor, UpdateItemProductProcessor>();
        }

        #endregion
    }
}