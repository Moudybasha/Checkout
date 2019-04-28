using CheckoutCart.DataContract.RequestEntities;
using CheckoutCart.DataContract.ResponseEntities;

namespace Checkout.Cart.Client.Abstraction
{
    public interface IClientHandler
    {
        ShoppingCartResponse AddItemToCart(long productId, int quantity, long userId);

        ShoppingCartResponse UpdateCartItem(long cartItemId, long productId, int newQuantity, long userId);


        void DeleteCartItem(long cartItemId, long productId, int quantity, long userId);

        void ClearCart(long userId);
    }
}