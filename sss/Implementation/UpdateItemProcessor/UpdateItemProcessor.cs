using System;
using CheckoutCart.Data.Model.Core.Abstraction;
using CheckoutCart.Data.Model.ShoppingCartModels;
using CheckoutCart.DataContract;

namespace CheckoutCart.Services.Implementation.UpdateItemProcessor
{
    public class UpdateItemProcessor : UpdateCartBaseProcessor

    {
        public UpdateItemProcessor(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override void Process(CartItemUpdateEntity cartItem)
        {
            ExistingCartItem.Quantity = cartItem.Count;
            ExistingCartItem.Product.AvailabilityCount =
                Math.Abs(ExistingCartItem.Product.AvailabilityCount - cartItem.Count);
            UnitOfWork.RepositoryFactory<CartItem>().Update(ExistingCartItem);
            UnitOfWork.Save();

            base.Process(cartItem);
        }
    }
}
