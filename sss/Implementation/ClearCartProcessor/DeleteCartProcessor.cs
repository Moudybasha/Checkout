using CheckoutCart.Data.Model.Core.Abstraction;

namespace CheckoutCart.Services.Implementation.ClearCartProcessor
{
    public class DeleteCartProcessor:ClearCartBaseProcessor
    {
        public DeleteCartProcessor(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override void Process(long userId)
        {
            base.Process(userId);
        }
    }
}
