using EPMS.Models.DomainModels;

namespace EPMS.Models.ModelMapers
{
    public static class RifItemMapper
    {
        public static RIFItem CreateRfiItem(this RIFItem source)
        {
            return new RIFItem
            {
                RIFItemId = source.RIFItemId,
                RIFId = source.RIFId,
                ItemVariationId = source.ItemVariationId,
                ItemDetails = source.ItemDetails,
                ItemQty = source.ItemQty,
                IsItemDescription = source.IsItemDescription,
                IsItemSKU = source.IsItemSKU,
                WarehouseId = source.WarehouseId,
                
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate
            };
        }
    }
}
