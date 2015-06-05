using EPMS.Models.DomainModels;

namespace EPMS.Web.ModelMappers
{
    public static class ItemReleaseDetailMapper
    {
        public static Models.ItemReleaseDetail CreateFromServerToClient(this ItemReleaseDetail source)
        {
            return new Models.ItemReleaseDetail
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
            };
        }
        public static ItemReleaseDetail CreateFromClientToServer(this Models.ItemReleaseDetail source)
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
            };
        }
    }
}