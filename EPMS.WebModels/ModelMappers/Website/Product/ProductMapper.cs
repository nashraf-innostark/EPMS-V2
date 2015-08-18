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
                ProductDescEn = source.ItemVariationId == null ? source.ProductDescEn : source.SKUDescriptionEn,
                ProductDescAr = source.ItemVariationId == null ? source.ProductDescAr : source.SKUDescriptionAr,
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
                ItemNameEn = source.ItemVariation !=null ? source.ItemVariation.SKUDescriptionEn : "",
                ItemNameAr = source.ItemVariation != null ? source.ItemVariation.SKUDescriptionAr : "",
                ProductImages = source.ProductImages.Select(x=>x.CreateFromServerToClient()).ToList(),
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
                ProductNameEn = source.InventoryItem.ItemNameEn,
                ProductNameAr = source.InventoryItem.ItemNameAr,
                ProductDescEn = source.SKUDescriptionEn,
                ProductDescAr = source.SKUDescriptionAr,
                ProductPrice = source.UnitPrice.ToString(),
                SKUCode = source.SKUCode,
                ItemImages = source.ItemImages.Select(x=>x.CreateFromServerToClient()),
                Sizes = source.Sizes.Select(x=>x.CreateFromServerToClient())
            };
        }
    }
}