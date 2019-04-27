using Checkout.CrossCutting.Core.ServiceLocator;
using CheckoutCart.Data.Model.Core.Abstraction;
using CheckoutCart.Data.Model.ShoppingCartModels;
using CheckoutCart.DataContract.RequestEntities;
using CheckoutCart.Services.Enums;

namespace CheckoutCart.Services.Implementation.DeleteItem
{
    public class DeleteItemProcessor : DeleteItemBaseProcessor
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteItemProcessor()
        {
            _unitOfWork = ServiceLocatorFactory.CurrentFactory.Create().GetService<IUnitOfWork>();
        }

        public override void Process(CartItemUpdateEntity cartItem)
        {
            var currentShoppingCartCount = _unitOfWork.RepositoryFactory<ShoppingCart>().Get(sc =>
                    sc.UserId == cartItem.UserId && sc.ShoppingCartStatu.Status == CartStatus.InProgress.ToString(),false)
                .CartItems.Count;

            if (currentShoppingCartCount > 1)
                _unitOfWork.RepositoryFactory<CartItem>().Delete(ci => ci.Id == cartItem.CartItemId);

            else
                _unitOfWork.RepositoryFactory<ShoppingCart>().Delete(sc =>
                    sc.UserId == cartItem.UserId &&
                    sc.ShoppingCartStatu.Status == CartStatus.InProgress.ToString());


            _unitOfWork.Save();
            base.Process(cartItem);
        }
    }
}