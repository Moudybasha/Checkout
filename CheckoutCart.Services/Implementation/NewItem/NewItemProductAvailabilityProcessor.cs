using Checkout.CrossCutting.Core.ServiceLocator;
using CheckoutCart.Data.Model.Core.Abstraction;
using CheckoutCart.Data.Model.ShoppingCartModels;
using CheckoutCart.DataContract.RequestEntities;
using CheckoutCart.DataContract.ResponseEntities;
using CheckoutCart.Services.CustomExceptions;
using CheckoutCart.Services.Resources;

namespace CheckoutCart.Services.Implementation.NewItem
{
    public class NewItemProductAvailabilityProcessor : NewItemBaseProcessor
    {
        private readonly IUnitOfWork _unitOfWork;

        public NewItemProductAvailabilityProcessor()
        {
            _unitOfWork = ServiceLocatorFactory.CurrentFactory.Create().GetService<IUnitOfWork>();
        }

        public override ShoppingCartResponse Process(CartItemEntity cartItem)
        {
            var product = GetProduct(cartItem.ProductId);

            if (!IsProductAvailable(product, cartItem.Quantity))
                throw new ProductNotAvailableException(string.Format(ErrorMessages.ProductNotAvailable, cartItem.Quantity,
                    cartItem.ProductId));

            UpdateProductAvailability(product, cartItem.Quantity);

            return base.Process(cartItem);
        }

        private Product GetProduct(long productId)
        {
            return _unitOfWork.RepositoryFactory<Product>().Get(p => p.Id == productId);
        }

        private bool IsProductAvailable(Product product, long quantity)
        {
            return product.AvailabilityCount >= quantity;
        }

        private void UpdateProductAvailability(Product product, long quantity)
        {
            product.AvailabilityCount -= quantity;
            _unitOfWork.RepositoryFactory<Product>().Update(product);
            _unitOfWork.Save();
        }
    }
}