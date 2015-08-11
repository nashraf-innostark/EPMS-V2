using EPMS.Models.DomainModels;

namespace EPMS.WebModels.ModelMappers
{
    public static class DepartmentMapper
    {
        public static Department CreateFrom(this WebsiteModels.Department source)
        {
            var department = new Department
            {
                DepartmentId = source.DepartmentId,
                DepartmentNameE = source.DepartmentNameE,
                DepartmentNameA = source.DepartmentNameA,
                DepartmentDesc = source.DepartmentDesc,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt
            };
            return department;
        }
        public static WebsiteModels.Department CreateFrom(this Department source)
        {
            return new WebsiteModels.Department
            {
                DepartmentId = source.DepartmentId,
                DepartmentNameE = source.DepartmentNameE,
                DepartmentNameA = source.DepartmentNameA,
                DepartmentDesc = source.DepartmentDesc,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt
            };

        }
        public static Models.DashboardModels.Department CreateForDashboard(this Department source)
        {
            return new Models.DashboardModels.Department
            {
                DepartmentId = source.DepartmentId,
                DepartmentNameE = source.DepartmentNameE,
                DepartmentNameA = source.DepartmentNameA
            };

        }
    }
}