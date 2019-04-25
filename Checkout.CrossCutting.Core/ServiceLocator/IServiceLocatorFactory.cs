namespace Checkout.CrossCutting.Core.ServiceLocator
{
    public interface IServiceLocatorFactory
    {
        IServiceLocator Create();
    }
}