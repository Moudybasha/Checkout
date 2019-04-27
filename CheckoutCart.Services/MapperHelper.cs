using AutoMapper;
using CheckoutCart.Data.Model.ShoppingCartModels;
using CheckoutCart.DataContract.RequestEntities;
using CheckoutCart.DataContract.ResponseEntities;


namespace CheckoutCart.Services
{
    public static class MapperHelper
    {
        static MapperHelper()
        {
            Mapper.Initialize(cfg =>
                cfg.CreateMap<ShoppingCart, ShoppingCartResponse>().ForMember(member => member.Status,
                    expression => expression.MapFrom(src => src.ShoppingCartStatu.Status)));
        }

        public static ShoppingCart Map(CartItemEntity cartItem)
        {
            return new ShoppingCart
            {
                UserId = cartItem.UserId,
            };
        }

        public static ShoppingCartResponse Map(ShoppingCart shoppingCart)
        {
            return Mapper.Map<ShoppingCartResponse>(shoppingCart);
        }
    }
}
