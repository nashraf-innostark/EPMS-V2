using EPMS.Models.DomainModels;

namespace EPMS.WebModels.ModelMappers
{
    public static class RfiItemMapper
    {
        public static RFIItem CreateRfiItem(this RFIItem source)
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
                RecUpdatedDate = source.RecUpdatedDate
            };
        }
    }
}
