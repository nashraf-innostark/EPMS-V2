using EPMS.Models.DomainModels;

namespace EPMS.Models.ModelMapers
{
    public static class RFIItemHistoryMapper
    {
        public static RFIItemHistory CreateRfiItemToRfiItemHistory(this RFIItem source)
        {
            return new RFIItemHistory
            {
                RFIItemId = source.RFIItemId,
                RFIId = source.RFIId,
                ItemVariationId = source.ItemVariationId,
                ItemDetails = source.ItemDetails,
                ItemQty = source.ItemQty,
                IsItemDescription = source.IsItemDescription,
                IsItemSKU = source.IsItemSKU,

                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate,
                ItemVariation = source.ItemVariation,
                PlaceInDepartment = source.PlaceInDepartment,
                PlaceInDepartment1 = source.PlaceInDepartment,
            };
        }
        public static RFIItem CreateRfiItemHistoryToRfiItem(this RFIItemHistory source)
        {
            return new RFIItem
            {
                RFIItemId = source.RFIItemId,
                RFIId = source.RFIId,
                ItemVariationId = source.ItemVariationId,
                ItemDetails = source.ItemDetails,
                ItemQty = source.ItemQty,
                IsItemDescription = source.IsItemDescription,
                IsItemSKU = source.IsItemSKU,

                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate,
                ItemVariation = source.ItemVariation,
                PlaceInDepartment = source.PlaceInDepartment,
            };
        }
    }
}
