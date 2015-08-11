using EPMS.Models.DomainModels;

namespace EPMS.WebModels.ModelMappers
{
    public static class PoItemHistoryMapper
    {
        public static PurchaseOrderItemHistory CreateFromPoItemToPoItemHistory(this PurchaseOrderItem source)
        {
            return new PurchaseOrderItemHistory
            {
                ItemId = source.ItemId,
                ItemDetails = source.ItemDetails,
                IsItemDescription = source.IsItemDescription,
                IsItemSKU = source.IsItemSKU,
                ItemQty = source.ItemQty,
                ItemVariationId = source.ItemVariationId,
                PlaceInDepartment = source.PlaceInDepartment,
                VendorId = source.VendorId,
                PurchaseOrderId = source.PurchaseOrderId,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate,
            };
        }
        public static PurchaseOrderItem CreateFromPoItemHistoryToPoItem(this PurchaseOrderItemHistory source)
        {
            return new PurchaseOrderItem
            {
                ItemId = source.ItemId,
                ItemDetails = source.ItemDetails,
                IsItemDescription = source.IsItemDescription,
                IsItemSKU = source.IsItemSKU,
                ItemQty = source.ItemQty,
                ItemVariationId = source.ItemVariationId,
                PlaceInDepartment = source.PlaceInDepartment,
                VendorId = source.VendorId,
                PurchaseOrderId = source.PurchaseOrderId,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate,
                ItemVariation = source.ItemVariation,
                Vendor = source.Vendor
            };
        }
    }
}
