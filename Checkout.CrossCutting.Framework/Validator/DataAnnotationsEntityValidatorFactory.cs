using Checkout.CrossCutting.Core.Validator;

namespace Checkout.CrossCutting.Framework.Validator
{
    public class DataAnnotationsEntityValidatorFactory
        : IEntityValidatorFactory
    {
        public IEntityValidator Create()
        {
            return new DataAnnotationsEntityValidator();
        }
    }
}