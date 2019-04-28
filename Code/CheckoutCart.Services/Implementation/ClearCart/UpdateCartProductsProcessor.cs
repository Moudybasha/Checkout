using Checkout.CrossCutting.Core.ServiceLocator;
using CheckoutCart.Data.Model.Core.Abstraction;
using CheckoutCart.Data.Model.ShoppingCartModels;
using CheckoutCart.Services.Enums;

namespace CheckoutCart.Services.Implementation.ClearCart
{
    public class UpdateCartProductsProcessor : ClearCartBaseProcessor
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCartProductsProcessor()
        {
            _unitOfWork = ServiceLocatorFactory.CurrentFactory.Create().GetService<IUnitOfWork>();
        }

        public override void Process(long userId)
        {
            var shoppingCart = GetShoppingCart(userId);

            UpdateProductQuantity(shoppingCart);

            base.Process(userId);
        }

        /// <summary>
        ///     Get the shopping cart of specific user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private ShoppingCart GetShoppingCart(long userId)
        {
            return _unitOfWork.RepositoryFactory<ShoppingCart>().Get(c =>
                c.UserId == userId && c.ShoppingCartStatu.Status == CartStatus.InProgress.ToString(), false);
        }

        /// <summary>
        ///     Updates the product quantity for each cart item in the shopping cart
        /// </summary>
        /// <param name="shoppingCart"></param>
        private void UpdateProductQuantity(ShoppingCart shoppingCart)
        {
            var cartItems = shoppingCart.CartItems;
            foreach (var cartItem in cartItems)
            {
                var product = cartItem.Product;
                product.AvailabilityCount += cartItem.Quantity;
                _unitOfWork.RepositoryFactory<Product>().Update(product);
            }

            _unitOfWork.Save();
            // DeAttach the cartItems from the context to be able to delete based on cascade delete
            _unitOfWork.RepositoryFactory<CartItem>().DeAttach(cartItems);
        }
    }
}