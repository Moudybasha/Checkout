using Checkout.CrossCutting.Core.ServiceLocator;
using CheckoutCart.Data.Model.Core.Abstraction;
using CheckoutCart.Data.Model.ShoppingCartModels;
using CheckoutCart.DataContract.RequestEntities;
using CheckoutCart.DataContract.ResponseEntities;

namespace CheckoutCart.Services.Implementation.UpdateItem
{
    public class UpdateItemProcessor : UpdateItemBaseProcessor

    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateItemProcessor()
        {
            _unitOfWork = ServiceLocatorFactory.CurrentFactory.Create().GetService<IUnitOfWork>();
        }

        /// <summary>
        ///     Updates the cartItem
        /// </summary>
        /// <param name="cartItemUpdate"></param>
        /// <returns></returns>
        public override ShoppingCartResponse Process(CartItemUpdateEntity cartItemUpdate)
        {
            ExistingCartItem.Product.AvailabilityCount
                += ExistingCartItem.Quantity - cartItemUpdate.Quantity;
            ExistingCartItem.Quantity = cartItemUpdate.Quantity;
            _unitOfWork.RepositoryFactory<CartItem>().Update(ExistingCartItem);
            _unitOfWork.Save();


            return MapperHelper.Map(ExistingCartItem.ShoppingCart);
        }
    }
}