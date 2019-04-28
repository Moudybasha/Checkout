using System;
using System.Collections.Specialized;
using Checkout.CrossCutting.Core.Configuration;

namespace Checkout.CrossCutting.Framework.Configuration
{
    public class AppSettingConfigurationFactory : IConfigurationFactory
    {
        private readonly NameValueCollection _appSettings;

        public AppSettingConfigurationFactory(NameValueCollection appSettings)
        {
            _appSettings = appSettings ?? throw new ArgumentNullException(nameof(appSettings));
        }

        public IConfiguration Create()
        {
            return new AppSettingConfiguration(_appSettings);
        }
    }
}