using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EPMS.Web.Models
{
    public class RIFItem
    {
        public long RIFItemId { get; set; }
        public long RIFId { get; set; }
        public long? ItemVariationId { get; set; }
        public string ItemSKUCode { get; set; }
        [Required(ErrorMessage = "The Item Details field is required")]
        public string ItemDetails { get; set; }
        [Required]
        [Display(Name = "Item Qty")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid quantity")]
        public long ItemQty { get; set; }
        public long ReleasedQty { get; set; }
        public bool IsItemDescription { get; set; }
        public bool IsItemSKU { get; set; }
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public string PlaceInDepartment { get; set; }
        public string RecCreatedBy { get; set; }
        public System.DateTime RecCreatedDate { get; set; }
        public string RecUpdatedBy { get; set; }
        public System.DateTime RecUpdatedDate { get; set; }
        [Required]
        [Display(ResourceType = typeof (Resources.Inventory.RIF.RIF), Name = "RIFItem_WarehouseId_Warehouse")]
        public long WarehouseId { get; set; }

        public IEnumerable<ItemWarehouse> ItemWarehouses { get; set; }
    }
}