using System;
using System.Globalization;
using System.Linq;
using EPMS.Models.DashboardModels;
using EPMS.Models.RequestModels;

namespace EPMS.WebModels.ModelMappers
{
    public static class InventoryItemMapper
    {
        public static WebsiteModels.InventoryItemForListView CreateListFromServerToClient(this Models.DomainModels.InventoryItem source)
        {
            var inventoryItem = new WebsiteModels.InventoryItemForListView();

            inventoryItem.ItemId = source.ItemId;
            inventoryItem.ItemCode = source.ItemCode;
            //inventoryItem.ItemNameEn = source.ItemNameEn;
            //inventoryItem.ItemNameAr = source.ItemNameAr;
            inventoryItem.ItemNameEn = CultureInfo.CurrentCulture.Name == "en" ? source.ItemNameEn : source.ItemNameAr;
            inventoryItem.ItemImagePath = source.ItemImagePath;
            var descE = source.ItemDescriptionEn;
            if (!string.IsNullOrEmpty(descE))
            {
                descE = descE.Replace("\r", "");
                descE = descE.Replace("\t", "");
                descE = descE.Replace("\n", "");
            }
            //inventoryItem.ItemDescriptionEn = descE;
            var descA = source.ItemDescriptionAr;
            if (!string.IsNullOrEmpty(descA))
            {
                descA = descA.Replace("\r", "");
                descA = descA.Replace("\t", "");
                descA = descA.Replace("\n", "");
            }
            //inventoryItem.ItemDescriptionAr = descA;
            inventoryItem.ItemDescriptionEn = CultureInfo.CurrentCulture.Name == "en" ? descE : descA;
            inventoryItem.DescriptionForQuotationEn = source.DescriptionForQuotationEn;
            inventoryItem.DescriptionForQuotationAr = source.DescriptionForQuotationAr;
            var hazE = source.HazardousEn;
            if (!string.IsNullOrEmpty(hazE))
            {
                hazE = hazE.Replace("\r", "");
                hazE = hazE.Replace("\t", "");
                hazE = hazE.Replace("\n", "");
            }
            inventoryItem.HazardousEn = hazE;
            var hazA = source.HazardousAr;
            if (!string.IsNullOrEmpty(hazA))
            {
                hazA = hazA.Replace("\r", "");
                hazA = hazA.Replace("\t", "");
                hazA = hazA.Replace("\n", "");
            }
            inventoryItem.HazardousAr = hazA;
            var usageE = source.UsageEn;
            if (!string.IsNullOrEmpty(usageE))
            {
                usageE = usageE.Replace("\r", "");
                usageE = usageE.Replace("\t", "");
                usageE = usageE.Replace("\n", "");
            }
            inventoryItem.UsageEn = usageE;
            var usageA = source.UsageAr;
            if (!string.IsNullOrEmpty(usageA))
            {
                usageA = usageA.Replace("\r", "");
                usageA = usageA.Replace("\t", "");
                usageA = usageA.Replace("\n", "");
            }
            inventoryItem.UsageAr = usageA;
            inventoryItem.ReorderLevel = source.ReorderLevel;
            inventoryItem.DepartmentId = source.DepartmentId;
            inventoryItem.WarehouseID = source.WarehouseID;
            inventoryItem.AveragePrice = 0;
            double? sum = source.ItemVariations.Where(x => x.PriceCalculation).Sum(y => y.UnitPrice / source.ItemVariations.Count(z => z.PriceCalculation));
            if (
                sum != null)
                inventoryItem.AveragePrice = Math.Round((double)sum, 2);
            inventoryItem.AverageCost = 0;
            inventoryItem.AverageCost = Math.Round((double)(source.ItemVariations.Where(x => x.CostCalculation).Sum(y => y.UnitCost / source.ItemVariations.Count(z => z.CostCalculation))), 2);
            inventoryItem.AveragePackagePrice = source.ItemVariations.Sum(y => y.PackagePrice / source.ItemVariations.Count());
            //inventoryItem.QuantityInHand = source.QuantityInHand;
            inventoryItem.QuantityInHand = source.QuantityInHand;
            //inventoryItem.QuantityInHand = source.ItemVariations.Sum(x => Convert.ToInt64(x.QuantityInHand));
            inventoryItem.QuantitySold = source.ItemVariations.Sum(x => Convert.ToInt64(x.ItemReleaseQuantities.Sum(y => y.Quantity)));
            inventoryItem.DepartmentPath = source.DepartmentPath;
            inventoryItem.QuantityInPackage = source.ItemVariations.Sum(x => x.QuantityInPackage);
            return inventoryItem;
        }
        public static WebsiteModels.InventoryItem CreateFromServerToClient(this Models.DomainModels.InventoryItem source)
        {
            WebsiteModels.InventoryItem inventoryItem = new WebsiteModels.InventoryItem();

            inventoryItem.ItemId = source.ItemId;
            inventoryItem.ItemCode = source.ItemCode;
            inventoryItem.ItemNameEn = source.ItemNameEn;
            inventoryItem.ItemNameAr = source.ItemNameAr;
            inventoryItem.ItemImagePath = source.ItemImagePath;
            var descE = source.ItemDescriptionEn;
            if (!string.IsNullOrEmpty(descE))
            {
                descE = descE.Replace("\r", "");
                descE = descE.Replace("\t", "");
                descE = descE.Replace("\n", "");
            }
            inventoryItem.ItemDescriptionEn = descE;
            var descA = source.ItemDescriptionAr;
            if (!string.IsNullOrEmpty(descA))
            {
                descA = descA.Replace("\r", "");
                descA = descA.Replace("\t", "");
                descA = descA.Replace("\n", "");
            }
            inventoryItem.ItemDescriptionAr = descA;
            inventoryItem.DescriptionForQuotationEn = source.DescriptionForQuotationEn;
            inventoryItem.DescriptionForQuotationAr = source.DescriptionForQuotationAr;
            var hazE = source.HazardousEn;
            if (!string.IsNullOrEmpty(hazE))
            {
                hazE = hazE.Replace("\r", "");
                hazE = hazE.Replace("\t", "");
                hazE = hazE.Replace("\n", "");
            }
            inventoryItem.HazardousEn = hazE;
            var hazA = source.HazardousAr;
            if (!string.IsNullOrEmpty(hazA))
            {
                hazA = hazA.Replace("\r", "");
                hazA = hazA.Replace("\t", "");
                hazA = hazA.Replace("\n", "");
            }
            inventoryItem.HazardousAr = hazA;
            var usageE = source.UsageEn;
            if (!string.IsNullOrEmpty(usageE))
            {
                usageE = usageE.Replace("\r", "");
                usageE = usageE.Replace("\t", "");
                usageE = usageE.Replace("\n", "");
            }
            inventoryItem.UsageEn = usageE;
            var usageA = source.UsageAr;
            if (!string.IsNullOrEmpty(usageA))
            {
                usageA = usageA.Replace("\r", "");
                usageA = usageA.Replace("\t", "");
                usageA = usageA.Replace("\n", "");
            }
            inventoryItem.UsageAr = usageA;
            inventoryItem.ReorderLevel = source.ReorderLevel;
            inventoryItem.DepartmentId = source.DepartmentId;
            inventoryItem.WarehouseID = source.WarehouseID;
            inventoryItem.RecCreatedBy = source.RecCreatedBy;
            inventoryItem.RecCreatedDt = source.RecCreatedDt;
            inventoryItem.RecLastUpdatedBy = source.RecLastUpdatedBy;
            inventoryItem.RecLastUpdatedDt = source.RecLastUpdatedDt;
            inventoryItem.ItemVariations = source.ItemVariations.Select(x => x.CreateFromServerToClient()).ToList();
            inventoryItem.AveragePrice = 0;
            double? sum = source.ItemVariations.Where(x => x.PriceCalculation).Sum(y => y.UnitPrice / source.ItemVariations.Count(z => z.PriceCalculation));
            if (
                sum != null)
                inventoryItem.AveragePrice = Math.Round((double) sum, 2);
            inventoryItem.AverageCost = 0;
            inventoryItem.AverageCost = Math.Round((double) (source.ItemVariations.Where(x => x.CostCalculation).Sum(y => y.UnitCost/ source.ItemVariations.Count(z => z.CostCalculation))), 2);
            inventoryItem.AveragePackagePrice = source.ItemVariations.Sum(y => y.PackagePrice / source.ItemVariations.Count());
            //inventoryItem.QuantityInHand = source.QuantityInHand;
            inventoryItem.QuantityInHand = source.QuantityInHand;
            //inventoryItem.QuantityInHand = source.ItemVariations.Sum(x => Convert.ToInt64(x.QuantityInHand));
            inventoryItem.QuantitySold = source.ItemVariations.Sum(x => Convert.ToInt64(x.ItemReleaseQuantities.Sum(y=>y.Quantity)));
            inventoryItem.DepartmentPath = source.DepartmentPath;
            inventoryItem.QuantityInPackage = source.ItemVariations.Sum(x=>x.QuantityInPackage);
            return inventoryItem;
        }
        public static InventoryItemDDL CreateFromServerToClientDDL(this Models.DomainModels.InventoryItem source)
        {
            InventoryItemDDL inventoryItem = new InventoryItemDDL
            {
                ItemId = source.ItemId,
                ItemNameE = source.ItemNameEn,
                ItemNameA = source.ItemNameAr
            };

            return inventoryItem;
        }
        public static InventoryItemRequest CreateFromClientToServer(this WebsiteModels.InventoryItem source)
        {
            var item = new Models.DomainModels.InventoryItem
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
                //RecCreatedBy = source.RecCreatedBy,
                //RecCreatedDt = source.RecCreatedDt,
                //RecLastUpdatedBy = source.RecLastUpdatedBy,
                //RecLastUpdatedDt = source.RecLastUpdatedDt,
                DepartmentPath = source.DepartmentPath,
                QuantityInPackage = source.QuantityInPackage
            };
            var request = new InventoryItemRequest();
            request.InventoryItem = item;
            return request;
        }
    }
}