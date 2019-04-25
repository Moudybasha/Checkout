using CheckoutCart.DataContract;

namespace CheckoutCart.Services.Abstractions
{
    public interface IOrderHandler
    {
        void AddToCart(CartItemEntity cartItem);

        void DeleteCartItem(CartItemUpdateEntity cartItemId);
        void ClearCart(long userId);

       

        void ModifyCartItem(CartItemUpdateEntity cartItem);
    }
}
