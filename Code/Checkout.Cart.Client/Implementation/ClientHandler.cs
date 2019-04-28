using System.Collections.Generic;
using System.Configuration;
using Checkout.Cart.Client.Abstraction;
using Checkout.Cart.Client.Resources;
using CheckoutCart.DataContract.RequestEntities;
using CheckoutCart.DataContract.ResponseEntities;

namespace Checkout.Cart.Client.Implementation
{
    public class ClientHandler : IClientHandler
    {
        private readonly IApiClient _client;


        public ClientHandler(IApiClient client)
        {
            _client = client;
        }

        public ShoppingCartResponse AddItemToCart(long productId, int quantity, long userId)
        {
            var cartItem = new CartItemEntity {ProductId = productId, Quantity = quantity, UserId = userId};
            return _client.Post<ShoppingCartResponse>(EndpointsResources.AddNewItem, cartItem);
        }

        public ShoppingCartResponse UpdateCartItem(long cartItemId,long productId,int newQuantity,long userId)
        {
            var cartItem = new CartItemUpdateEntity()
                {CartItemId = cartItemId, ProductId = productId, Quantity = newQuantity, UserId = userId};

            return _client.Put<ShoppingCartResponse>(EndpointsResources.UpdateCartItem, cartItem);
        }

        public void DeleteCartItem(long cartItemId, long productId, int quantity, long userId)
        {
            var cartItem = new CartItemUpdateEntity()
                { CartItemId = cartItemId, ProductId = productId, Quantity = quantity, UserId = userId };

            _client.Delete(EndpointsResources.RemoveCartItem, cartItem);
        }

        public void ClearCart(long userId)
        {
            var segments = new Dictionary<string, string> {{nameof(userId), userId.ToString()}};
            _client.Delete(EndpointsResources.ClearCart, segments);
        }
    }
}