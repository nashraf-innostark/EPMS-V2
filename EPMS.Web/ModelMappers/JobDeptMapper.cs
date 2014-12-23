using System;
using DomainModels = EPMS.Models.DomainModels;

namespace EPMS.Web.ModelMappers
{
    public static class JobDeptMapper
    {
        /// <summary>
        /// Mapper for displaying Job Titles and Department Name on AddEdit page
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Models.JobTitleAndDepartment CreateFromJob(this DomainModels.JobTitle source)
        {
            return new Models.JobTitleAndDepartment
            {
                JobId = source.JobTitleId,
                JobTitle = source.JobTitleName,
                DeptId = source.Department.DepartmentId,
                DeptNameE = source.Department.DepartmentNameE,
                DeptNameA = source.Department.DepartmentNameA,
                BasicSalary = source.BasicSalary ?? 0,
            };
        }
    }
}