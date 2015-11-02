using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EPMS.WebModels.WebsiteModels
{
    public class ItemReleaseDetail
    {
        public ItemReleaseDetail()
        {
            ItemReleaseQuantities = new List<ItemReleaseQuantity>();
        }
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
        public long RequestedQuantity { get; set; }
        public bool IsItemDescription { get; set; }
        public bool IsItemSKU { get; set; }
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public string ItemSKUCode { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDate { get; set; }
        public string RecUpdatedBy { get; set; }
        public DateTime RecUpdatedDate { get; set; }

        public IList<ItemReleaseQuantity> ItemReleaseQuantities { get; set; }
    }
}