using EPMS.Models.DomainModels;

namespace EPMS.Models.ModelMapers
{
    public static class DIFItemHistoryMapper
    {
        public static DIFItemHistory CreateFromDifItemToDifItemHistory(this DIFItem source)
        {
            var rifItem = new DIFItemHistory
            {
                ItemId = source.ItemId,
                DIFId = source.DIFId,
                IsItemDescription = source.IsItemDescription,
                IsItemSKU = source.IsItemSKU,
                ItemQty = source.ItemQty,
                ItemDetails = source.ItemDetails,
                PlaceInDepartment = source.PlaceInDepartment,
                ItemVariationId = source.ItemVariationId,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate,
            };
            return rifItem;
        }
    }
}
