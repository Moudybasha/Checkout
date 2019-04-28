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
                UserId = cartItem.UserId
            };
        }


        /// <summary>
        ///     Map ShoppingCart database object to ShoppingCart response object
        /// </summary>
        /// <param name="shoppingCart">database object that will be mapped</param>
        /// <returns>ShoppingCartResponse object</returns>
        public static ShoppingCartResponse Map(ShoppingCart shoppingCart)
        {
            return Mapper.Map<ShoppingCartResponse>(shoppingCart);
        }
    }
}