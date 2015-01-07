using ApiModels = EPMS.Web.Models;
using DomainModels = EPMS.Models.DomainModels;

namespace EPMS.Web.ModelMappers
{
    public static class JobOfferedMapper
    {
        public static Models.JobOffered CreateFrom(this DomainModels.JobOffered source)
        {
            return new Models.JobOffered
            {
                JobOfferedId = source.JobOfferedId,
                JobTitleId = source.JobTitleId,
                ShowBasicSalary = source.ShowBasicSalary,
                IsOpenStatus = source.IsOpen?"Open":"Close",
                IsOpen = source.IsOpen,
                NoOfPosts = source.NoOfPosts,
                DepartmentNameE = source.JobTitle.Department.DepartmentNameE,
                DepartmentNameA = source.JobTitle.Department.DepartmentNameA,
                JobTitleDescE = source.JobTitle.JobTitleDescE,
                JobTitleDescA = source.JobTitle.JobTitleDescA,
                BasicSalary = source.JobTitle.BasicSalary,
                JobTitleNameE = source.JobTitle.JobTitleNameE,
                JobTitleNameA = source.JobTitle.JobTitleNameA,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt
            };
        }

        public static DomainModels.JobOffered CreateFrom(this Models.JobOffered source)
        {
            return new DomainModels.JobOffered
            {
                JobOfferedId = source.JobOfferedId,
                JobTitleId = source.JobTitleId,
                ShowBasicSalary = source.ShowBasicSalary,
                IsOpen = source.IsOpen,
                NoOfPosts = source.NoOfPosts,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt
            };
        }
    }
}