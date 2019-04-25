namespace CheckoutCart.DataContract
{
    public class CartItemEntity
    {
        public long ProductId { get; set; }
        public int Count { get; set; }

        public long UserId { get; set; }

    }
}
