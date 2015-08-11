using System;
using System.ComponentModel.DataAnnotations;

namespace EPMS.WebModels.WebsiteModels
{
    public class QuotationItemDetail
    {
        public long ItemId { get; set; }
        public long QuotationId { get; set; }
        [Required]
        public string ItemDetails { get; set; }
        [Required]
        public decimal ItemQuantity { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime? RecCreatedDt { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecUpdatedDt { get; set; }
        public string RecUpdatedBy { get; set; }
    }
}