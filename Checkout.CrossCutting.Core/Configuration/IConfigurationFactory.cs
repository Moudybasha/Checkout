namespace Checkout.CrossCutting.Core.Configuration
{
    public interface IConfigurationFactory
    {
        IConfiguration Create();
    }
}