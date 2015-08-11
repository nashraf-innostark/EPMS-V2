using EPMS.Models.DomainModels;

namespace EPMS.WebModels.ModelMappers
{
    public static class RifItemHistoryMapper
    {
        public static RIFItemHistory CreateFromRifItemToRifItemHistory(this RIFItem source)
        {
            return new RIFItemHistory
            {
                RIFItemId = source.RIFItemId,
                RIFId = source.RIFId,
                ItemVariationId = source.ItemVariationId == 0 ? null : source.ItemVariationId,
                IsItemDescription = source.IsItemDescription,
                IsItemSKU = source.IsItemSKU,
                ItemQty = source.ItemQty,
                ItemDetails = source.ItemDetails,
                PlaceInDepartment = source.PlaceInDepartment,

                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate,
                ItemVariation = source.ItemVariation,
            };
        }
        public static RIFItem CreateFromRifItemHistoryToRifItem(this RIFItemHistory source)
        {
            return new RIFItem
            {
                RIFItemId = source.RIFItemId,
                RIFId = source.RIFId,
                ItemVariationId = source.ItemVariationId == 0 ? null : source.ItemVariationId,
                IsItemDescription = source.IsItemDescription,
                IsItemSKU = source.IsItemSKU,
                ItemQty = source.ItemQty,
                ItemDetails = source.ItemDetails,
                PlaceInDepartment = source.PlaceInDepartment,

                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate,
                ItemVariation = source.ItemVariation,
                RIF = source.RIFHistory.CreateFromRifHistoryToRif(),
            };
        }
    }
}
