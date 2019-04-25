using System;
using CheckoutCart.Data.Model.Core.Abstraction;
using CheckoutCart.Data.Model.ShoppingCartModels;
using CheckoutCart.DataContract;

namespace CheckoutCart.Services.Implementation.UpdateItemProcessor
{
    public class CheckAvailabilityProcessor:UpdateCartBaseProcessor
    {
        public CheckAvailabilityProcessor(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override void Process(CartItemUpdateEntity cartItem)
        {
            var existingCartItem = UnitOfWork.RepositoryFactory<CartItem>().Get(c => c.Id == cartItem.CartItemId);
            if (cartItem.Count > existingCartItem.Quantity)
            {
                var isAvailable =
                    CheckStockAvailability(cartItem.ProductId, cartItem.Count - existingCartItem.Quantity);
                if (!isAvailable)
                    throw new Exception();
            }
            base.Process(cartItem);
        }

        private bool CheckStockAvailability(long productId, long quantity)
        {
            var isAvailable = UnitOfWork.RepositoryFactory<Product>()
                .Any(c => c.Id == productId && c.AvailabilityCount >= quantity);
            return isAvailable;
        }
    }
}
