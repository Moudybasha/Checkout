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

            ModifyProduct(shoppingCart);

            base.Process(userId);
        }

        private ShoppingCart GetShoppingCart(long userId)
        {
            return _unitOfWork.RepositoryFactory<ShoppingCart>().Get(c =>
                c.UserId == userId && c.ShoppingCartStatu.Status == CartStatus.InProgress.ToString());
        }

        private void ModifyProduct(ShoppingCart shoppingCart)
        {
            var cartItems = shoppingCart.CartItems;
            foreach (var cartItem in cartItems)
            {
                var product = cartItem.Product;
                product.AvailabilityCount += cartItem.Quantity;
                _unitOfWork.RepositoryFactory<Product>().Update(product);
            }

            _unitOfWork.Save();
        }
    }
}