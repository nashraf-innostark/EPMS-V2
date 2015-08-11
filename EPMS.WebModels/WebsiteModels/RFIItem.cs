using System.ComponentModel.DataAnnotations;

namespace EPMS.WebModels.WebsiteModels
{
    public class RFIItem
    {
        public long RFIItemId { get; set; }
        public long RFIId { get; set; }
        public long? ItemVariationId { get; set; }
        public string ItemSKUCode { get; set; }
        [Required(ErrorMessage = "The Item Details field is required")]
        public string ItemDetails { get; set; }
        [Required]
        [Display(Name = "Item Qty")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid quantity")]
        public long ItemQty { get; set; }
        public bool IsItemDescription { get; set; }
        public bool IsItemSKU { get; set; }
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public string PlaceInDepartment { get; set; }
        public string RecCreatedBy { get; set; }
        public System.DateTime RecCreatedDate { get; set; }
        public string RecUpdatedBy { get; set; }
        public System.DateTime RecUpdatedDate { get; set; }
    }
}