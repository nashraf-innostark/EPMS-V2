using System.Collections.Generic;
using System.Linq;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.WebModels.ModelMappers.Website.ProductImage;

namespace EPMS.WebModels.ModelMappers.Website.Product
{
    public static class ProductMapper
    {
        public static WebsiteModels.Product CreateFromServerToClient(this Models.DomainModels.Product source)
        {
            return new WebsiteModels.Product
            {
                ProductId = source.ProductId,
                ProductNameEn = source.ProductNameEn,
                ProductNameAr = source.ProductNameAr,
                ItemVariationId = source.ItemVariationId,
                ProductDescEn = source.ItemVariationId == null ? source.ProductDescEn : source.ItemDescriptionEn,
                ProductDescAr = source.ItemVariationId == null ? source.ProductDescAr : source.ItemDescriptionAr,
                ProductPrice = source.ItemVariationId != null ? source.ItemVariation.UnitPrice.ToString() : source.ProductPrice,
                DiscountedPrice = source.DiscountedPrice,
                ProductSpecificationEn = source.ProductSpecificationEn,
                ProductSpecificationAr = source.ProductSpecificationAr,
                ProductSize = source.ProductSize,
                ProductSectionId = source.ProductSectionId,
                NewArrival = source.NewArrival,
                Featured = source.Featured,
                RandomProduct = source.RandomProduct,
                BestSeller = source.BestSeller,
                SKUCode = source.SKUCode,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
                ItemNameEn = source.ItemVariation != null ? source.ItemVariation.SKUDescriptionEn : "",
                ItemNameAr = source.ItemVariation != null ? source.ItemVariation.SKUDescriptionAr : "",
                ProductImages = source.ProductImages.Select(x => x.CreateFromServerToClient()).ToList(),
                ItemImage = source.ItemVariation != null && source.ItemVariation.ItemImages != null && source.ItemVariation.ItemImages.FirstOrDefault() != null ?
                    source.ItemVariation.ItemImages.FirstOrDefault().ItemImagePath : "",
                ProductImage = source.ProductImages != null && source.ProductImages.Any() && source.ProductImages.FirstOrDefault() != null ?
                    source.ProductImages.FirstOrDefault().ProductImagePath : "",
            };
        }
        public static WebsiteModels.Product CreateFromServerToClientFromInventory(this Models.DomainModels.Product source)
        {
            return new WebsiteModels.Product
            {
                ProductId = source.ProductId,
                ProductNameEn = source.ProductNameEn,
                ProductNameAr = source.ProductNameAr,
                ItemVariationId = source.ItemVariationId,
                ProductDescEn = source.ItemVariationId == null ? source.ProductDescEn : source.ItemDescriptionEn,
                ProductDescAr = source.ItemVariationId == null ? source.ProductDescAr : source.ItemDescriptionAr,
                ProductPrice = source.ItemVariationId != null ? source.ItemVariation.UnitPrice.ToString() : source.ProductPrice,
                DiscountedPrice = source.DiscountedPrice,
                ProductSpecificationEn = source.ProductSpecificationEn,
                ProductSpecificationAr = source.ProductSpecificationAr,
                ProductSize = source.ProductSize,
                ProductSectionId = source.ProductSectionId,
                SKUCode = source.SKUCode,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
                ItemNameEn = source.ItemVariation != null && source.ItemVariation.InventoryItem != null ? source.ItemVariation.InventoryItem.ItemNameEn : "",
                ItemNameAr = source.ItemVariation != null && source.ItemVariation.InventoryItem != null ? source.ItemVariation.InventoryItem.ItemNameAr : "",
                ProductImages = source.ProductImages != null ? source.ProductImages.Select(x => x.CreateFromServerToClient()).ToList() :
                                    new List<WebsiteModels.ProductImage>(),
                ItemImage = source.ItemVariation != null && source.ItemVariation.ItemImages != null && source.ItemVariation.ItemImages.FirstOrDefault() != null ?
                    source.ItemVariation.ItemImages.FirstOrDefault().ItemImagePath : "",
                ProductImage = source.ProductImages != null && source.ProductImages.Any() && source.ProductImages.FirstOrDefault() != null ?
                    source.ProductImages.FirstOrDefault().ProductImagePath : "",
                SizeId = source.ItemVariation != null && source.ItemVariation.Sizes.FirstOrDefault() != null ? source.ItemVariation.Sizes.FirstOrDefault().SizeId : 0,
            };
        }

        public static ProductRequest CreateFromClientToServer(this WebsiteModels.Product source)
        {
            var product = new Models.DomainModels.Product
            {
                ProductId = source.ProductId,
                ProductNameEn = source.ProductNameEn,
                ProductNameAr = source.ProductNameAr,
                ItemVariationId = source.ItemVariationId,
                ProductDescEn = source.ProductDescEn,
                ProductDescAr = source.ProductDescAr,
                ProductPrice = source.ProductPrice,
                DiscountedPrice = source.DiscountedPrice,
                ProductSpecificationEn = source.ProductSpecificationEn,
                ProductSpecificationAr = source.ProductSpecificationAr,
                ProductSize = source.ProductSize,
                ProductSectionId = source.ProductSectionId,
                NewArrival = source.NewArrival,
                Featured = source.Featured,
                RandomProduct = source.RandomProduct,
                BestSeller = source.BestSeller,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt
            };
            var request = new ProductRequest
            {
                Product = product,
                ProductImages =
                    new List<Models.DomainModels.ProductImage>(source.ProductImages.Select(x => x.CreateFromClientToServer()))
            };
            return request;
        }

        public static WebsiteModels.Product CreateFromItemVariation(this ItemVariation source)
        {
            return new WebsiteModels.Product
            {
                ProductId = source.Products.FirstOrDefault(x => x.ItemVariationId == source.ItemVariationId).ProductId,
                ItemVariationId = source.ItemVariationId,
                ProductNameEn = source.InventoryItem.ItemNameEn,
                ProductNameAr = source.InventoryItem.ItemNameAr,
                ProductDescEn = source.InventoryItem.ItemDescriptionEn,
                ProductDescAr = source.InventoryItem.ItemDescriptionAr,
                ProductPrice = source.UnitPrice.ToString(),
                SKUCode = source.SKUCode,
                ItemImages = source.ItemImages.Select(x => x.CreateFromServerToClient()),
                Sizes = source.Sizes.Select(x => x.CreateFromServerToClient()),
                ItemImage = source.ItemImages != null && source.ItemImages.FirstOrDefault() != null ?
                    source.ItemImages.FirstOrDefault().ItemImagePath : "",
            };
        }
    }
}