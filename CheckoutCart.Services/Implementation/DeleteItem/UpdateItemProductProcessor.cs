using Checkout.CrossCutting.Core.ServiceLocator;
using CheckoutCart.Data.Model.Core.Abstraction;
using CheckoutCart.Data.Model.ShoppingCartModels;
using CheckoutCart.DataContract.RequestEntities;

namespace CheckoutCart.Services.Implementation.DeleteItem
{
    public class UpdateItemProductProcessor:DeleteItemBaseProcessor
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateItemProductProcessor()
        {
            _unitOfWork = ServiceLocatorFactory.CurrentFactory.Create().GetService<IUnitOfWork>();
        }

        public override void Process(CartItemUpdateEntity cartItem)
        {
            var product = _unitOfWork.RepositoryFactory<Product>().Get(p => p.Id == cartItem.ProductId);
            product.AvailabilityCount += cartItem.Quantity;

            _unitOfWork.RepositoryFactory<Product>().Update(product);

            
            base.Process(cartItem);
        }
    }
}
