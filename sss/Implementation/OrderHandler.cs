using System;
using CheckoutCart.Data.Model.Core.Abstraction;
using CheckoutCart.Data.Model.ShoppingCartModels;
using CheckoutCart.DataContract;
using CheckoutCart.Services.Abstractions;
using CheckoutCart.Services.Implementation.NewItemProcessor;

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

            //if (!CheckStockAvailability(cartItem.ProductId, cartItem.Count))
            //    throw new Exception(); //TODO: Add custom exception

            //var cart = CheckExistingCart(cartItem.UserId);
            //if (cart == null)
            //{
            //    cart = Mapper.Map(cartItem);
            //    cart.CartItems.Add(new CartItem {ProductId = cartItem.ProductId, Quantity = cartItem.Count});
            //    _unitOfWork.RepositoryFactory<ShoppingCart>().Insert(cart);
            //}
            //else
            //{
            //    cart.CartItems.Add(new CartItem {ProductId = cartItem.ProductId, Quantity = cartItem.Count});
            //    _unitOfWork.RepositoryFactory<ShoppingCart>().Update(cart);
            //}

            //_unitOfWork.Save();

            #endregion

            AddToCartBaseProcessor cart = new CheckAvailability(_unitOfWork).SetNextStep(new NewItemProcessor.NewItemProcessor(_unitOfWork));

            cart.Process(cartItem);

        }

        public void DeleteCartItem(CartItemUpdateEntity cartItem)
        {
            //TODO: handle adding product count
            var product = new Product {Id = cartItem.ProductId};
            _unitOfWork.RepositoryFactory<Product>().Attach(product);

            product.AvailabilityCount += cartItem.Count;

            _unitOfWork.RepositoryFactory<Product>().Update(product);
            _unitOfWork.Save();
        }

        public void ClearCart(long userId)
        {
            //TODO: update the stock
            _unitOfWork.RepositoryFactory<ShoppingCart>().Delete(cart => cart.UserId == userId);
        }

        public void ModifyCartItem(CartItemUpdateEntity cartItems)
        {
            var existingCartItem = _unitOfWork.RepositoryFactory<CartItem>().Get(c => c.Id == cartItems.CartItemId);
            if (cartItems.Count > existingCartItem.Quantity)
            {
                var isAvailable =
                    CheckStockAvailability(cartItems.ProductId, cartItems.Count - existingCartItem.Quantity);
                if (!isAvailable)
                    throw new Exception();
            }

            existingCartItem.Quantity = cartItems.Count;
            existingCartItem.Product.AvailabilityCount =
                Math.Abs(existingCartItem.Product.AvailabilityCount - cartItems.Count);
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