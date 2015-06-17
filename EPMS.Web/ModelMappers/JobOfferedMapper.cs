using System;
using ApiModels = EPMS.Web.Models;
using DomainModels = EPMS.Models.DomainModels;

namespace EPMS.Web.ModelMappers
{
    public static class JobOfferedMapper
    {
        public static Models.JobOffered CreateFrom(this DomainModels.JobOffered source)
        {

            Models.JobOffered retVal = new ApiModels.JobOffered();
            retVal.JobOfferedId = source.JobOfferedId;
            retVal.JobTitleId = source.JobTitleId;
            retVal.ShowBasicSalary = source.ShowBasicSalary;
            retVal.IsOpenStatus = source.IsOpen ? Resources.HR.JobOffered.Open : Resources.HR.JobOffered.Close;
            retVal.IsOpen = source.IsOpen;
            retVal.NoOfPosts = source.NoOfPosts;
            retVal.DepartmentNameE = source.JobTitle.Department.DepartmentNameE;
            retVal.DepartmentNameA = source.JobTitle.Department.DepartmentNameA;
            string decspE = "";
            string decspA = "";
            if (!String.IsNullOrEmpty(source.JobTitle.JobTitleDescE))
            {
                decspE = source.JobTitle.JobTitleDescE.Replace("\n", "");
                decspE = decspE.Replace("\r", "");
                decspE = decspE.Replace("\t", "");
            }
            if (!String.IsNullOrEmpty(source.JobTitle.JobTitleDescA))
            {
                decspA = source.JobTitle.JobTitleDescA.Replace("\n", "");
                decspA = decspA.Replace("\r", "");
                decspA = decspA.Replace("\t", "");
            }
            retVal.JobTitleDescE = decspE;
            retVal.JobTitleDescShortE = decspE.Length > 20 ? decspE.Substring(0, 20) + "..." : decspE;
            retVal.JobTitleDescA = decspA;
            retVal.JobTitleDescShortA = decspA.Length > 20 ? decspA.Substring(0, 20) + "..." : decspA;
            retVal.BasicSalary = source.JobTitle.BasicSalary;
            retVal.JobTitleNameE = source.JobTitle.JobTitleNameE;
            retVal.JobTitleNameA = source.JobTitle.JobTitleNameA;
            retVal.RecCreatedBy = source.RecCreatedBy;
            retVal.RecCreatedDt = source.RecCreatedDt;
            retVal.RecLastUpdatedBy = source.RecLastUpdatedBy;
            retVal.RecLastUpdatedDt = source.RecLastUpdatedDt;
            return retVal;
        }
        public static Models.JobOffered CreateFromServerToClientForJobs(this DomainModels.JobOffered source)
        {

            Models.JobOffered retVal = new ApiModels.JobOffered();
            retVal.JobOfferedId = source.JobOfferedId;
            retVal.JobTitleId = source.JobTitleId;
            retVal.ShowBasicSalary = source.ShowBasicSalary;
            retVal.IsOpenStatus = source.IsOpen ? Resources.HR.JobOffered.Open : Resources.HR.JobOffered.Close;
            retVal.IsOpen = source.IsOpen;
            retVal.NoOfPosts = source.NoOfPosts;
            retVal.DepartmentNameE = source.JobTitle.Department.DepartmentNameE;
            retVal.DepartmentNameA = source.JobTitle.Department.DepartmentNameA;
            string decspE = "";
            string decspA = "";
            if (!String.IsNullOrEmpty(source.JobTitle.JobTitleDescE))
            {
                decspE = source.JobTitle.JobTitleDescE.Replace("\n", "");
                decspE = decspE.Replace("\r", "");
                decspE = decspE.Replace("\t", "");
            }
            if (!String.IsNullOrEmpty(source.JobTitle.JobTitleDescA))
            {
                decspA = source.JobTitle.JobTitleDescA.Replace("\n", "");
                decspA = decspA.Replace("\r", "");
                decspA = decspA.Replace("\t", "");
            }
            decspE = decspE.Replace("<p>", " ");
            decspE = decspE.Replace("</p>", " ");
            retVal.JobTitleDescE = decspE;
            retVal.JobTitleDescShortE = decspE.Length > 20 ? decspE.Substring(0, 20) + "..." : decspE;
            decspA = decspA.Replace("<p>", " ");
            decspA = decspA.Replace("</p>", " ");
            retVal.JobTitleDescA = decspA;
            retVal.JobTitleDescShortA = decspA.Length > 20 ? decspA.Substring(0, 20) + "..." : decspA;
            retVal.BasicSalary = source.JobTitle.BasicSalary;
            retVal.JobTitleNameE = source.JobTitle.JobTitleNameE;
            retVal.JobTitleNameA = source.JobTitle.JobTitleNameA;
            retVal.RecCreatedBy = source.RecCreatedBy;
            retVal.RecCreatedDt = source.RecCreatedDt;
            retVal.RecLastUpdatedBy = source.RecLastUpdatedBy;
            retVal.RecLastUpdatedDt = source.RecLastUpdatedDt;
            return retVal;
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

        public static DashboardModels.Recruitment CreateForDashboard(this DomainModels.JobOffered source)
        {
            return new DashboardModels.Recruitment
            {
                JobOfferedId = source.JobOfferedId,
                NoOfApplicants = source.JobApplicants.Count,
                TitleE = source.JobTitle.JobTitleNameE,
                TitleA = source.JobTitle.JobTitleNameA
            };
        }
    }
}