﻿using System;
using EPMS.Models.DomainModels;

namespace EPMS.WebModels.ModelMappers
{
    public static class PurchaseItemsMapper
    {
        public static WebsiteModels.PurchaseOrderItem CreateFromServerToClient(this PurchaseOrderItem source)
        {
            var retVal = new WebsiteModels.PurchaseOrderItem
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
            };
            if (source.ItemVariation != null)
            {
                retVal.ItemName = Resources.Shared.Common.TextDirection == "ltr"
                    ? source.ItemVariation.InventoryItem.ItemNameEn
                    : source.ItemVariation.InventoryItem.ItemNameAr;
                retVal.ItemCode = source.ItemVariation.InventoryItem.ItemCode;
                retVal.ItemSKUCode = source.ItemVariation.SKUCode;
                retVal.UnitPrice = source.ItemVariation.UnitPrice ?? 0;
            }
            return retVal;
        }
        public static PurchaseOrderItem CreateFromClientToServer(this WebsiteModels.PurchaseOrderItem source)
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
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate,
            };
        }
        public static PurchaseOrderItem CreateFromClientToServer(this WebsiteModels.PurchaseOrderItem source, long poId, string createdBy, DateTime createdDate, DateTime updatedDate)
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

                PurchaseOrderId = poId,

                RecCreatedBy = createdBy,
                RecCreatedDate = createdDate,
                RecUpdatedBy = createdBy,
                RecUpdatedDate = updatedDate,
            };
        }
    }
}