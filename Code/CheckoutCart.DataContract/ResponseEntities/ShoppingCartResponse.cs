using System;
using System.Collections.Generic;
using System.Linq;
using CheckoutCart.DataContract.ResponseEntities.DTOs;

namespace CheckoutCart.DataContract.ResponseEntities
{
    public class ShoppingCartResponse
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Status { get; set; }
        public  IEnumerable<CartItemDto> CartItems { get; set; }

        public double TotalCartPrice => CartItems.Sum(c => c.TotalProductPrice);


    }
}
