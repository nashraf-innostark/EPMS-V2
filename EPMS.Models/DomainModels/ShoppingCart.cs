using System;

namespace EPMS.Models.DomainModels
{
    public class ShoppingCart
    {
        public long CartId { get; set; }
        public string UserCartId { get; set; }
        public long ProductId { get; set; }
        public long SizeId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDate { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime RecLastUpdatedDate { get; set; }

        public virtual Product Product { get; set; }
        public virtual Size Size { get; set; }
    }
}
