using System;
using System.Globalization;
using EPMS.Models.DomainModels;

namespace EPMS.WebModels.ModelMappers
{
    public static class TransferItemsMapper
    {
        public static WebsiteModels.TIRItem CreateFromServerToClient(this TIRItem source)
        {
            var retVal = new WebsiteModels.TIRItem
            {
                ItemId = source.ItemId,
                ItemDetails = source.ItemDetails,
                IsItemDescription = source.IsItemDescription,
                IsItemSKU = source.IsItemSKU,
                ItemQty = source.ItemQty,
                ItemVariationId = source.ItemVariationId,
                TIRId = source.TIRId,
                PlaceInDepartment = source.PlaceInDepartment,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate.ToString("dd/MM/yyyy", new CultureInfo("en")),
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
        public static TIRItem CreateFromClientToServer(this WebsiteModels.TIRItem source)
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
                PlaceInDepartment = source.PlaceInDepartment,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = DateTime.ParseExact(source.RecCreatedDate, "dd/MM/yyyy", new CultureInfo("en")),
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate,
            };
        }
        public static TIRItem CreateFromClientToServer(this WebsiteModels.TIRItem source, long tirId, string createdBy, string createdDate, DateTime updatedDate)
        {
            return new TIRItem
            {
                ItemId = source.ItemId,
                ItemDetails = source.ItemDetails,
                IsItemDescription = source.IsItemDescription,
                IsItemSKU = source.IsItemSKU,
                ItemQty = source.ItemQty,
                ItemVariationId = source.ItemVariationId,
                PlaceInDepartment = source.PlaceInDepartment,
                
                TIRId = tirId,

                RecCreatedBy = createdBy,
                RecCreatedDate = DateTime.ParseExact(createdDate, "dd/MM/yyyy", new CultureInfo("en")),
                RecUpdatedBy = createdBy,
                RecUpdatedDate = updatedDate,
            };
        }
    }
}