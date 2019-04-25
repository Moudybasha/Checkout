using System;
using CheckoutCart.Data.Model.Core.Abstraction;
using CheckoutCart.Data.Model.ShoppingCartModels;
using CheckoutCart.DataContract;

namespace CheckoutCart.Services.Implementation.NewItemProcessor
{
    public class CheckAvailability:AddToCartBaseProcessor
    {
        public CheckAvailability(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override void Process(CartItemEntity cartItem)
        {
            if (!CheckStockAvailability(cartItem.ProductId, cartItem.Count))
                throw new Exception(); //TODO: Add custom exception
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
