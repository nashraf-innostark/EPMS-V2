using System;
using System.Configuration;
using EPMS.Models.DomainModels;

namespace EPMS.Web.ModelMappers
{
    public static class EmployeeMapper
    {
        public static Employee CreateFromClientToServer(this Models.Employee source)
        {
            var caseType = new Employee
            {
                EmployeeId = source.EmployeeId,
                EmployeeNameE = source.EmployeeNameE ?? "",
                EmployeeNameA = source.EmployeeNameA?? "",
                EmployeeImagePath = source.EmployeeImagePath,
                EmployeeIqama = source.EmployeeIqama ?? 0,
                EmployeeIqamaIssueDt = source.EmployeeIqamaIssueDt ?? DateTime.Now,
                EmployeeIqamaExpiryDt = source.EmployeeIqamaExpiryDt ?? DateTime.Now,
                EmployeeDOB = source.EmployeeDOB,
                EmployeeLandlineNum = source.EmployeeLandlineNum ?? "",
                MaritalStatus = source.MaritalStatus,
                EmployeeMobileNum = source.EmployeeMobileNum ?? "",
                JobTitleId = source.JobTitleId,
                EmployeeNationality = source.EmployeeNationality,
                EmployeePassportNum = source.EmployeePassportNum ?? "",
                EmployeePassportExpiryDt = source.EmployeePassportExpiryDt ?? DateTime.Now,
                EmployeeDetailsE = source.EmployeeDetailsE,
                EmployeeDetailsA = source.EmployeeDetailsA,
                RecCreatedBy = source.RecCreatedBy ?? "",
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy ?? "",
                RecLastUpdatedDt = source.RecLastUpdatedDt,
                Email = source.Email,
            };
            return caseType;
        }
        public static Models.Employee CreateFromServerToClient(this Employee source)
        {
            return new Models.Employee
            {
                EmployeeId = source.EmployeeId,
                EmployeeNameE = source.EmployeeNameE ?? "",
                EmployeeNameA = source.EmployeeNameA ?? "",
                EmployeeImagePath = source.EmployeeImagePath,
                EmployeeIqama = source.EmployeeIqama ?? 0,
                EmployeeIqamaIssueDt = source.EmployeeIqamaIssueDt ?? DateTime.Now,
                EmployeeIqamaExpiryDt = source.EmployeeIqamaExpiryDt ?? DateTime.Now,
                EmployeeDOB = source.EmployeeDOB,
                EmployeeLandlineNum = source.EmployeeLandlineNum ?? "",
                MaritalStatus = source.MaritalStatus,
                EmployeeMobileNum = source.EmployeeMobileNum ?? "",
                JobTitleId = source.JobTitleId,
                EmployeeNationality = source.EmployeeNationality,
                EmployeePassportNum = source.EmployeePassportNum ?? "",
                EmployeePassportExpiryDt = source.EmployeePassportExpiryDt ?? DateTime.Now,
                EmployeeDetailsE = source.EmployeeDetailsE,
                EmployeeDetailsA = source.EmployeeDetailsA,
                RecCreatedBy = source.RecCreatedBy ?? "",
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy ?? "",
                RecLastUpdatedDt = source.RecLastUpdatedDt,
                Email = source.Email,
                JobTitle = source.JobTitle.CreateFrom(),
            };

        }
        public static Models.Employee CreateFromServerToClientWithImage(this Employee source)
        {
            return new Models.Employee
            {
                EmployeeId = source.EmployeeId,
                EmployeeNameE = source.EmployeeNameE ?? "",
                EmployeeNameA = source.EmployeeNameA ?? "",
                EmployeeImagePath = source.EmployeeImagePath == null ? "" : ImageUrl(source.EmployeeImagePath),
                EmployeeIqama = source.EmployeeIqama ?? 0,
                EmployeeIqamaIssueDt = source.EmployeeIqamaIssueDt ?? DateTime.Now,
                EmployeeIqamaExpiryDt = source.EmployeeIqamaExpiryDt ?? DateTime.Now,
                EmployeeDOB = source.EmployeeDOB,
                EmployeeLandlineNum = source.EmployeeLandlineNum ?? "",
                MaritalStatus = source.MaritalStatus,
                EmployeeMobileNum = source.EmployeeMobileNum ?? "",
                JobTitleId = source.JobTitleId,
                EmployeeNationality = source.EmployeeNationality,
                EmployeePassportNum = source.EmployeePassportNum ?? "",
                EmployeePassportExpiryDt = source.EmployeePassportExpiryDt ?? DateTime.Now,
                EmployeeDetailsE = source.EmployeeDetailsE,
                EmployeeDetailsA = source.EmployeeDetailsA,
                RecCreatedBy = source.RecCreatedBy ?? "",
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy ?? "",
                RecLastUpdatedDt = source.RecLastUpdatedDt,
                Email = source.Email,
            };

        }
        private static string ImageUrl(string imageName)
        {
            string path = (ConfigurationManager.AppSettings["SiteURL"] + ConfigurationManager.AppSettings["EmployeeImage"] + "/" + imageName);

            return "<img  data-mfp-src=" + path + " src=" + path + " class='mfp-image image-link cursorHand' height=70 width=100 />";
        }
    }
}