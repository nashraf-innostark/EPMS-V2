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
                DepartmentName = source.DepartmentName,
                DepartmentDesc = source.DepartmentDesc,
                //DepartmentNameA = source.DepartmentNameA,
                //CreatedBy = source.CreatedBy,
                //CreatedDate = source.CreatedDate,
                //UpdatedBy = source.UpdatedBy,
                //UpdatedDate = source.UpdatedDate
            };
            return department;
        }
        public static Models.Department CreateFrom(this Department source)
        {
            return new Models.Department
            {
                DepartmentId = source.DepartmentId,
                DepartmentName = source.DepartmentName,
                DepartmentDesc = source.DepartmentDesc,
                //DepartmentNameA = source.DepartmentNameA,
                //CreatedBy = source.CreatedBy,
                //CreatedDate = source.CreatedDate,
                //UpdatedBy = source.UpdatedBy,
                //UpdatedDate = source.UpdatedDate
            };

        }
    }
}