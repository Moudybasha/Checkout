namespace CheckoutCart.DataContract.RequestEntities
{
    public class CartItemEntity
    {
        public long ProductId { get; set; }
        public int Quantity { get; set; }

        public long UserId { get; set; }

    }
}
