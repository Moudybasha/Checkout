using Checkout.CrossCutting.Core.ServiceLocator;
using CheckoutCart.Data.Model.Core.Abstraction;
using CheckoutCart.Data.Model.ShoppingCartModels;
using CheckoutCart.DataContract.RequestEntities;
using CheckoutCart.DataContract.ResponseEntities;
using CheckoutCart.Services.CustomExceptions;
using CheckoutCart.Services.Resources;

namespace CheckoutCart.Services.Implementation.UpdateItem
{
    public class UpdateItemProductAvailabilityProcessor : UpdateItemBaseProcessor
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateItemProductAvailabilityProcessor()
        {
            _unitOfWork = ServiceLocatorFactory.CurrentFactory.Create().GetService<IUnitOfWork>();
        }

        public override ShoppingCartResponse Process(CartItemUpdateEntity cartItemUpdate)
        {
            ExistingCartItem = _unitOfWork.RepositoryFactory<CartItem>().Get(c => c.Id == cartItemUpdate.CartItemId);


            if (cartItemUpdate.Quantity > ExistingCartItem.Quantity)
            {
                var quantityDiff = cartItemUpdate.Quantity - ExistingCartItem.Quantity;

                var product = GetProduct(cartItemUpdate.ProductId);

                var isAvailable =
                    IsProductAvailable(product, quantityDiff);

                if (!isAvailable)
                    throw new ProductNotAvailableException(string.Format(ErrorMessages.ProductNotAvailable,
                        quantityDiff, cartItemUpdate.ProductId));
            }

            return base.Process(cartItemUpdate);
        }

        private Product GetProduct(long productId)
        {
            return _unitOfWork.RepositoryFactory<Product>()
                .Get(c => c.Id == productId, false);
        }

        private bool IsProductAvailable(Product product, long quantity)
        {
            return product.AvailabilityCount >= quantity;
        }
    }
}
