using System;
using System.ComponentModel.DataAnnotations;
using EPMS.Implementation.Services;

namespace EPMS.Web.Models
{
    public class InventoryItemForVariation
    {
        public long ItemId { get; set; }
        public string ItemCode { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Inventory.InventoryItem), ErrorMessageResourceName = "NameValidation")]
        [StringLength(200, ErrorMessageResourceType = typeof(Resources.Inventory.InventoryItem), ErrorMessageResourceName = "NameLengthValidation")]
        public string ItemNameEn { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Inventory.InventoryItem), ErrorMessageResourceName = "NameValidation")]
        [StringLength(200, ErrorMessageResourceType = typeof(Resources.Inventory.InventoryItem), ErrorMessageResourceName = "NameLengthValidation")]
        public string ItemNameAr { get; set; }
        public string ItemImagePath { get; set; }
        public string ItemDescriptionEn { get; set; }
        public string ItemDescriptionAr { get; set; }
        public string DescriptionForQuotationEn { get; set; }
        public string DescriptionForQuotationAr { get; set; }
        public string HazardousEn { get; set; }
        public string HazardousAr { get; set; }
        public string UsageEn { get; set; }
        public string UsageAr { get; set; }
        public int? ReorderLevel { get; set; }
        public long? DepartmentId { get; set; }
        public long? WarehouseID { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime RecLastUpdatedDt { get; set; }
        public double? AveragePrice { get; set; }
        public double? AverageCost { get; set; }
        public double? AveragePackagePrice { get; set; }
        public double? QuantityInPackage { get; set; }
        public long? QuantityInHand { get; set; }
        public long? QuantitySold { get; set; }
        public string DepartmentPath { get; set; }
        public InventoryDepartment InventoryDepartment { get; set; }
    }
}