using System;
using System.Collections.Generic;

namespace EPMS.WebModels.WebsiteModels
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
        public bool NewArrival { get; set; }
        public bool BestSeller { get; set; }
        public bool RandomProduct { get; set; }
        public bool Featured { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime RecLastUpdatedDt { get; set; }
        public string ItemNameEn { get; set; }
        public string ItemNameAr { get; set; }
        public string SKUCode { get; set; }
        public string ProductImage { get; set; }
        public string ItemImage { get; set; }

        public List<ProductImage> ProductImages { get; set; }
        public IEnumerable<ItemImage> ItemImages { get; set; }
        public IEnumerable<Size> Sizes { get; set; }
    }
}