using CheckoutCart.Data.Model.Core.Abstraction;
using CheckoutCart.DataContract;

namespace CheckoutCart.Services.Implementation.NewItemProcessor
{
    public abstract class AddToCartBaseProcessor
    {
        protected static IUnitOfWork UnitOfWork;
        protected AddToCartBaseProcessor NextStep;

        
        protected AddToCartBaseProcessor(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public AddToCartBaseProcessor SetNextStep(AddToCartBaseProcessor nextStep)
        {
            NextStep = nextStep;
            return this;
        }

        public virtual void Process(CartItemEntity cartItem)
        {
            NextStep?.Process(cartItem);

        }
    }
}
