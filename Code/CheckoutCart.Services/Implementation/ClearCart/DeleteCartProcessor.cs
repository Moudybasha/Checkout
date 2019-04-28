using Checkout.CrossCutting.Core.ServiceLocator;
using CheckoutCart.Data.Model.Core.Abstraction;
using CheckoutCart.Data.Model.ShoppingCartModels;
using CheckoutCart.Services.Enums;

namespace CheckoutCart.Services.Implementation.ClearCart
{
    public class DeleteCartProcessor : ClearCartBaseProcessor
    {
        private readonly IUnitOfWork _unitOfWOrk;

        public DeleteCartProcessor()
        {
            _unitOfWOrk = ServiceLocatorFactory.CurrentFactory.Create().GetService<IUnitOfWork>();
        }

        /// <summary>
        ///     Delete the cart from DB
        /// </summary>
        /// <remarks>Note that the current delete functionality is built on top cascade delete</remarks>
        /// <param name="userId"></param>
        public override void Process(long userId)
        {
            _unitOfWOrk.RepositoryFactory<ShoppingCart>().Delete(c =>
                c.UserId == userId && c.ShoppingCartStatu.Status == CartStatus.InProgress.ToString());
            _unitOfWOrk.Save();

            base.Process(userId);
        }
    }
}