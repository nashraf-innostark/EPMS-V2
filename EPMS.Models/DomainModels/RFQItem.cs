using System;

namespace EPMS.Models.DomainModels
{
    public class RFQItem
    {
        public long ItemId { get; set; }
        public long RFQId { get; set; }
        public string ItemDetails { get; set; }
        public decimal ItemQuantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDate { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime RecLastUpdatedDate { get; set; }

        public virtual RFQ RFQ { get; set; }
    }
}
