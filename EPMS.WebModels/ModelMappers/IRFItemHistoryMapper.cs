using EPMS.Models.DomainModels;

namespace EPMS.WebModels.ModelMappers
{
    public static class IRFItemHistoryMapper
    {
        public static ItemReleaseDetailHistory CreateFromIrfDetailToIrfDetailHistory(this ItemReleaseDetail source)
        {
            return new ItemReleaseDetailHistory
            {
                IRFDetailId = source.IRFDetailId,
                ItemReleaseId = source.ItemReleaseId,
                ItemDetails = source.ItemDetails,
                IsItemDescription = source.IsItemDescription,
                IsItemSKU = source.IsItemDescription,
                ItemVariationId = source.ItemVariationId,
                ItemQty = source.ItemQty,
                PlaceInDepartment = source.PlaceInDepartment,
                PlaceInWarehouse = source.PlaceInWarehouse,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate,
                ItemVariation = source.ItemVariation,
            };
        }
        public static ItemReleaseDetail CreateFromIrfDetailHistoryToIrfDetail(this ItemReleaseDetailHistory source)
        {
            return new ItemReleaseDetail
            {
                IRFDetailId = source.IRFDetailId,
                ItemReleaseId = source.ItemReleaseId,
                ItemDetails = source.ItemDetails,
                IsItemDescription = source.IsItemDescription,
                IsItemSKU = source.IsItemDescription,
                ItemVariationId = source.ItemVariationId,
                ItemQty = source.ItemQty,
                PlaceInDepartment = source.PlaceInDepartment,
                PlaceInWarehouse = source.PlaceInWarehouse,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate,
                ItemVariation = source.ItemVariation,
            };
        }
    }
}
