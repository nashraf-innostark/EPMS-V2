using WebModels = EPMS.Web.Models;
using DomainModels = EPMS.Models.DomainModels;

namespace EPMS.Web.ModelMappers.Website.ProductSection
{
    public static class ProductSectionMapper
    {
        public static WebModels.ProductSection CreateFromServerToClient(this DomainModels.ProductSection source)
        {
            return new WebModels.ProductSection
            {
                SectionId = source.SectionId,
                SectionNameEn = source.SectionNameEn,
                SectionNameAr = source.SectionNameAr,
                InventoyDepartmentId = source.InventoyDepartmentId,
                ParentSectionId = source.ParentSectionId,
                SectionContentEn = source.SectionContentEn,
                SectionContentAr = source.SectionContentAr,
                ShowToPublic = source.ShowToPublic,
                ShowToPublicForIndex = source.ShowToPublic ? "Yes" : "No",
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
                InventoryDepartmentNameEn = source.InventoryDepartment != null ? source.InventoryDepartment.DepartmentNameEn : string.Empty,
                InventoryDepartmentNameAr = source.InventoryDepartment != null ? source.InventoryDepartment.DepartmentNameAr : string.Empty,
                //if DepartmentId is null, it means the Section is Manually created.
                IsManuallyCreated = source.InventoyDepartmentId == null ? "Yes":"No"
            };
        }
        public static WebModels.ProductSection CreateFromServerToClientForTree(this DomainModels.ProductSection source)
        {
            return new WebModels.ProductSection
            {
                SectionId = source.SectionId,
                SectionNameEn = source.SectionNameEn,
                SectionNameAr = source.SectionNameAr,
                InventoyDepartmentId = source.InventoyDepartmentId,
                ParentSectionId = source.ParentSectionId,
                SectionContentEn = source.SectionContentEn,
                SectionContentAr = source.SectionContentAr,
                ShowToPublic = source.ShowToPublic,
                InventoryDepartmentNameEn = source.InventoryDepartment !=null ? source.InventoryDepartment.DepartmentNameEn : string.Empty,
                InventoryDepartmentNameAr = source.InventoryDepartment != null ? source.InventoryDepartment.DepartmentNameAr : string.Empty,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecCreatedDate = source.RecCreatedDt.ToShortDateString(),
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
                InventoryDepartment = source.InventoryDepartment != null ? source.InventoryDepartment.CreateForProductSectionFromServerToClient() : new WebModels.InventoryDepartment()
            };
        }
        public static DomainModels.ProductSection CreateFromClientToServer(this WebModels.ProductSection source)
        {
            return new DomainModels.ProductSection
            {
                SectionId = source.SectionId,
                SectionNameEn = source.SectionNameEn,
                SectionNameAr = source.SectionNameAr,
                InventoyDepartmentId = source.InventoyDepartmentId,
                ParentSectionId = source.ParentSectionId,
                SectionContentEn = source.SectionContentEn,
                SectionContentAr = source.SectionContentAr,
                ShowToPublic = source.ShowToPublic,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt
            };
        }
    }
}