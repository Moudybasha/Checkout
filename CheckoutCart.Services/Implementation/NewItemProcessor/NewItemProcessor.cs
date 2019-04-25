using CheckoutCart.Data.Model.Core.Abstraction;
using CheckoutCart.Data.Model.ShoppingCartModels;
using CheckoutCart.DataContract;
using CheckoutCart.Services.Enums;

namespace CheckoutCart.Services.Implementation.NewItemProcessor
{
    public class NewItemProcessor:AddToCartBaseProcessor
    {
        public NewItemProcessor(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override void Process(CartItemEntity cartItem)
        {
            var cart = CheckExistingCart(cartItem.UserId);
            if (cart == null)
            {
                cart = Mapper.Map(cartItem);
                cart.CartItems.Add(new CartItem { ProductId = cartItem.ProductId, Quantity = cartItem.Count });
                UnitOfWork.RepositoryFactory<ShoppingCart>().Insert(cart);
            }
            else
            {
                cart.CartItems.Add(new CartItem { ProductId = cartItem.ProductId, Quantity = cartItem.Count });
                UnitOfWork.RepositoryFactory<ShoppingCart>().Update(cart);
            }

            UnitOfWork.Save();
        }

        private ShoppingCart CheckExistingCart(long userId)
        {
            return UnitOfWork.RepositoryFactory<ShoppingCart>()
                .Get(shoppingCart =>
                    shoppingCart.ShoppingCartStatu.Status == CartStatus.InProgress.ToString() && shoppingCart.User.Id == userId);
        }
    }
}
