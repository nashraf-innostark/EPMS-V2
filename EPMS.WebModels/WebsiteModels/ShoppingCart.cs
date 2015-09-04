using System;
using System.ComponentModel.DataAnnotations;

namespace EPMS.WebModels.WebsiteModels
{
    public class ShoppingCart
    {
        public long CartId { get; set; }
        public string UserCartId { get; set; }
        public long ProductId { get; set; }
        public long SizeId { get; set; }
        public decimal UnitPrice { get; set; }
        [Required]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDate { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime RecLastUpdatedDate { get; set; }
        public string ItemNameEn { get; set; }
        public string ItemNameAr { get; set; }
        public string SkuCode { get; set; }
        public string ImagePath { get; set; }
        public double ItemTotal { get; set; }
    }
}
