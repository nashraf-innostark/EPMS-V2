using System;
using EPMS.Models.DomainModels;

namespace EPMS.Web.ModelMappers
{
    public static class PurchaseItemsMapper
    {
        public static Models.PurchaseOrderItem CreateFromServerToClient(this PurchaseOrderItem source)
        {
            var retVal = new Models.PurchaseOrderItem
            {
                ItemId = source.ItemId,
                ItemDetails = source.ItemDetails,
                IsItemDescription = source.IsItemDescription,
                IsItemSKU = source.IsItemSKU,
                ItemQty = source.ItemQty,
                ItemVariationId = source.ItemVariationId,
                PurchaseOrderId = source.PurchaseOrderId,
                PlaceInDepartment = source.PlaceInDepartment,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate,
                VendorId = Convert.ToInt64(source.VendorId),
                UnitPrice = source.UnitPrice,
                Total = (source.ItemQty != 0 && source.UnitPrice != null) ? source.ItemQty * source.UnitPrice : 0,
            };
            if (source.ItemVariation != null)
            {
                retVal.ItemName = Resources.Shared.Common.TextDirection == "ltr"
                    ? source.ItemVariation.InventoryItem.ItemNameEn
                    : source.ItemVariation.InventoryItem.ItemNameAr;
                retVal.ItemCode = source.ItemVariation.InventoryItem.ItemCode;
                retVal.ItemSKUCode = source.ItemVariation.SKUCode;
            }
            return retVal;
        }
        public static PurchaseOrderItem CreateFromClientToServer(this Models.PurchaseOrderItem source)
        {
            return new PurchaseOrderItem
            {
                ItemId = source.ItemId,
                ItemDetails = source.ItemDetails,
                IsItemDescription = source.IsItemDescription,
                IsItemSKU = source.IsItemSKU,
                ItemQty = source.ItemQty,
                ItemVariationId = source.ItemVariationId,
                PurchaseOrderId = source.PurchaseOrderId,
                PlaceInDepartment = source.PlaceInDepartment,
                VendorId = source.VendorId,
                UnitPrice = source.UnitPrice,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate,
            };
        }
        public static PurchaseOrderItem CreateFromClientToServer(this Models.PurchaseOrderItem source, long poId, string createdBy, DateTime createdDate, DateTime updatedDate)
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
                UnitPrice = source.UnitPrice,

                PurchaseOrderId = poId,

                RecCreatedBy = createdBy,
                RecCreatedDate = createdDate,
                RecUpdatedBy = createdBy,
                RecUpdatedDate = updatedDate,
            };
        }
    }
}