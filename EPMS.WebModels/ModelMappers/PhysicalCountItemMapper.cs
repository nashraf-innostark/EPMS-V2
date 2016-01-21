using System;
using System.Globalization;
using System.Linq;
using EPMS.Models.DomainModels;

namespace EPMS.WebModels.ModelMappers
{
    public static class PhysicalCountItemMapper
    {
        public static WebsiteModels.PhysicalCountItemModel CreateFromServerToClient(this PhysicalCountItem source)
        {
            WebsiteModels.PhysicalCountItemModel retVal = new WebsiteModels.PhysicalCountItemModel
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
                RecCreatedDate = source.RecCreatedDate.ToString("dd/MM/yyyy", new CultureInfo("en")),
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDate = source.RecLastUpdatedDate,

                ItemBarcode = source.ItemVariation.ItemBarcode,
                ItemsInPackage = source.ItemVariation.InventoryItem.QuantityInPackage ?? 0,
                //TotalItemsCountInWarehouse = source.ItemVariation.ItemWarehouses.Sum(x=>x.Quantity),
                TotalItemsCountInWarehouse = source.ItemVariation.ItemWarehouses.Sum(x=>x.Quantity) - source.ItemVariation.ItemReleaseQuantities.Where(y=>y.WarehouseId == source.WarehouseId).Sum(x=>x.Quantity)
            };
            retVal.TotalItemsInPackages = retVal.NoOfPackagesInWarehouse * Convert.ToInt64(retVal.ItemsInPackage);
            retVal.TotalItemsCount = retVal.TotalItemsInPackages + retVal.NoOfItemInWarehouse;
            return retVal;
        }

        public static PhysicalCountItem CreateFromClientToServer(this WebsiteModels.PhysicalCountItemModel source)
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
                RecCreatedDate = DateTime.ParseExact(source.RecCreatedDate, "dd/MM/yyyy", new CultureInfo("en")),
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDate = source.RecLastUpdatedDate
            };
        }
    }
}