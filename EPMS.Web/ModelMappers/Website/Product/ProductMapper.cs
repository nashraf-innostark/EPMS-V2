using System.Collections.Generic;
using System.Linq;
using EPMS.Models.RequestModels;
using EPMS.Web.ModelMappers.Website.ProductImage;
using WebModels = EPMS.Web.Models;
using DomainModels = EPMS.Models.DomainModels;

namespace EPMS.Web.ModelMappers.Website.Product
{
    public static class ProductMapper
    {
        public static WebModels.Product CreateFromServerToClient(this DomainModels.Product source)
        {
            return new WebModels.Product
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
                RecLastUpdatedDt = source.RecLastUpdatedDt,
                ItemNameEn = source.ItemVariation != null ? source.ItemVariation.SKUDescriptionEn : string.Empty,
                ProductImages = source.ProductImages.Select(x=>x.CreateFromServerToClient()).ToList(),
                ItemNameAr = source.ItemVariation != null ? source.ItemVariation.SKUDescriptionAr : string.Empty
            };
        }

        public static ProductRequest CreateFromClientToServer(this WebModels.Product source)
        {
            var product = new DomainModels.Product
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
                    new List<DomainModels.ProductImage>(source.ProductImages.Select(x => x.CreateFromClientToServer()))
            };
            return request;
        }
    }
}