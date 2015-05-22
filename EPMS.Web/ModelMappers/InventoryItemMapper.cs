using EPMS.Models.RequestModels;
using WebModels = EPMS.Web.Models;
using DomainModels = EPMS.Models.DomainModels;


namespace EPMS.Web.ModelMappers
{
    public static class InventoryItemMapper
    {
        public static WebModels.InventoryItem CreateFromServerToClient(this DomainModels.InventoryItem source)
        {
            return new WebModels.InventoryItem
            {
                ItemId = source.ItemId,
                ItemNameEn = source.ItemNameEn,
                ItemNameAr = source.ItemNameAr,
                ItemImagePath = source.ItemImagePath,
                ItemDescriptionEn = source.ItemDescriptionEn,
                ItemDescriptionAr = source.ItemDescriptionAr,
                HazardousEn = source.HazardousEn,
                HazardousAr = source.HazardousAr,
                UsageEn = source.UsageEn,
                UsageAr = source.UsageAr,
                ReorderLevel = source.ReorderLevel,
                DepartmentId = source.DepartmentId,
                WarehouseID = source.WarehouseID,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
            };
        }

        public static InventoryItemRequest CreateFromClientToServer(this WebModels.InventoryItem source)
        {
            var item = new DomainModels.InventoryItem
            {
                ItemId = source.ItemId,
                ItemNameEn = source.ItemNameEn,
                ItemNameAr = source.ItemNameAr,
                ItemImagePath = source.ItemImagePath,
                ItemDescriptionEn = source.ItemDescriptionEn,
                ItemDescriptionAr = source.ItemDescriptionAr,
                HazardousEn = source.HazardousEn,
                HazardousAr = source.HazardousAr,
                UsageEn = source.UsageEn,
                UsageAr = source.UsageAr,
                ReorderLevel = source.ReorderLevel,
                DepartmentId = source.DepartmentId,
                WarehouseID = source.WarehouseID,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt
            };
            var request = new InventoryItemRequest();
            request.InventoryItem = item;
            return request;
        }
    }
}