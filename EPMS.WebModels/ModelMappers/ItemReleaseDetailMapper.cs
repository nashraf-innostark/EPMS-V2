using System.Collections.Generic;
using System.Linq;
using EPMS.Models.DomainModels;

namespace EPMS.WebModels.ModelMappers
{
    public static class ItemReleaseDetailMapper
    {
        public static WebsiteModels.ItemReleaseDetail CreateFromServerToClient(this ItemReleaseDetail source)
        {
            var itemDetails = source.ItemDetails;
            itemDetails = itemDetails.Replace("\r", " ");
            itemDetails = itemDetails.Replace("\t", " ");
            itemDetails = itemDetails.Replace("\n", " , ");
            var retVal = new WebsiteModels.ItemReleaseDetail
            {
                IRFDetailId = source.IRFDetailId,
                ItemReleaseId = source.ItemReleaseId,
                ItemDetails = itemDetails,
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
                ItemReleaseQuantities = source.ItemReleaseQuantities != null ? source.ItemReleaseQuantities.Select(x => x.CreateFromServerToClient()).ToList() : new List<WebsiteModels.ItemReleaseQuantity>()
            };
            if (source.ItemVariation != null)
            {
                retVal.ItemName = Resources.Shared.Common.TextDirection == "ltr"
                    ? source.ItemVariation.InventoryItem.ItemNameEn
                    : source.ItemVariation.InventoryItem.ItemNameAr;
                retVal.ItemCode = source.ItemVariation.InventoryItem.ItemCode;
                retVal.ItemSKUCode = source.ItemVariation.SKUCode;
            }
            var rfi = source.ItemRelease.RFI;
            if (rfi != null && rfi.RFIItems.Any())
            {
                retVal.RequestedQuantity =
                    rfi.RFIItems.FirstOrDefault(x => x.ItemVariationId == retVal.ItemVariationId).ItemQty;
            }
            return retVal;
        }
        public static ItemReleaseDetail CreateFromClientToServer(this WebsiteModels.ItemReleaseDetail source)
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
                ItemReleaseQuantities = source.ItemReleaseQuantities.Select(x => x.CreateFromServerToClient(source.IRFDetailId)).ToList()
            };
        }
    }
}