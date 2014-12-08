using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPMS.Models.DomainModels;

namespace EPMS.Web.ModelMappers
{
    public static class DepartmentMapper
    {
        public static Department CreateFrom(this Models.Department source)
        {
            var caseType = new Department
            {
                DepartmentId = source.DepartmentId ?? 0,
                DepartmentNameE = source.DepartmentNameE,
                DepartmentNameA = source.DepartmentNameA,
                DepartmentDesc = source.DepartmentDesc,
                CreatedBy = source.CreatedBy,
                CreatedDate = source.CreatedDate,
                UpdatedBy = source.UpdatedBy,
                UpdatedDate = source.UpdatedDate
            };
            return caseType;
        }
        public static Models.Department CreateFrom(this Department source)
        {
            return new Models.Department
            {
                DepartmentId = source.DepartmentId,
                DepartmentNameE = source.DepartmentNameE,
                DepartmentNameA = source.DepartmentNameA,
                DepartmentDesc = source.DepartmentDesc,
                CreatedBy = source.CreatedBy,
                CreatedDate = source.CreatedDate,
                UpdatedBy = source.UpdatedBy,
                UpdatedDate = source.UpdatedDate
            };

        }
    }
}