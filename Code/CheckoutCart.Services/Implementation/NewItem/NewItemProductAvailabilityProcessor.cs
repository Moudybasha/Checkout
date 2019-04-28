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

        /// <summary>
        ///     Process updating product availability
        /// </summary>
        /// <param name="cartItem"></param>
        /// <exception cref="ProductNotAvailableException"></exception>
        /// <returns></returns>
        public override ShoppingCartResponse Process(CartItemEntity cartItem)
        {
            var product = GetProduct(cartItem.ProductId);

            if (!IsProductAvailable(product, cartItem.Quantity))
                throw new ProductNotAvailableException(string.Format(ErrorMessages.ProductNotAvailable,
                    cartItem.Quantity,
                    cartItem.ProductId));

            UpdateProductAvailability(product, cartItem.Quantity);

            return base.Process(cartItem);
        }

        /// <summary>
        ///     Get product object from DB
        /// </summary>
        /// <param name="productId"></param>
        /// <returns>product object</returns>
        private Product GetProduct(long productId)
        {
            return _unitOfWork.RepositoryFactory<Product>().Get(p => p.Id == productId);
        }

        /// <summary>
        ///     Checks if there is available quantity
        /// </summary>
        /// <param name="product">The product that will be checked</param>
        /// <param name="quantity">The required quantity</param>
        /// <returns></returns>
        private bool IsProductAvailable(Product product, long quantity)
        {
            return product.AvailabilityCount >= quantity;
        }

        /// <summary>
        ///     Updates the product quantity
        /// </summary>
        /// <param name="product">The product that will be updated</param>
        /// <param name="quantity">The quantity that will be cut from the product current quantity</param>
        private void UpdateProductAvailability(Product product, long quantity)
        {
            product.AvailabilityCount -= quantity;
            _unitOfWork.RepositoryFactory<Product>().Update(product);
            _unitOfWork.Save();
        }
    }
}