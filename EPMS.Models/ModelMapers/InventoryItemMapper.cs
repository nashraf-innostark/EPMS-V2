using EPMS.Models.DomainModels;

namespace EPMS.Models.ModelMapers
{
    public static class InventoryItemMapper
    {
        public static InventoryItem UpdateDbDataFromClient(this InventoryItem destination, InventoryItem source)
        {
            destination.ItemId = source.ItemId;
            destination.ItemCode = source.ItemCode;
            destination.ItemNameEn = source.ItemNameEn;
            destination.ItemNameAr = source.ItemNameAr;
            destination.ItemImagePath = source.ItemImagePath;
            destination.ItemDescriptionEn = source.ItemDescriptionEn;
            destination.ItemDescriptionAr = source.ItemDescriptionAr;
            destination.DescriptionForQuotationEn = source.DescriptionForQuotationEn;
            destination.DescriptionForQuotationAr = source.DescriptionForQuotationAr;
            destination.HazardousEn = source.HazardousEn;
            destination.HazardousAr = source.HazardousAr;
            destination.UsageEn = source.UsageEn;
            destination.UsageAr = source.UsageAr;
            destination.ReorderLevel = source.ReorderLevel;
            destination.DepartmentId = source.DepartmentId;
            destination.WarehouseID = source.WarehouseID;
            destination.RecLastUpdatedBy = source.RecLastUpdatedBy;
            destination.RecLastUpdatedDt = source.RecLastUpdatedDt;
            destination.DepartmentPath = source.DepartmentPath;
            destination.QuantityInPackage = source.QuantityInPackage;
            return destination;
        }
    }
}
