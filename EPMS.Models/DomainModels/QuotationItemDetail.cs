using System;

namespace EPMS.Models.DomainModels
{
    public class QuotationItemDetail
    {
        public long ItemId { get; set; }
        public long QuotationId { get; set; }
        public long? ItemVariationId { get; set; }
        public string ItemDetails { get; set; }
        public decimal ItemQuantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime RecCreatedDate { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecLastUpdatedDate { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public bool IsItemDescription { get; set; }
        public bool IsItemSKU { get; set; }

        public virtual ItemVariation ItemVariation { get; set; }
        public virtual Quotation Quotation { get; set; }
    }
}
