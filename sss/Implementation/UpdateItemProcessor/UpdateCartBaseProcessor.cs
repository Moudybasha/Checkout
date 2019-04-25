using CheckoutCart.Data.Model.Core.Abstraction;
using CheckoutCart.Data.Model.ShoppingCartModels;
using CheckoutCart.DataContract;

namespace CheckoutCart.Services.Implementation.UpdateItemProcessor
{
    public abstract class UpdateCartBaseProcessor
    {
        protected static IUnitOfWork UnitOfWork;
        protected UpdateCartBaseProcessor NextStep;
        protected static CartItem ExistingCartItem;

        protected UpdateCartBaseProcessor(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public UpdateCartBaseProcessor SetNextStep(UpdateCartBaseProcessor nextStep)
        {
            NextStep = nextStep;
            return this;
        }

        
        public virtual void Process(CartItemUpdateEntity cartItem)
        {
            NextStep?.Process(cartItem);

        }
    }
}
