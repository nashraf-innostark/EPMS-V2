using System;
using System.Collections.Generic;

namespace EPMS.WebModels.WebsiteModels
{
    [Serializable()]
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            ShoppingCartItems = new List<ShoppingCartItem>();
        }
        public long CartId { get; set; }
        public string UserCartId { get; set; }
        public string TransactionId { get; set; }
        public int Status { get; set; }
        public decimal? AmountPaid { get; set; }
        public string CurrencyCode { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDate { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime RecLastUpdatedDate { get; set; }

        public IList<ShoppingCartItem> ShoppingCartItems { get; set; }
        public IList<ShoppingCartItem> ShoppingCartItemsForPrice { get; set; }
    }
}
