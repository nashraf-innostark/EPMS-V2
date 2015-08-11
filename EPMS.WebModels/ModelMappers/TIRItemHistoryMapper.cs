using EPMS.Models.DomainModels;

namespace EPMS.WebModels.ModelMappers
{
    public static class TIRItemHistoryMapper
    {
        public static TIRItemHistory CreateFromTirItemToTirItemHistory(this TIRItem source)
        {
            return new TIRItemHistory
            {
                ItemId = source.ItemId,
                ItemDetails = source.ItemDetails,
                IsItemDescription = source.IsItemDescription,
                IsItemSKU = source.IsItemSKU,
                ItemQty = source.ItemQty,
                ItemVariationId = source.ItemVariationId,
                TIRId = source.TIRId,
                PlaceInDepartment = source.PlaceInDepartment,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate,
                ItemVariation = source.ItemVariation,
            };
        }
        public static TIRItem CreateFromTirItemHistoryToTirItem(this TIRItemHistory source)
        {
            return new TIRItem
            {
                ItemId = source.ItemId,
                ItemDetails = source.ItemDetails,
                IsItemDescription = source.IsItemDescription,
                IsItemSKU = source.IsItemSKU,
                ItemQty = source.ItemQty,
                ItemVariationId = source.ItemVariationId,
                TIRId = source.TIRId,
                PlaceInDepartment = source.PlaceInDepartment,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate,
                ItemVariation = source.ItemVariation,
            };
        }
    }
}
