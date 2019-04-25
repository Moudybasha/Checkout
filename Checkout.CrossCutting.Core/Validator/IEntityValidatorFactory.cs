namespace Checkout.CrossCutting.Core.Validator
{
    public interface IEntityValidatorFactory
    {
        IEntityValidator Create();
    }
}