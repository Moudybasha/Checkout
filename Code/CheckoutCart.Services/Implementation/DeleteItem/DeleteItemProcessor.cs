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

        /// <summary>
        ///     Process Delete cart item
        /// </summary>
        /// <remarks>
        ///     There is two scenarios for deleting cart item
        ///     1. if there is only one cart item in shopping cart, then the entire shopping cart will be deleted
        ///     2. if there more than one cart item in shopping cart, then the cart item only will be deleted
        /// </remarks>
        /// <param name="cartItem"></param>
        public override void Process(CartItemUpdateEntity cartItem)
        {
            var currentShoppingCartItems = _unitOfWork.RepositoryFactory<ShoppingCart>().Get(sc =>
                        sc.UserId == cartItem.UserId && sc.ShoppingCartStatu.Status == CartStatus.InProgress.ToString(),
                    false)
                .CartItems;


            if (currentShoppingCartItems.Count > 1)
                _unitOfWork.RepositoryFactory<CartItem>().Delete(ci => ci.Id == cartItem.CartItemId);

            else
                _unitOfWork.RepositoryFactory<ShoppingCart>().Delete(sc =>
                    sc.UserId == cartItem.UserId &&
                    sc.ShoppingCartStatu.Status == CartStatus.InProgress.ToString());

            _unitOfWork.RepositoryFactory<CartItem>().DeAttach(currentShoppingCartItems);
            _unitOfWork.Save();
            base.Process(cartItem);
        }
    }
}