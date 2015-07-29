using System;
using System.Collections.Generic;

namespace EPMS.Models.DomainModels
{
    public class ProductSection
    {
        public long SectionId { get; set; }
        public string SectionNameEn { get; set; }
        public string SectionNameAr { get; set; }
        public string SectionContentEn { get; set; }
        public string SectionContentAr { get; set; }
        public Nullable<long> InventoyDepartmentId { get; set; }
        public bool ShowToPublic { get; set; }
        public Nullable<long> ParentSectionId { get; set; }
        public string RecCreatedBy { get; set; }
        public System.DateTime RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public System.DateTime RecLastUpdatedDt { get; set; }

        public virtual InventoryDepartment InventoryDepartment { get; set; }
        public virtual ICollection<ProductSection> ProductSections { get; set; }
        public virtual ProductSection ParentSection { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
