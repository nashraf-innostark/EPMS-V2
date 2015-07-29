using System;

namespace EPMS.Web.Models
{
    public class ProductSection
    {
        public long SectionId { get; set; }
        public string SectionNameEn { get; set; }
        public string SectionNameAr { get; set; }
        public string SectionContentEn { get; set; }
        public string SectionContentAr { get; set; }
        public long? InventoyDepartmentId { get; set; }
        public bool ShowToPublic { get; set; }
        public long? ParentSectionId { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime RecLastUpdatedDt { get; set; }
        public string InventoryDepartmentNameEn { get; set; }
        public string InventoryDepartmentNameAr { get; set; }
        public string IsManuallyCreated { get; set; }
        public string ShowToPublicForIndex { get; set; }
    }
}