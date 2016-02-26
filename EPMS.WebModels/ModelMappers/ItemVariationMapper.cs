using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;

namespace EPMS.WebModels.ModelMappers
{
    public static class ItemVariationMapper
    {
        public static WebsiteModels.ItemVariation CreateFromServerToClient(this Models.DomainModels.ItemVariation source)
        {
            WebsiteModels.ItemVariation model = new WebsiteModels.ItemVariation();
            model.ItemVariationId = source.ItemVariationId;
            model.InventoryItemId = source.InventoryItemId;
            model.ItemBarcode = source.ItemBarcode;
            model.SKUCode = source.SKUCode;
            model.CostCalculation = source.CostCalculation;
            //var avgPrice = ((double)source.UnitPrice + (double) source.PurchaseOrderItems.Sum(x => x.UnitPrice))/2;

            model.UnitPrice = source.UnitPrice;
            model.QuantityInPackage = source.QuantityInPackage;
            model.PackagePrice = source.PackagePrice;
            model.PriceCalculation = source.PriceCalculation;
            var descE = source.InventoryItem.ItemDescriptionEn;
            if (!string.IsNullOrEmpty(descE))
            {
                descE = descE.Replace("\r", "");
                descE = descE.Replace("\t", "");
                descE = descE.Replace("\n", "");
            }
            model.DescriptionEn = descE;
            var descA = source.InventoryItem.ItemDescriptionAr;
            if (!string.IsNullOrEmpty(descE))
            {
                descA = descA.Replace("\r", "");
                descA = descA.Replace("\t", "");
                descA = descA.Replace("\n", "");
            }
            model.DescriptionAr = descA;
            model.DescriptionForQuotationEn = source.DescriptionForQuotationEn;
            model.DescriptionForQuotationAr = source.DescriptionForQuotationAr;
            model.SKUDescriptionEn = source.SKUDescriptionEn;
            model.SKUDescriptionAr = source.SKUDescriptionAr;
            //model.QuantityInHand = (source.ItemWarehouses.Sum(x=>x.Quantity) - source.ItemReleaseDetails.Sum(x=>x.ItemQty) + source.DIFItems.Sum(x=>x.ItemQty)).ToString();
            if (Convert.ToInt64(source.QuantityInHand) != 0)
            {
                model.QtyInHand = true;
            }
            
           
            model.ReorderPoint = source.ReorderPoint;
            model.QuantityInManufacturing = source.QuantityInManufacturing;
            model.Weight = source.Weight;
            model.Height = source.Height;
            model.Width = source.Width;
            model.Depth = source.Depth;
            var notesEn = source.NotesEn;
            if (!string.IsNullOrEmpty(notesEn))
            {
                notesEn = notesEn.Replace("\r", "");
                notesEn = notesEn.Replace("\t", "");
                notesEn = notesEn.Replace("\n", "");
            }
            model.NotesEn = notesEn;
            var notesAr = source.NotesAr;
            if (!string.IsNullOrEmpty(notesAr))
            {
                notesAr = notesAr.Replace("\r", "");
                notesAr = notesAr.Replace("\t", "");
                notesAr = notesAr.Replace("\n", "");
            }
            model.NotesAr = notesAr;
            var infoEn = source.AdditionalInfoEn;
            if (!string.IsNullOrEmpty(infoEn))
            {
                infoEn = infoEn.Replace("\r", "");
                infoEn = infoEn.Replace("\t", "");
                infoEn = infoEn.Replace("\n", "");
            }
            model.AdditionalInfoEn = infoEn;
            var infoAr = source.AdditionalInfoAr;
            if (!string.IsNullOrEmpty(infoAr))
            {
                infoAr = infoAr.Replace("\r", "");
                infoAr = infoAr.Replace("\t", "");
                infoAr = infoAr.Replace("\n", "");
            }
            model.AdditionalInfoAr = infoAr;
            model.RecCreatedBy = source.RecCreatedBy;
            model.RecCreatedDt = source.RecCreatedDt.ToString("dd/MM/yyyy", new CultureInfo("en"));
            model.RecLastUpdatedBy = source.RecLastUpdatedBy;
            model.RecLastUpdatedDt = source.RecLastUpdatedDt;
            model.Colors = source.Colors.Select(x => x.CreateFromServerToClient()).ToList();
            model.Sizes = source.Sizes.Select(x => x.CreateFromServerToClient()).ToList();
            model.Statuses = source.Status.Select(x => x.CreateFromServerToClient()).ToList();
            model.ItemManufacturers = source.ItemManufacturers.Select(x => x.CreateFromServerToClient()).ToList();
            model.ItemImages = source.ItemImages.Select(x => x.CreateFromServerToClient()).ToList();

            model.ItemWarehouses = source.ItemWarehouses.Select(x => x.CreateForItemWarehouse()).ToList();
            var descEn = source.InventoryItem.ItemDescriptionEn;
            if (!string.IsNullOrEmpty(descEn))
            {
                descEn = descEn.Replace("\r", "");
                descEn = descEn.Replace("\t", "");
                descEn = descEn.Replace("\n", "");
            }
            var descAr = source.InventoryItem.ItemDescriptionAr;
            if (!string.IsNullOrEmpty(descAr))
            {
                descAr = descAr.Replace("\r", "");
                descAr = descAr.Replace("\t", "");
                descAr = descAr.Replace("\n", "");
            }

            model.ItemDescForIndexEn = descEn;
            model.ItemDescForIndexAr = descAr;
            model.SizeNameEn = source.Sizes != null && source.Sizes.Count > 0
                ? source.Sizes.FirstOrDefault().SizeNameEn
                : "";
            model.SizeNameAr = source.Sizes != null && source.Sizes.Count > 0
                ? source.Sizes.FirstOrDefault().SizeNameAr
                : "";
            model.ColorNameEn = source.Colors != null && source.Colors.Count > 0
                ? source.Colors.FirstOrDefault().ColorNameEn
                : "";
            model.ColorNameAr = source.Colors != null && source.Colors.Count > 0
                ? source.Colors.FirstOrDefault().ColorNameAr
                : "";
            model.StatusNameEn = source.Status != null && source.Status.Count > 0
                ? source.Status.FirstOrDefault().StatusNameEn
                : "";
            model.StatusNameAr = source.Status != null && source.Status.Count > 0
                ? source.Status.FirstOrDefault().StatusNameAr
                : "";
            var totalCost = source.ItemManufacturers.Sum(y => y.Quantity*Convert.ToDouble(y.Price));
            var totalQuantity = source.ItemManufacturers.Sum(y => y.Quantity);
            if (totalCost > 0 && totalQuantity > 0)
            {
                model.UnitCost = Math.Round((double) (totalCost/totalQuantity), 2);
            }
            else
            {
                model.UnitCost = 0;
            }
            var manufacturerCount = model.ItemManufacturers.Count;
            model.AverageCost = model.UnitCost/manufacturerCount;

            var itemReleaseQty =
                source.ItemReleaseQuantities.Where(x => x.ItemReleaseDetail.ItemRelease.Status == 1)
                    .Sum(x => x.Quantity);
            var poItems = source.PurchaseOrderItems.Where(y => y.PurchaseOrder.Status == 1)
                                .Sum(y => Convert.ToDouble(y.ItemQty));
            var defectedItemQuantity = source.DIFItems.Where(x=>x.DIF.Status == 2).Sum(x => x.ItemQty);
            var returnItemQuantity = source.RIFItems.Where(x=>x.RIF.Status == 2).Sum(x => x.ItemQty);

            var qty = (Convert.ToDouble(source.QuantityInHand) +
                                         source.ItemManufacturers.Sum(x => x.Quantity) + returnItemQuantity + poItems) -
                                        (itemReleaseQty + defectedItemQuantity);
            model.TotalQuantityInHand = qty;
            model.QuantityInHand = qty.ToString();

            var qtySold =
                source.ItemReleaseQuantities.Where(y => y.ItemVariationId == source.ItemVariationId && y.ItemReleaseDetail.ItemRelease.Status == 1)
                    .Sum(x => x.Quantity) - 
                    source.RIFItems.Where(x=>x.RIF.Status == 2 && x.ItemVariationId == source.ItemVariationId).Sum(x=>x.ItemQty);
            model.QuantitySold = qtySold;
            return model;
        }

        public static WebsiteModels.ItemVariation CreateFromServerToClient(
            this Models.DomainModels.ItemVariation source, IEnumerable<ItemWarehouse> itemWarehouses)
        {
            WebsiteModels.ItemVariation model = new WebsiteModels.ItemVariation();
            model.ItemVariationId = source.ItemVariationId;
            model.InventoryItemId = source.InventoryItemId;
            model.ItemBarcode = source.ItemBarcode;
            model.SKUCode = source.SKUCode;
            model.UnitCost = source.UnitCost;
            model.CostCalculation = source.CostCalculation;
            model.UnitPrice = source.UnitPrice;
            model.QuantityInPackage = source.QuantityInPackage;
            model.PackagePrice = source.PackagePrice;
            model.PriceCalculation = source.PriceCalculation;
            model.DescriptionEn = source.InventoryItem.ItemDescriptionEn;
            model.DescriptionAr = source.InventoryItem.ItemDescriptionAr;
            model.DescriptionForQuotationEn = source.DescriptionForQuotationEn;
            model.DescriptionForQuotationAr = source.DescriptionForQuotationAr;
            model.SKUDescriptionEn = source.SKUDescriptionEn;
            model.SKUDescriptionAr = source.SKUDescriptionAr;
            model.QuantityInHand = source.QuantityInHand;
            //model.QuantitySold = source.QuantitySold;
            model.ReorderPoint = source.ReorderPoint;
            model.QuantityInManufacturing = source.QuantityInManufacturing;
            model.Weight = source.Weight;
            model.Height = source.Height;
            model.Width = source.Width;
            model.Depth = source.Depth;
            model.NotesEn = source.NotesEn;
            model.NotesAr = source.NotesAr;
            model.AdditionalInfoEn = source.AdditionalInfoEn;
            model.AdditionalInfoAr = source.AdditionalInfoAr;
            model.RecCreatedBy = source.RecCreatedBy;
            model.RecCreatedDt = source.RecCreatedDt.ToString("dd/MM/yyyy", new CultureInfo("en"));
            model.RecLastUpdatedBy = source.RecLastUpdatedBy;
            model.RecLastUpdatedDt = source.RecLastUpdatedDt;
            model.Colors = source.Colors.Select(x => x.CreateFromServerToClient()).ToList();
            model.Sizes = source.Sizes.Select(x => x.CreateFromServerToClient()).ToList();
            model.Statuses = source.Status.Select(x => x.CreateFromServerToClient()).ToList();
            model.ItemManufacturers = source.ItemManufacturers.Select(x => x.CreateFromServerToClient()).ToList();
            model.ItemImages = source.ItemImages.Select(x => x.CreateFromServerToClient()).ToList();
            model.ItemWarehouses = itemWarehouses.Select(x => x.CreateForItemWarehouse()).ToList();
            var descEn = source.InventoryItem.ItemDescriptionEn;
            if (!string.IsNullOrEmpty(descEn))
            {
                descEn = descEn.Replace("\r", "");
                descEn = descEn.Replace("\t", "");
                descEn = descEn.Replace("\n", "");
            }
            var descAr = source.InventoryItem.ItemDescriptionAr;
            if (!string.IsNullOrEmpty(descAr))
            {
                descAr = descAr.Replace("\r", "");
                descAr = descAr.Replace("\t", "");
                descAr = descAr.Replace("\n", "");
            }

            model.ItemDescForIndexEn = descEn;
            model.ItemDescForIndexAr = descAr;
            return model;
        }

        public static ItemVariationRequest CreateFromClientToServer(this WebsiteModels.ItemVariation source)
        {
            var item = new ItemVariation();
            item.ItemVariationId = source.ItemVariationId;
            item.InventoryItemId = source.InventoryItemId;
            item.ItemBarcode = source.ItemBarcode;
            item.SKUCode = source.SKUCode;
            item.UnitCost = source.UnitCost;
            item.CostCalculation = source.CostCalculation;
            item.UnitPrice = source.UnitPrice;
            item.QuantityInPackage = source.QuantityInPackage;
            item.PackagePrice = source.PackagePrice;
            item.PriceCalculation = source.PriceCalculation;
            item.DescriptionForQuotationEn = source.DescriptionForQuotationEn;
            item.DescriptionForQuotationAr = source.DescriptionForQuotationAr;
            item.SKUDescriptionEn = source.SKUDescriptionEn;
            item.SKUDescriptionAr = source.SKUDescriptionAr;
            item.QuantityInHand = source.QuantityInHand;
            item.QuantitySold = Convert.ToString(source.QuantitySold);
            item.ReorderPoint = source.ReorderPoint;
            item.QuantityInManufacturing = source.QuantityInManufacturing;
            item.Weight = source.Weight;
            item.Height = source.Height;
            item.Width = source.Width;
            item.Depth = source.Depth;
            item.NotesEn = source.NotesEn;
            item.NotesAr = source.NotesAr;
            item.AdditionalInfoEn = source.AdditionalInfoEn;
            item.AdditionalInfoAr = source.AdditionalInfoAr;
            item.RecCreatedBy = source.RecCreatedBy;
            item.RecCreatedDt = DateTime.ParseExact(source.RecCreatedDt, "dd/MM/yyyy", new CultureInfo("en"));
            item.RecLastUpdatedBy = source.RecLastUpdatedBy;
            item.RecLastUpdatedDt = source.RecLastUpdatedDt;
            var request = new ItemVariationRequest();
            request.ItemVariation = item;
            request.ItemImages = new List<ItemImage>(source.ItemImages.Select(x => x.CreateFrom()));
            request.ItemManufacturers = new List<ItemManufacturer>(source.ItemManufacturers.Select(x => x.CreateFrom()));
            request.ItemWarehouses = new List<ItemWarehouse>(source.ItemWarehouses.Select(x => x.CreateFrom()));
            return request;
        }

        //For manipulating Item Images
        public static ItemImage CreateFrom(this WebsiteModels.ItemImage source)
        {
            return new ItemImage
            {
                ImageId = source.ImageId,
                ItemImagePath = source.ItemImagePath,
                ImageOrder = source.ImageOrder,
                ShowImage = source.ShowImage,
                ItemVariationId = source.ItemVariationId
            };
        }

        //For manipulating Item Manufacturer
        public static ItemManufacturer CreateFrom(this WebsiteModels.ItemManufacturer source)
        {
            return new ItemManufacturer
            {
                ItemVariationId = source.ItemVariationId,
                ManufacturerId = source.ManufacturerId,
                Price = source.Price,
                Quantity = source.Quantity
            };
        }

        //For manipulating Item Warehouse
        public static ItemWarehouse CreateFrom(this WebsiteModels.ItemWarehouse source)
        {
            return new ItemWarehouse
            {
                ItemVariationId = source.ItemVariationId,
                PlaceInWarehouse = source.PlaceInWarehouse,
                Quantity = source.Quantity,
                WarehouseId = source.WarehouseId,
                WarehouseDetailId = source.WarehouseDetailId
            };
        }
    }
}