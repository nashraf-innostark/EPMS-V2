using System;
using System.Collections.Generic;

namespace EPMS.Web.Models
{
    public class Product
    {
        public long ProductId { get; set; }
        public long? ItemVariationId { get; set; }
        public string ProductNameEn { get; set; }
        public string ProductNameAr { get; set; }
        public string ProductDescEn { get; set; }
        public string ProductDescAr { get; set; }
        public string ProductPrice { get; set; }
        public string DiscountedPrice { get; set; }
        public string ProductSize { get; set; }
        public string ProductSpecificationEn { get; set; }
        public string ProductSpecificationAr { get; set; }
        public long? ProductSectionId { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime RecLastUpdatedDt { get; set; }
        public List<ProductImage> ProductImages { get; set; }
    }
}