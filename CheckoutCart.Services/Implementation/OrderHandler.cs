using System;
using CheckoutCart.Data.Model.Core.Abstraction;
using CheckoutCart.Data.Model.ShoppingCartModels;
using CheckoutCart.DataContract.RequestEntities;
using CheckoutCart.Services.Abstractions;

namespace CheckoutCart.Services.Implementation
{
    public class OrderHandler : IOrderHandler
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddToCart(CartItemEntity cartItem)
        {
            #region Refactoring

            //if (!CheckStockAvailability(cartItemUpdate.ProductId, cartItemUpdate.Quantity))
            //    throw new Exception(); //TODO: Add custom exception

            //var cart = CheckExistingCart(cartItemUpdate.UserId);
            //if (cart == null)
            //{
            //    cart = MapperHelper.Map(cartItemUpdate);
            //    cart.CartItems.Add(new CartItem {ProductId = cartItemUpdate.ProductId, Quantity = cartItemUpdate.Quantity});
            //    _unitOfWork.RepositoryFactory<ShoppingCart>().Insert(cart);
            //}
            //else
            //{
            //    cart.CartItems.Add(new CartItem {ProductId = cartItemUpdate.ProductId, Quantity = cartItemUpdate.Quantity});
            //    _unitOfWork.RepositoryFactory<ShoppingCart>().Update(cart);
            //}

            //_unitOfWork.Save();

            #endregion

//            AddToCartBaseProcessor cart = new CheckAvailability(_unitOfWork).SetNextStep(new NewItemProcessor.NewItemProcessor(_unitOfWork));
//
//            cart.Process(cartItemUpdate);

        }

        public void DeleteCartItem(CartItemUpdateEntity cartItemUpdate)
        {
            //TODO: handle adding product count
            var product = new Product {Id = cartItemUpdate.ProductId};
            _unitOfWork.RepositoryFactory<Product>().Attach(product);

            product.AvailabilityCount += cartItemUpdate.Quantity;

            _unitOfWork.RepositoryFactory<Product>().Update(product);
            _unitOfWork.Save();
        }

        public void ClearCart(long userId)
        {
            //TODO: update the stock
            _unitOfWork.RepositoryFactory<ShoppingCart>().Delete(cart => cart.UserId == userId);
        }

        public void ModifyCartItem(CartItemUpdateEntity cartItemsUpdate)
        {
            var existingCartItem = _unitOfWork.RepositoryFactory<CartItem>().Get(c => c.Id == cartItemsUpdate.CartItemId);
            if (cartItemsUpdate.Quantity > existingCartItem.Quantity)
            {
                var isAvailable =
                    CheckStockAvailability(cartItemsUpdate.ProductId, cartItemsUpdate.Quantity - existingCartItem.Quantity);
                if (!isAvailable)
                    throw new Exception();
            }

            existingCartItem.Quantity = cartItemsUpdate.Quantity;
            existingCartItem.Product.AvailabilityCount =
                Math.Abs(existingCartItem.Product.AvailabilityCount - cartItemsUpdate.Quantity);
            _unitOfWork.RepositoryFactory<CartItem>().Update(existingCartItem);
            _unitOfWork.Save();
        }


        #region Private Methods

        private bool CheckStockAvailability(long productId, long quantity)
        {
            var isAvailable = _unitOfWork.RepositoryFactory<Product>()
                .Any(c => c.Id == productId && c.AvailabilityCount >= quantity);
            return isAvailable;
        }

        private ShoppingCart CheckExistingCart(long userId)
        {
            return _unitOfWork.RepositoryFactory<ShoppingCart>()
                .Get(shoppingCart =>
                    shoppingCart.ShoppingCartStatu.Status == "InProgress" && shoppingCart.User.Id == userId);
        }

        #endregion
    }
}