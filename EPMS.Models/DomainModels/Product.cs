using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPMS.Models.DomainModels
{
    public class Product
    {
        public long ProductId { get; set; }
        public Nullable<long> ItemVariationId { get; set; }
        public string ProductNameEn { get; set; }
        public string ProductNameAr { get; set; }
        public string ProductDescEn { get; set; }
        public string ProductDescAr { get; set; }
        public string ProductPrice { get; set; }
        public string DiscountedPrice { get; set; }
        public string ProductSize { get; set; }
        public string ProductSpecificationEn { get; set; }
        public string ProductSpecificationAr { get; set; }
        public Nullable<long> ProductSectionId { get; set; }
        public string RecCreatedBy { get; set; }
        public System.DateTime RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public System.DateTime RecLastUpdatedDt { get; set; }

        public virtual ItemVariation ItemVariation { get; set; }
        public virtual ProductSection ProductSection { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
    }
}
