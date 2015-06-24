using System;
using System.Collections.Generic;

namespace EPMS.Web.Models
{
    public class InventoryItem
    {
        public long ItemId { get; set; }
        public string ItemCode { get; set; }
        public string ItemNameEn { get; set; }
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
        public string QuantityInPackage { get; set; }
        public string QuantityInHand { get; set; }
        public string QuantitySold { get; set; }

        public List<ItemVariation> ItemVariations { get; set; }
    }
}