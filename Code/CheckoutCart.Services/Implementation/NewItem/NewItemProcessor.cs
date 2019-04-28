using Checkout.CrossCutting.Core.ServiceLocator;
using CheckoutCart.Data.Model.Core.Abstraction;
using CheckoutCart.Data.Model.ShoppingCartModels;
using CheckoutCart.DataContract.RequestEntities;
using CheckoutCart.DataContract.ResponseEntities;
using CheckoutCart.Services.Enums;

namespace CheckoutCart.Services.Implementation.NewItem
{
    public class NewItemProcessor : NewItemBaseProcessor
    {
        private readonly IUnitOfWork _unitOfWork;

        public NewItemProcessor()
        {
            _unitOfWork = ServiceLocatorFactory.CurrentFactory.Create().GetService<IUnitOfWork>();
        }

        /// <summary>
        ///     Process adding new Item to cart
        /// </summary>
        /// <remarks>
        ///     There is two scenarios for this:
        ///     1. Either there is no active shopping cart, then a new cart will be created with the new item.
        ///     2. or there is an active shopping cart, then the new item will be added to the same cart.
        /// </remarks>
        /// <param name="cartItem"></param>
        /// <returns></returns>
        public override ShoppingCartResponse Process(CartItemEntity cartItem)
        {
            var cart = CheckExistingCart(cartItem.UserId);
            if (cart == null)
            {
                cart = MapperHelper.Map(cartItem);
                cart.CartItems.Add(new CartItem {ProductId = cartItem.ProductId, Quantity = cartItem.Quantity});
                _unitOfWork.RepositoryFactory<ShoppingCart>().Insert(cart);
            }
            else
            {
                cart.CartItems.Add(new CartItem {ProductId = cartItem.ProductId, Quantity = cartItem.Quantity});
                _unitOfWork.RepositoryFactory<ShoppingCart>().Update(cart);
            }

            _unitOfWork.Save();

            return MapperHelper.Map(cart);
        }

        /// <summary>
        ///     Gets the existing shopping cart
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>The Shopping cart that has In Progress status</returns>
        private ShoppingCart CheckExistingCart(long userId)
        {
            return _unitOfWork.RepositoryFactory<ShoppingCart>()
                .Get(shoppingCart =>
                    shoppingCart.ShoppingCartStatu.Status == CartStatus.InProgress.ToString() &&
                    shoppingCart.User.Id == userId);
        }
    }
}