using System;

namespace CheckoutCart.Services.CustomExceptions
{
    /// <summary>
    ///     Custom Exception represent product not available
    /// </summary>
    public class ProductNotAvailableException : Exception
    {
        public ProductNotAvailableException()
        {
        }

        public ProductNotAvailableException(string message) : base(message)
        {
        }

        public ProductNotAvailableException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}