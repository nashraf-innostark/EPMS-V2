using EPMS.Models.DomainModels;

namespace EPMS.Models.ModelMapers
{
    public static class DIFMapper
    {
        public static DIFItem CreateRfiItem(this DIFItem source)
        {
            return new DIFItem
            {
                ItemId = source.ItemId,
                DIFId = source.DIFId,
                ItemVariationId = source.ItemVariationId,
                ItemDetails = source.ItemDetails,
                ItemQty = source.ItemQty,
                IsItemDescription = source.IsItemDescription,
                IsItemSKU = source.IsItemSKU,
                PlaceInDepartment = source.PlaceInDepartment,

                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate
            };
        }
    }
}
