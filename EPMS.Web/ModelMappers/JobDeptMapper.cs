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
        public static Models.JobTitleAndDepartment CreateFromServerToClient(this DomainModels.JobTitle source)
        {
            return new Models.JobTitleAndDepartment
            {
                JobId = source.JobTitleId,
                JobTitle = source.JobTitleName,
                DeptId = source.Department.DepartmentId,
                DeptName = source.Department.DepartmentName,
                BasicSalary = source.BasicSalary ?? 0,
            };
        }
    }
}