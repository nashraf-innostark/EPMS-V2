using System;
using System.ComponentModel.DataAnnotations;

namespace EPMS.WebModels.WebsiteModels
{
    public class QuotationItemDetail
    {
        public long ItemId { get; set; }
        public long QuotationId { get; set; }
        public long? ItemVariationId { get; set; }
        [Required]
        public string ItemDetails { get; set; }
        [Required]
        public decimal ItemQuantity { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime RecCreatedDate { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecLastUpdatedDate { get; set; }
        public string RecLastUpdatedBy { get; set; }
    }
}