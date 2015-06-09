using System;
using EPMS.Models.DomainModels;

namespace EPMS.Web.ModelMappers
{
    public static class TransferItemsMapper
    {
        public static Models.TIRItem CreateFromServerToClient(this TIRItem source)
        {
            var retVal = new Models.TIRItem
            {
                ItemId = source.ItemId,
                ItemDetails = source.ItemDetails,
                IsItemDescription = source.IsItemDescription,
                IsItemSKU = source.IsItemSKU,
                ItemQty = source.ItemQty,
                ItemVariationId = source.ItemVariationId,
                TIRId = source.TIRId,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate,
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
        public static TIRItem CreateFromClientToServer(this Models.TIRItem source)
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
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate,
            };
        }
        public static TIRItem CreateFromClientToServer(this Models.TIRItem source, long tirId, string createdBy, DateTime createdDate, DateTime updatedDate)
        {
            return new TIRItem
            {
                ItemId = source.ItemId,
                ItemDetails = source.ItemDetails,
                IsItemDescription = source.IsItemDescription,
                IsItemSKU = source.IsItemSKU,
                ItemQty = source.ItemQty,
                ItemVariationId = source.ItemVariationId,
                
                TIRId = tirId,

                RecCreatedBy = createdBy,
                RecCreatedDate = createdDate,
                RecUpdatedBy = createdBy,
                RecUpdatedDate = updatedDate,
            };
        }
    }
}