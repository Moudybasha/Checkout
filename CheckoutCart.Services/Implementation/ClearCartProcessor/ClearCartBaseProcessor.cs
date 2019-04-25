using CheckoutCart.Data.Model.Core.Abstraction;
using CheckoutCart.Data.Model.ShoppingCartModels;


namespace CheckoutCart.Services.Implementation.ClearCartProcessor
{
    public abstract class ClearCartBaseProcessor
    {
        protected static IUnitOfWork UnitOfWork;
        protected ClearCartBaseProcessor NextStep;
        protected static CartItem ExistingCartItem;

        protected ClearCartBaseProcessor(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public ClearCartBaseProcessor SetNextStep(ClearCartBaseProcessor nextStep)
        {
            NextStep = nextStep;
            return this;
        }


        public virtual void Process(long userId)
        {
            NextStep?.Process(userId);

        }
    }
}
