namespace EPMS.WebModels.ModelMappers
{
    public static class JobDeptMapper
    {
        /// <summary>
        /// Mapper for displaying Job Titles and Department Name on AddEdit page
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static WebsiteModels.JobTitleAndDepartment CreateFromServerToClient(this Models.DomainModels.JobTitle source)
        {
            return new WebsiteModels.JobTitleAndDepartment
            {
                JobId = source.JobTitleId,
                JobTitleE = source.JobTitleNameE,
                JobTitleA = source.JobTitleNameA,
                DeptId = source.Department.DepartmentId,
                DeptNameE = source.Department.DepartmentNameE,
                DeptNameA = source.Department.DepartmentNameA,
                BasicSalary = source.BasicSalary ?? 0,
            };
        }
    }
}