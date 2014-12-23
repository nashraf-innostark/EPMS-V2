using EPMS.Models.DomainModels;

namespace EPMS.Web.ModelMappers
{
    public static class DepartmentMapper
    {
        public static Department CreateFrom(this Models.Department source)
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
        public static Models.Department CreateFrom(this Department source)
        {
            return new Models.Department
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
    }
}