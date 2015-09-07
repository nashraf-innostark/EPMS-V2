namespace EPMS.Models.DomainModels
{
    public class ShoppingCartItem
    {
        public long CartItemId { get; set; }
        public long CartId { get; set; }
        public long ProductId { get; set; }
        public long SizeId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }

        public virtual Product Product { get; set; }
        public virtual ShoppingCart ShoppingCart { get; set; }
        public virtual Size Size { get; set; }
    }
}
