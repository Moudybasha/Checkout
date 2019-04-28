using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutCart.DataContract.ResponseEntities.DTOs
{
    public class CartItemDto
    {
        public long Id { get; set; }
        public ProductDto Product { get; set; }
        public int Quantity { get; set; }

        public double TotalProductPrice => Product.Price * Quantity;

    }
}
