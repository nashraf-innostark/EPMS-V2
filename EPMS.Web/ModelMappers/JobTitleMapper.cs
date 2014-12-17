using ApiModels = EPMS.Web.Models;
using DomainModels = EPMS.Models.DomainModels;
using AreasModel=EPMS.Web.Areas.HR.Models;

namespace EPMS.Web.ModelMappers
{
    public static class JobTitleMapper
    {
        public static ApiModels.JobTitleDropDown CreateFromDropDown(this DomainModels.JobTitle source)
        {
            return new ApiModels.JobTitleDropDown
             {
                 JobTitleId = source.JobTitleId,
                 JobTitleName = source.JobTitleName,
             };
        }

        public static AreasModel.JobTitle CreateFrom(this DomainModels.JobTitle source)
        {
            return new AreasModel.JobTitle
            {
                //JobId = source.JobId,
                //JobTitleNameE = source.JobTitleNameE,
                //JobTitleNameA = source.JobTitleNameA,
                //JobDescriptionE = source.JobDescriptionE,
                //JobDescriptionA = source.JobDescriptionA,
                //DepartmentId = source.DepartmentId,
                //BasicSalary = source.BasicSalary,
                //Department = source.Department.CreateFrom(),
                //CreatedBy = source.CreatedBy,
                //CreatedDate = source.CreatedDate,
                //UpdatedBy = source.UpdatedBy,
                //UpdatedDate = source.UpdatedDate
            };

        }
        public static DomainModels.JobTitle CreateFrom(this AreasModel.JobTitle source)
        {
            return new DomainModels.JobTitle
            {
                //JobId = source.JobId,
                //JobTitleNameE = source.JobTitleNameE,
                //JobTitleNameA = source.JobTitleNameA,
                //JobDescriptionE = source.JobDescriptionE,
                //JobDescriptionA = source.JobDescriptionA,
                //DepartmentId = source.DepartmentId,
                //BasicSalary = source.BasicSalary,
                //CreatedBy = source.CreatedBy,
                //CreatedDate = source.CreatedDate,
                //UpdatedBy = source.UpdatedBy,
                //UpdatedDate = source.UpdatedDate
            };

        }
    }
}