using CheckoutCart.DataContract;
using CheckoutCart.DataContract.RequestEntities;

namespace CheckoutCart.Services.Abstractions
{
    public interface IOrderHandler
    {
        void AddToCart(CartItemEntity cartItem);

        void DeleteCartItem(CartItemUpdateEntity cartItemUpdateId);
        void ClearCart(long userId);

       

        void ModifyCartItem(CartItemUpdateEntity cartItemUpdate);
    }
}
