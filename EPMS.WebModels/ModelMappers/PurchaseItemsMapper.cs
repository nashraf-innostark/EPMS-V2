﻿using System;
using System.Globalization;
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
                RecCreatedDateStr = source.RecCreatedDate.ToString("dd/MM/yyyy", new CultureInfo("en")),
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
                UnitPrice = source.UnitPrice,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = !string.IsNullOrEmpty(source.RecCreatedDateStr) ? DateTime.ParseExact(source.RecCreatedDateStr, "dd/MM/yyyy", new CultureInfo("en")) : source.RecCreatedDate,
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate,
            };
        }
        public static PurchaseOrderItem CreateFromClientToServer(this WebsiteModels.PurchaseOrderItem source, long poId, string createdBy, string createdDate, DateTime updatedDate)
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
                RecCreatedDate = DateTime.ParseExact(createdDate, "dd/MM/yyyy", new CultureInfo("en")),
                RecUpdatedBy = createdBy,
                RecUpdatedDate = updatedDate,
            };
        }
    }
}