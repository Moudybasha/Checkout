using System.Configuration;
using Checkout.CrossCutting.Core.Configuration;
using Checkout.CrossCutting.Core.ServiceLocator;
using Checkout.CrossCutting.Framework.Configuration;
using Checkout.CrossCutting.Framework.ServiceLocator;
using Unity;

namespace UnitTestProject1
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



        }

        private static void ConfigureFactories()
        {
            //LoggerFactory.SetCurrent(new Log4NetLoggerFactory());
            ServiceLocatorFactory.SetCurrent(new UnityServiceLocatorFactory(_currentContainer));
            ConfigurationFactory.SetCurrent(new AppSettingConfigurationFactory(ConfigurationManager.AppSettings));


        }

        #endregion
    }
}