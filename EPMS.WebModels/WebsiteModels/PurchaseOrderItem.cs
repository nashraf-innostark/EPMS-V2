using System;
using System.ComponentModel.DataAnnotations;
using EPMS.WebModels.Resources.Inventory.PO;

namespace EPMS.WebModels.WebsiteModels
{
    public class PurchaseOrderItem
    {
        public long ItemId { get; set; }
        public long PurchaseOrderId { get; set; }
        public long? ItemVariationId { get; set; }
        public string ItemDetails { get; set; }
        public long ItemQty { get; set; }
        public bool IsItemDescription { get; set; }
        public bool IsItemSKU { get; set; }
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public string ItemSKUCode { get; set; }
        [Required]
        [Display(ResourceType = typeof (PO), Name = "PurchaseOrderItem_UnitPrice_Unit_Price")]
        public decimal? UnitPrice { get; set; }
        public decimal? Total { get; set; }
        public string PlaceInDepartment { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDate { get; set; }
        public string RecUpdatedBy { get; set; }
        public DateTime RecUpdatedDate { get; set; }
        public long? VendorId { get; set; }
        
    }
}