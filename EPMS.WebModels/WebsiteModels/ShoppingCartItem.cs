using System.ComponentModel.DataAnnotations;

namespace EPMS.WebModels.WebsiteModels
{
    public class ShoppingCartItem
    {
        public long CartItemId { get; set; }
        public long CartId { get; set; }
        public long ProductId { get; set; }
        public long? SizeId { get; set; }
        public decimal UnitPrice { get; set; }
        [Required]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }
        public string ItemNameEn { get; set; }
        public string ItemNameAr { get; set; }
        public string SkuCode { get; set; }
        public string ImagePath { get; set; }
        public double ItemTotal { get; set; }
    }
}
