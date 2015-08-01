using System;
using System.Linq;
using EPMS.Models.DomainModels;
using EPMS.Web.Models;

namespace EPMS.Web.ModelMappers
{
    public static class PhysicalCountItemMapper
    {
        public static PhysicalCountItemModel CreateFromServerToClient(this PhysicalCountItem source)
        {
            PhysicalCountItemModel retVal = new PhysicalCountItemModel
            {
                PcItemId = source.PCItemId,
                PcId = source.PCId,
                ItemVariationId = source.ItemVariationId,
                WarehouseId = source.WarehouseId,

                NoOfItemInWarehouse = source.NoOfItemInWarehouse,
                NoOfPackagesInWarehouse = source.NoOfPackagesInWarehouse,
                ItemDetailsEn = source.ItemVariation.InventoryItem.ItemNameEn + " - " + source.ItemVariation.SKUDescriptionEn,
                ItemDetailsAr = source.ItemVariation.InventoryItem.ItemNameAr + " - " + source.ItemVariation.SKUDescriptionAr,

                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDate = source.RecLastUpdatedDate,

                ItemBarcode = source.ItemVariation.ItemBarcode,
                ItemsInPackage = source.ItemVariation.InventoryItem.QuantityInPackage ?? 0,
                TotalItemsCountInWarehouse = source.ItemVariation.ItemWarehouses.Sum(x=>x.Quantity)
            };
            retVal.TotalItemsInPackages = retVal.NoOfPackagesInWarehouse * Convert.ToInt64(retVal.ItemsInPackage);
            retVal.TotalItemsCount = retVal.TotalItemsInPackages + retVal.NoOfItemInWarehouse;
            return retVal;
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