using System;
using System.ComponentModel.DataAnnotations;

namespace EPMS.Web.Models
{
    public class ItemReleaseDetail
    {
        public long IRFDetailId { get; set; }
        public long ItemReleaseId { get; set; }
        [Required(ErrorMessage = "The Item Details field is required")]
        public string ItemDetails { get; set; }
        public long? ItemVariationId { get; set; }
        public string PlaceInDepartment { get; set; }
        [Required(ErrorMessage = "The Warehouse field is required.")]
        public string PlaceInWarehouse { get; set; }
        [Required]
        [Display(Name = "Item Qty")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid quantity")]
        public long ItemQty { get; set; }
        public bool IsItemDescription { get; set; }
        public bool IsItemSKU { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDate { get; set; }
        public string RecUpdatedBy { get; set; }
        public DateTime RecUpdatedDate { get; set; }
    }
}