//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CheckoutCart.Data.Model.ShoppingCartModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class CartItem
    {
        public long Id { get; set; }
        public long ShoppingCartId { get; set; }
        public long ProductId { get; set; }
        public int Quantity { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual ShoppingCart ShoppingCart { get; set; }
    }
}
