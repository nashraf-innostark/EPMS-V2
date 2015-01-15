using System;

namespace EPMS.Web.Models
{
    public class QuotationItemDetail
    {
        public long ItemId { get; set; }
        public long QuotationId { get; set; }
        public string ItemDetails { get; set; }
        public decimal ItemQuantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime? RecCreatedDt { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecUpdatedDt { get; set; }
        public string RecUpdatedBy { get; set; }
    }
}