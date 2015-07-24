namespace EPMS.Web.ModelMappers.Website.Department
{
    public static class WebsiteDepartmentMapper
    {
        public static EPMS.Models.DomainModels.WebsiteDepartment CreateFromClientToServer(this Models.WebsiteDepartment source)
        {
            string descp = source.Description;
            if (!string.IsNullOrEmpty(descp))
            {
                descp = descp.Replace("\n", "");
                descp = descp.Replace("\r", "");
            }
            return new EPMS.Models.DomainModels.WebsiteDepartment
            {
                DepartmentId = source.DepartmentId,
                NameEn = source.NameEn,
                NameAr = source.NameAr,
                DepartmentOrder = source.DepartmentOrder,
                ImageName = source.ImageName,
                Link = source.Link,
                Description = descp,
                ShowToPublic = source.ShowToPublic,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate
            };
        }

        public static Models.WebsiteDepartment CreateFromServerToClient(this EPMS.Models.DomainModels.WebsiteDepartment source)
        {
            return new Models.WebsiteDepartment
            {
                DepartmentId = source.DepartmentId,
                NameEn = source.NameEn,
                NameAr = source.NameAr,
                DepartmentOrder = source.DepartmentOrder,
                ImageName = source.ImageName,
                Link = source.Link,
                Description = source.Description,
                ShowToPublic = source.ShowToPublic,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate
            };
        }
    }
}