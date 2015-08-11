using System;

namespace EPMS.WebModels.ModelMappers
{
    public static class JobTitleMapper
    {
        public static WebsiteModels.JobTitleDropDown CreateForDropDown(this Models.DomainModels.JobTitle source)
        {
            return new WebsiteModels.JobTitleDropDown
             {
                 JobTitleId = source.JobTitleId,
                 JobTitleName = source.JobTitleNameE,
                 BasicSalary = source.BasicSalary ?? 0,
             };
        }

        public static WebsiteModels.JobTitle CreateFrom(this Models.DomainModels.JobTitle source)
        {
            WebsiteModels.JobTitle retVal = new WebsiteModels.JobTitle();
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
        public static Models.DomainModels.JobTitle CreateFrom(this WebsiteModels.JobTitle source)
        {
            Models.DomainModels.JobTitle retVal = new Models.DomainModels.JobTitle();
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