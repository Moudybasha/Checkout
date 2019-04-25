using CheckoutCart.Data.Model.Core.Abstraction;

namespace CheckoutCart.Services.Implementation.DeleteItemProcessor
{
    public abstract class DeleteItemBaseProcessor
    {
        protected static IUnitOfWork UnitOfWork;
        protected DeleteItemBaseProcessor NextStep;
       

        protected DeleteItemBaseProcessor(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public DeleteItemBaseProcessor SetNextStep(DeleteItemBaseProcessor nextStep)
        {
            NextStep = nextStep;
            return this;
        }


        public virtual void Process(long cartId)
        {
            NextStep?.Process(cartId);

        }
    }
}
