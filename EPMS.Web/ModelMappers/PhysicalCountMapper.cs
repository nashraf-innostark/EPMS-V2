using EPMS.Models.DomainModels;
using EPMS.Web.Models;

namespace EPMS.Web.ModelMappers
{
    public static class PhysicalCountMapper
    {
        public static PhysicalCountListModel CreateListFromServerToClient(this PhysicalCount source)
        {
            return new PhysicalCountListModel
            {
                PCId = source.PCId
            };
        }
        //public static PhysicalCountModel CreateFromServerToClient(this PhysicalCount source)
        //{
        //    return new PhysicalCountModel
        //    {
        //        PCId = source.PCId,
        //        ItemVariationId = source.ItemVariationId,
        //        WarehouseId = source.WarehouseId,
                
        //        ItemsInPackage = 0,
        //        TotalItemsCount = 0,
        //        TotalItemsInPackages = 0,
                
        //        NoOfItemInWarehouse = source.NoOfItemInWarehouse,
        //        NoOfPackagesInWarehouse = source.NoOfPackagesInWarehouse,
        //        ItemDetailsEn = source.ItemVariation.InventoryItem.ItemNameEn+" - "+source.ItemVariation.SKUCode + " - "+source.ItemVariation.SKUDescriptionEn,
        //        ItemDetailsAr = source.ItemVariation.InventoryItem.ItemNameAr+" - "+source.ItemVariation.SKUCode + " - "+source.ItemVariation.SKUDescriptionAr,

        //        RecCreatedBy = source.RecCreatedBy,
        //        RecCreatedDate = source.RecCreatedDate,
        //        RecLastUpdatedBy = source.RecLastUpdatedBy,
        //        RecLastUpdatedDate = source.RecLastUpdatedDate
        //    };
        //}

        //public static PhysicalCount CreateFromClientToServer(this PhysicalCountModel source)
        //{
        //    return new PhysicalCount
        //    {
        //        PCId = source.PCId,
        //        ItemVariationId = source.ItemVariationId,
        //        WarehouseId = source.WarehouseId,

        //        NoOfItemInWarehouse = source.NoOfItemInWarehouse,
        //        NoOfPackagesInWarehouse = source.NoOfPackagesInWarehouse,
 
        //        RecCreatedBy = source.RecCreatedBy,
        //        RecCreatedDate = source.RecCreatedDate,
        //        RecLastUpdatedBy = source.RecLastUpdatedBy,
        //        RecLastUpdatedDate = source.RecLastUpdatedDate
        //    };
        //}
    }
}