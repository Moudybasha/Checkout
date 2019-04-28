namespace Checkout.CrossCutting.Core.Configuration
{
    public interface IConfiguration
    {
        string Get(string key);
    }
}