using System.Collections.Specialized;
using Checkout.CrossCutting.Core.Configuration;

namespace Checkout.CrossCutting.Framework.Configuration
{
    public class AppSettingConfiguration : IConfiguration
    {
        private readonly NameValueCollection _appSettings;

        public AppSettingConfiguration(NameValueCollection appSettings)
        {
            _appSettings = appSettings;
        }

        public string Get(string key)
        {
            return _appSettings[key];
        }
    }
}