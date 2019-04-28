using System.Net;
using System.Net.Http;
using System.Web.Http;
using CheckoutCart.DataContract.RequestEntities;
using CheckoutCart.DataContract.ResponseEntities;
using CheckoutCart.Services.Implementation.ClearCart;
using CheckoutCart.Services.Implementation.DeleteItem;
using CheckoutCart.Services.Implementation.NewItem;
using CheckoutCart.Services.Implementation.UpdateItem;

namespace CheckoutCart.Host.WebAPI.Controllers
{
    [RoutePrefix("api/cart")]
    public class CartController : ApiController
    {
        private readonly DeleteItemBaseProcessor _deleteItemProcessor;
        private readonly NewItemBaseProcessor _newItemProcessor;
        private readonly UpdateItemBaseProcessor _updateItemProcessor;
        private readonly ClearCartBaseProcessor _clearCartProcessor;

        public CartController(DeleteItemBaseProcessor deleteItemProcessor, NewItemBaseProcessor newItemProcessor,
            UpdateItemBaseProcessor updateItemProcessor, ClearCartBaseProcessor clearCartProcessor)
        {
            _deleteItemProcessor = deleteItemProcessor;
            _newItemProcessor = newItemProcessor;
            _updateItemProcessor = updateItemProcessor;
            _clearCartProcessor = clearCartProcessor;
        }

        // GET: Cart
        [Route("item")]
        [HttpPost]
        public ShoppingCartResponse AddToCart(CartItemEntity cartItem)
        {
            return _newItemProcessor.Process(cartItem);
        }

        [Route("item")]
        [HttpPut]
        public ShoppingCartResponse ModifyCartItem(CartItemUpdateEntity cartItemUpdate)
        {
           return _updateItemProcessor.Process(cartItemUpdate);
        }

        [HttpDelete]
        [Route("item")]
        public HttpResponseMessage DeleteCartItem(CartItemUpdateEntity cartItemUpdate)
        {
            _deleteItemProcessor.Process(cartItemUpdate);
            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }

        [HttpDelete]
        [Route("{userId}")]
        public HttpResponseMessage ClearCart(long userId)
        {
            _clearCartProcessor.Process(userId);
            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }

        
    }
}