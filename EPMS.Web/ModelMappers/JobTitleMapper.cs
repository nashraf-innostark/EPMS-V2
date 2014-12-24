using ApiModels = EPMS.Web.Models;
using DomainModels = EPMS.Models.DomainModels;

namespace EPMS.Web.ModelMappers
{
    public static class JobTitleMapper
    {
        public static ApiModels.JobTitleDropDown CreateForDropDown(this DomainModels.JobTitle source)
        {
            return new ApiModels.JobTitleDropDown
             {
                 JobTitleId = source.JobTitleId,
                 JobTitleName = source.JobTitleNameE,
                 BasicSalary = source.BasicSalary ?? 0,
             };
        }

        public static Models.JobTitle CreateFrom(this DomainModels.JobTitle source)
        {
            return new Models.JobTitle
            {
                JobTitleId = source.JobTitleId,
                JobTitleNameE = source.JobTitleNameE,
                JobTitleNameA = source.JobTitleNameA,
                JobTitleDescE = source.JobTitleDescE,
                JobTitleDescA = source.JobTitleDescA,
                DepartmentId = source.DepartmentId,
                BasicSalary = source.BasicSalary,
                Department = source.Department.CreateFrom(),
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt
            };

        }
        public static DomainModels.JobTitle CreateFrom(this Models.JobTitle source)
        {
            return new DomainModels.JobTitle
            {
                JobTitleId = source.JobTitleId,
                JobTitleNameE = source.JobTitleNameE,
                JobTitleNameA = source.JobTitleNameA,
                JobTitleDescE = source.JobTitleDescE,
                JobTitleDescA = source.JobTitleDescA,
                DepartmentId = source.DepartmentId,
                BasicSalary = source.BasicSalary,
                //Department = source.Department.CreateFrom(),
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt
            };

        }
    }
}