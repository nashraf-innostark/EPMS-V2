using System;

namespace EPMS.WebModels.WebsiteModels
{
    public class ShoppingCart
    {
        public long CartId { get; set; }
        public string UserCartId { get; set; }
        public long ProductId { get; set; }
        public int Quantity { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDate { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime RecLastUpdatedDate { get; set; }
        public int Size { get; set; }
    }
}
