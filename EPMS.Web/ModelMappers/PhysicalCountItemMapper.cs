using EPMS.Models.DomainModels;
using EPMS.Web.Models;

namespace EPMS.Web.ModelMappers
{
    public static class PhysicalCountItemMapper
    {
        public static PhysicalCountItemModel CreateFromServerToClient(this PhysicalCountItem source)
        {
            return new PhysicalCountItemModel
            {
                PcItemId = source.PCItemId,
                PcId = source.PCId,
                ItemVariationId = source.ItemVariationId,
                WarehouseId = source.WarehouseId,

                ItemsInPackage = 0,
                TotalItemsCount = 0,
                TotalItemsInPackages = 0,

                NoOfItemInWarehouse = source.NoOfItemInWarehouse,
                NoOfPackagesInWarehouse = source.NoOfPackagesInWarehouse,
                ItemDetailsEn = source.ItemVariation.InventoryItem.ItemNameEn + " - " + source.ItemVariation.SKUCode + " - " + source.ItemVariation.SKUDescriptionEn,
                ItemDetailsAr = source.ItemVariation.InventoryItem.ItemNameAr + " - " + source.ItemVariation.SKUCode + " - " + source.ItemVariation.SKUDescriptionAr,

                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDate = source.RecLastUpdatedDate
            };
        }

        public static PhysicalCountItem CreateFromClientToServer(this PhysicalCountItemModel source)
        {
            return new PhysicalCountItem
            {
                PCItemId = source.PcItemId,
                PCId = source.PcId,
                ItemVariationId = source.ItemVariationId,
                WarehouseId = source.WarehouseId,

                NoOfItemInWarehouse = source.NoOfItemInWarehouse,
                NoOfPackagesInWarehouse = source.NoOfPackagesInWarehouse,

                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDate = source.RecLastUpdatedDate
            };
        }
    }
}