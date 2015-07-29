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
                RecLastUpdatedDt = source.RecLastUpdatedDt
            };
        }

        public static DomainModels.Product CreateFromClientToServer(this WebModels.Product source)
        {
            return new DomainModels.Product
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
        }
    }
}