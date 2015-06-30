using System;
using System.Linq;
using EPMS.Models.RequestModels;
using iTextSharp.text.pdf;
using WebModels = EPMS.Web.Models;
using DomainModels = EPMS.Models.DomainModels;


namespace EPMS.Web.ModelMappers
{
    public static class InventoryItemMapper
    {
        public static WebModels.InventoryItem CreateFromServerToClient(this DomainModels.InventoryItem source)
        {
            WebModels.InventoryItem inventoryItem = new WebModels.InventoryItem();

            inventoryItem.ItemId = source.ItemId;
            inventoryItem.ItemCode = source.ItemCode;
            inventoryItem.ItemNameEn = source.ItemNameEn;
            inventoryItem.ItemNameAr = source.ItemNameAr;
            inventoryItem.ItemImagePath = source.ItemImagePath;
            inventoryItem.ItemDescriptionEn = source.ItemDescriptionEn;
            inventoryItem.ItemDescriptionAr = source.ItemDescriptionAr;
            inventoryItem.DescriptionForQuotationEn = source.DescriptionForQuotationEn;
            inventoryItem.DescriptionForQuotationAr = source.DescriptionForQuotationAr;
            inventoryItem.HazardousEn = source.HazardousEn;
            inventoryItem.HazardousAr = source.HazardousAr;
            inventoryItem.UsageEn = source.UsageEn;
            inventoryItem.UsageAr = source.UsageAr;
            inventoryItem.ReorderLevel = source.ReorderLevel;
            inventoryItem.DepartmentId = source.DepartmentId;
            inventoryItem.WarehouseID = source.WarehouseID;
            inventoryItem.RecCreatedBy = source.RecCreatedBy;
            inventoryItem.RecCreatedDt = source.RecCreatedDt;
            inventoryItem.RecLastUpdatedBy = source.RecLastUpdatedBy;
            inventoryItem.RecLastUpdatedDt = source.RecLastUpdatedDt;
            inventoryItem.ItemVariations = source.ItemVariations.Select(x => x.CreateFromServerToClient()).ToList();
            inventoryItem.AveragePrice = 0;
            inventoryItem.AveragePrice = source.ItemVariations.Where(x => x.PriceCalculation).Sum(y => y.UnitPrice / source.ItemVariations.Count(z => z.PriceCalculation));
            inventoryItem.AveragePackagePrice = source.ItemVariations.Sum(y => y.PackagePrice / source.ItemVariations.Count());
            inventoryItem.QuantityInHand = source.ItemVariations.Sum(x => Convert.ToInt64(x.QuantityInHand));
            inventoryItem.QuantitySold = source.ItemVariations.Sum(x => Convert.ToInt64(x.QuantitySold));
            return inventoryItem;
        }

        public static InventoryItemRequest CreateFromClientToServer(this WebModels.InventoryItem source)
        {
            var item = new DomainModels.InventoryItem
            {
                ItemId = source.ItemId,
                ItemCode = source.ItemCode,
                ItemNameEn = source.ItemNameEn,
                ItemNameAr = source.ItemNameAr,
                ItemImagePath = source.ItemImagePath,
                ItemDescriptionEn = source.ItemDescriptionEn,
                ItemDescriptionAr = source.ItemDescriptionAr,
                DescriptionForQuotationEn = source.DescriptionForQuotationEn,
                DescriptionForQuotationAr = source.DescriptionForQuotationAr,
                HazardousEn = source.HazardousEn,
                HazardousAr = source.HazardousAr,
                UsageEn = source.UsageEn,
                UsageAr = source.UsageAr,
                ReorderLevel = source.ReorderLevel,
                DepartmentId = source.DepartmentId,
                WarehouseID = source.WarehouseID,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt
            };
            var request = new InventoryItemRequest();
            request.InventoryItem = item;
            return request;
        }
    }
}