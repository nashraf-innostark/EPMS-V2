using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using EPMS.Models.RequestModels;
using WebModels = EPMS.Web.Models;
using DomainModels = EPMS.Models.DomainModels;

namespace EPMS.Web.ModelMappers
{
    public static class ItemVariationMapper
    {
        public static WebModels.ItemVariation CreateFromServerToClient(this DomainModels.ItemVariation source)
        {
            return new WebModels.ItemVariation
            {
                ItemVariationId = source.ItemVariationId,
                InventoryItemId = source.InventoryItemId,
                ItemBarcode = source.ItemBarcode,
                SKUCode = source.SKUCode,
                UnitPrice = source.UnitPrice,
                PackagePrice = source.PackagePrice,
                PriceCalculation = source.PriceCalculation,
                DescriptionEn = source.DescriptionEn,
                DescriptionAr = source.DescriptionAr,
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
                Colors = source.Colors.Select(x => x.CreateFromServerToClient()).ToList(),
                Sizes = source.Sizes.Select(x => x.CreateFromServerToClient()).ToList(),
                Statuses = source.Status.Select(x => x.CreateFromServerToClient()).ToList(),
                Manufacturers = source.Manufacturers.Select(x=>x.CreateFromServerToClient()).ToList(),
                ItemImages = source.ItemImages.Select(x=>x.CreateFromServerToClient()).ToList()
            };
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
                DescriptionEn = source.DescriptionEn,
                DescriptionAr = source.DescriptionAr,
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
            request.ItemImages = new List<DomainModels.ItemImage>(source.ItemImages.Select(x=>x.CreateFrom()));
            return request;
        }

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
    }
}