using System;
using System.Collections.Generic;

namespace EPMS.Models.DomainModels
{
    public class ShoppingCart
    {
        public long CartId { get; set; }
        public string UserCartId { get; set; }
        public string TransactionId { get; set; }
        public bool? Status { get; set; }
        public decimal? AmountPaid { get; set; }
        public string CurrencyCode { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDate { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime RecLastUpdatedDate { get; set; }

        public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
