using CheckoutCart.Data.Model.ShoppingCartModels;
using CheckoutCart.DataContract;


namespace CheckoutCart.Services
{
    public static class Mapper
    {
        public static ShoppingCart Map(CartItemEntity cartItem)
        {
           return new ShoppingCart
            {
                UserId = cartItem.UserId,
            };

           

            
        }
    }
}
