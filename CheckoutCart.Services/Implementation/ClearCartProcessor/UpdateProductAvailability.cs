using CheckoutCart.Data.Model.Core.Abstraction;
using CheckoutCart.Data.Model.ShoppingCartModels;
using CheckoutCart.Services.Enums;

namespace CheckoutCart.Services.Implementation.ClearCartProcessor
{
    public class UpdateProductAvailability:ClearCartBaseProcessor
    {
        public UpdateProductAvailability(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override void Process(long userId)
        {
            var shoppingCart = GetShoppingCart(userId);

            ModifyProduct(shoppingCart);

            base.Process(userId);
        }

        private static ShoppingCart GetShoppingCart(long userId)
        {
            return UnitOfWork.RepositoryFactory<ShoppingCart>().Get(c =>
                c.UserId == userId && c.ShoppingCartStatu.Status == CartStatus.InProgress.ToString());
        }

        private static void ModifyProduct(ShoppingCart shoppingCart)
        {
            var cartItems = shoppingCart.CartItems;
            foreach (var cartItem in cartItems)
            {
                var product = cartItem.Product;
                product.AvailabilityCount += cartItem.Quantity;
                UnitOfWork.RepositoryFactory<Product>().Update(product);
            }
        }
    }
}
