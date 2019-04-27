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

        private ShoppingCart CheckExistingCart(long userId)
        {
            return _unitOfWork.RepositoryFactory<ShoppingCart>()
                .Get(shoppingCart =>
                    shoppingCart.ShoppingCartStatu.Status == CartStatus.InProgress.ToString() &&
                    shoppingCart.User.Id == userId);
        }
    }
}