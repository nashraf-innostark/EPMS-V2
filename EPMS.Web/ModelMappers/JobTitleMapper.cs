using System;
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
            Models.JobTitle retVal = new ApiModels.JobTitle();
            retVal.JobTitleId = source.JobTitleId;
            retVal.JobTitleNameE = source.JobTitleNameE;
            retVal.JobTitleNameA = source.JobTitleNameA;
            string decspE = "";
            string decspA = "";
            if (!String.IsNullOrEmpty(source.JobTitleDescE))
            {
                decspE = source.JobTitleDescE.Replace("\n", "");
                decspE = decspE.Replace("\r", "");
            }
            if (!String.IsNullOrEmpty(source.JobTitleDescA))
            {
                decspA = source.JobTitleDescA.Replace("\n", "");
                decspA = decspA.Replace("\r", "");
            }
            retVal.JobTitleDescE = decspE;
            retVal.JobTitleDescA = decspA;
            retVal.DepartmentId = source.DepartmentId;
            retVal.BasicSalary = source.BasicSalary ?? 0;
            retVal.DepartmentNameE = source.Department.DepartmentNameE;
            retVal.DepartmentNameA = source.Department.DepartmentNameA;
            retVal.RecCreatedBy = source.RecCreatedBy;
            retVal.RecCreatedDt = source.RecCreatedDt;
            retVal.RecLastUpdatedBy = source.RecLastUpdatedBy;
            retVal.RecLastUpdatedDt = source.RecLastUpdatedDt;
            retVal.EmployeesCount = source.Employees.Count;
            return retVal;
        }
        public static DomainModels.JobTitle CreateFrom(this Models.JobTitle source)
        {
            DomainModels.JobTitle retVal = new DomainModels.JobTitle();
            retVal.JobTitleId = source.JobTitleId;
            retVal.JobTitleNameE = source.JobTitleNameE;
            retVal.JobTitleNameA = source.JobTitleNameA;
            string decspE = "";
            string decspA = "";
            if (!String.IsNullOrEmpty(source.JobTitleDescE))
            {
                decspE = source.JobTitleDescE.Replace("\n", "");
                decspE = decspE.Replace("\r", "");
            }
            if (!String.IsNullOrEmpty(source.JobTitleDescA))
            {
                decspA = source.JobTitleDescA.Replace("\n", "");
                decspA = decspA.Replace("\r", "");
            }
            retVal.JobTitleDescE = decspE;
            retVal.JobTitleDescA = decspA;
            retVal.DepartmentId = source.DepartmentId;
            retVal.BasicSalary = source.BasicSalary;
            retVal.RecCreatedBy = source.RecCreatedBy;
            retVal.RecCreatedDt = source.RecCreatedDt;
            retVal.RecLastUpdatedBy = source.RecLastUpdatedBy;
            retVal.RecLastUpdatedDt = source.RecLastUpdatedDt;
            return retVal;
        }
    }
}