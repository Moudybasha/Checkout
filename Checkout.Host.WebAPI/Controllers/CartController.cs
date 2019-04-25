using System.Web.Http;
using CheckoutCart.Data.Model.Core.Implementation;
using CheckoutCart.Data.Model.ShoppingCartModels;
using CheckoutCart.DataContract;
using CheckoutCart.Services.Abstractions;
using CheckoutCart.Services.Implementation;

namespace CheckoutCart.Host.WebAPI.Controllers
{
   [RoutePrefix("api/cart")]
    public class CartController : ApiController
    {
        private readonly IOrderHandler _handler;

        public CartController()
        {
            _handler = new OrderHandler(new UnitOfWork(new CheckOutCartEntities()));
        }
        // GET: Cart
        [Route("item")]
        [HttpPost]
        public void AddToCart(CartItemEntity cartItem)
        {
            _handler.AddToCart(cartItem);
        }

        [Route("item")]
        [HttpPut]
        public void ModifyCartItem(CartItemUpdateEntity cartItem)
        {
            _handler.ModifyCartItem(cartItem);
        }

        [HttpDelete]
        [Route("item")]
        public void DeleteCartItem(CartItemUpdateEntity cartItem)
        {
            _handler.DeleteCartItem(cartItem);
        }
        
        [HttpDelete]
        public void ClearCart(string userName)
        {

        }

        public void GetShoppingCartItems(string userName)
        {

        }
    }
}
