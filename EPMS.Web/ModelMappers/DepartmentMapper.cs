using EPMS.Models.DomainModels;
using AreasModel = EPMS.Web.Areas.HR.Models;

namespace EPMS.Web.ModelMappers
{
    public static class DepartmentMapper
    {
        public static Department CreateFrom(this AreasModel.Department source)
        {
            var caseType = new Department
            {
                //DepartmentId = source.DepartmentId ?? 0,
                //DepartmentNameE = source.DepartmentNameE,
                //DepartmentNameA = source.DepartmentNameA,
                //DepartmentDesc = source.DepartmentDesc,
                //CreatedBy = source.CreatedBy,
                //CreatedDate = source.CreatedDate,
                //UpdatedBy = source.UpdatedBy,
                //UpdatedDate = source.UpdatedDate
            };
            return caseType;
        }
        public static AreasModel.Department CreateFrom(this Department source)
        {
            return new AreasModel.Department
            {
                //DepartmentId = source.DepartmentId,
                //DepartmentNameE = source.DepartmentNameE,
                //DepartmentNameA = source.DepartmentNameA,
                //DepartmentDesc = source.DepartmentDesc,
                //CreatedBy = source.CreatedBy,
                //CreatedDate = source.CreatedDate,
                //UpdatedBy = source.UpdatedBy,
                //UpdatedDate = source.UpdatedDate
            };

        }
    }
}