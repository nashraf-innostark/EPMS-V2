using System;
using System.Globalization;

namespace EPMS.WebModels.ModelMappers.Website.ProductSection
{
    public static class ProductSectionMapper
    {
        public static WebsiteModels.ProductSection CreateFromServerToClient(this Models.DomainModels.ProductSection source)
        {
            return new WebsiteModels.ProductSection
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
        public static WebsiteModels.ProductSection CreateFromServerToClientForTree(this Models.DomainModels.ProductSection source)
        {
            return new WebsiteModels.ProductSection
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
                RecCreatedDate = source.RecCreatedDt.ToString("dd/MM/yyyy", new CultureInfo("en")),
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
                InventoryDepartment = source.InventoryDepartment != null ? source.InventoryDepartment.CreateForProductSectionFromServerToClient() : new WebsiteModels.InventoryDepartment()
            };
        }
        public static Models.DomainModels.ProductSection CreateFromClientToServer(this WebsiteModels.ProductSection source)
        {
            return new Models.DomainModels.ProductSection
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
                RecCreatedDt = !string.IsNullOrEmpty(source.RecCreatedDate) ? DateTime.ParseExact(source.RecCreatedDate, "dd/MM/yyyy", new CultureInfo("en")) : source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt
            };
        }
    }
}