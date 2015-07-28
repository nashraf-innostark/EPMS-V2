using System;
using System.Collections.Generic;
using System.Linq;
using EPMS.Models.RequestModels;
using iTextSharp.text.pdf.qrcode;
using WebModels = EPMS.Web.Models;
using DomainModels = EPMS.Models.DomainModels;

namespace EPMS.Web.ModelMappers
{
    public static class ItemVariationMapper
    {
        public static WebModels.ItemVariation CreateFromServerToClient(this DomainModels.ItemVariation source)
        {
            Models.ItemVariation model = new Models.ItemVariation();
            model.ItemVariationId = source.ItemVariationId;
            model.InventoryItemId = source.InventoryItemId;
            model.ItemBarcode = source.ItemBarcode;
            model.SKUCode = source.SKUCode;
            model.UnitPrice = source.UnitPrice;
            model.PackagePrice = source.PackagePrice;
            model.PriceCalculation = source.PriceCalculation;
            model.DescriptionEn = source.InventoryItem.ItemDescriptionEn;
            model.DescriptionAr = source.InventoryItem.ItemDescriptionAr;
            model.SKUDescriptionEn = source.SKUDescriptionEn;
            model.SKUDescriptionAr = source.SKUDescriptionAr;
            model.QuantityInHand = source.QuantityInHand;
            model.QuantitySold = source.QuantitySold;
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
            model.RecCreatedDt = source.RecCreatedDt;
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
            model.SizeNameEn = source.Sizes != null && source.Sizes.Count > 0 ? source.Sizes.FirstOrDefault().SizeNameEn : "";
            model.SizeNameAr = source.Sizes != null && source.Sizes.Count > 0 ? source.Sizes.FirstOrDefault().SizeNameAr : "";
            model.ColorNameEn = source.Colors != null && source.Colors.Count > 0 ? source.Colors.FirstOrDefault().ColorNameEn: "";
            model.ColorNameAr = source.Colors != null && source.Colors.Count > 0 ? source.Colors.FirstOrDefault().ColorNameAr: "";
            model.StatusNameEn = source.Status != null && source.Status.Count > 0 ? source.Status.FirstOrDefault().StatusNameEn : "";
            model.StatusNameAr = source.Status != null && source.Status.Count > 0 ? source.Status.FirstOrDefault().StatusNameAr : "";
            var totalCost = source.ItemManufacturers.Sum(y => y.Quantity * Convert.ToInt64(y.Price));
            var totalQuantity = source.ItemManufacturers.Sum(y => y.Quantity);
            if (totalCost > 0 && totalQuantity > 0)
            {
                model.AverageCost = totalCost/totalQuantity;
            }
            else
            {
                model.AverageCost = 0;
            }
            return model;
        }
        public static WebModels.ItemVariation CreateFromServerToClient(this DomainModels.ItemVariation source, IEnumerable<DomainModels.ItemWarehouse> itemWarehouses)
        {
            Models.ItemVariation model = new Models.ItemVariation();
            model.ItemVariationId = source.ItemVariationId;
            model.InventoryItemId = source.InventoryItemId;
            model.ItemBarcode = source.ItemBarcode;
            model.SKUCode = source.SKUCode;
            model.UnitPrice = source.UnitPrice;
            model.PackagePrice = source.PackagePrice;
            model.PriceCalculation = source.PriceCalculation;
            model.DescriptionEn = source.InventoryItem.ItemDescriptionEn;
            model.DescriptionAr = source.InventoryItem.ItemDescriptionAr;
            model.SKUDescriptionEn = source.SKUDescriptionEn;
            model.SKUDescriptionAr = source.SKUDescriptionAr;
            model.QuantityInHand = source.QuantityInHand;
            model.QuantitySold = source.QuantitySold;
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
            model.RecCreatedDt = source.RecCreatedDt;
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

        public static ItemVariationRequest CreateFromClientToServer(this WebModels.ItemVariation source)
        {
            var item = new DomainModels.ItemVariation
            {
                ItemVariationId = source.ItemVariationId,
                InventoryItemId = source.InventoryItemId,
                ItemBarcode = source.ItemBarcode,
                SKUCode = source.SKUCode,
                UnitPrice = source.UnitPrice,
                PackagePrice = source.PackagePrice,
                PriceCalculation = source.PriceCalculation,
                SKUDescriptionEn = source.SKUDescriptionEn,
                SKUDescriptionAr = source.SKUDescriptionAr,
                QuantityInHand = source.QuantityInHand,
                QuantitySold = source.QuantitySold,
                ReorderPoint = source.ReorderPoint,
                QuantityInManufacturing = source.QuantityInManufacturing,
                Weight = source.Weight,
                Height = source.Height,
                Width = source.Width,
                Depth = source.Depth,
                NotesEn = source.NotesEn,
                NotesAr = source.NotesAr,
                AdditionalInfoEn = source.AdditionalInfoEn,
                AdditionalInfoAr = source.AdditionalInfoAr,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
            };
            var request = new ItemVariationRequest();
            request.ItemVariation = item;
            request.ItemImages = new List<DomainModels.ItemImage>(source.ItemImages.Select(x => x.CreateFrom()));
            request.ItemManufacturers = new List<DomainModels.ItemManufacturer>(source.ItemManufacturers.Select(x => x.CreateFrom()));
            request.ItemWarehouses = new List<DomainModels.ItemWarehouse>(source.ItemWarehouses.Select(x => x.CreateFrom()));
            return request;
        }

        //For manipulating Item Images
        public static DomainModels.ItemImage CreateFrom(this WebModels.ItemImage source)
        {
            return new DomainModels.ItemImage
            {
                ImageId = source.ImageId,
                ItemImagePath = source.ItemImagePath,
                ImageOrder = source.ImageOrder,
                ShowImage = source.ShowImage,
                ItemVariationId = source.ItemVariationId
            };
        }

        //For manipulating Item Manufacturer
        public static DomainModels.ItemManufacturer CreateFrom(this WebModels.ItemManufacturer source)
        {
            return new DomainModels.ItemManufacturer
            {
                ItemVariationId = source.ItemVariationId,
                ManufacturerId = source.ManufacturerId,
                Price = source.Price,
                Quantity = source.Quantity
            };
        }

        //For manipulating Item Warehouse
        public static DomainModels.ItemWarehouse CreateFrom(this WebModels.ItemWarehouse source)
        {
            return new DomainModels.ItemWarehouse
            {
                ItemVariationId = source.ItemVariationId,
                PlaceInWarehouse = source.PlaceInWarehouse,
                Quantity = source.Quantity,
                WarehouseId = source.WarehouseId
            };
        }
    }
}