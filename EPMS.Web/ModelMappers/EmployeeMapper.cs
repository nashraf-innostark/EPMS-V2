using System;
using System.Configuration;
using System.IO;
using System.Linq;
using EPMS.Models.DomainModels;
using EPMS.Web.ModelMappers;

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
                EmployeeJobId = source.EmployeeJobId,
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
                EmployeeJobId = source.EmployeeJobId,
                //EmployeeRequests = source.EmployeeRequests.Select(x=>x.CreateFromServerToClient()),
                Allowances = source.Allowances.Select(x=>x.CreateFromServerToClient()),
            };

        }
        public static Models.Employee CreateFromServerToClientWithImage(this Employee source)
        {
            return new Models.Employee
            {
                EmployeeId = source.EmployeeId,
                EmployeeNameE = source.EmployeeNameE ?? "",
                EmployeeNameA = source.EmployeeNameA ?? "",
                EmployeeImagePath = ImageUrl(source.EmployeeImagePath),
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
                EmployeeJobId = source.EmployeeJobId,
                JobTitle = source.JobTitle.CreateFrom(),
                //EmployeeRequests = source.EmployeeRequests.Select(x => x.CreateFromServerToClient()),
                Allowances = source.Allowances.Select(x => x.CreateFromServerToClient()),
            };

        }
        private static string ImageUrl(string imageName)
        {
            if (string.IsNullOrEmpty(imageName))
            {
                imageName = "profile.jpg";
            }
            string path = (ConfigurationManager.AppSettings["SiteURL"] + ConfigurationManager.AppSettings["EmployeeImage"] + imageName);
            
            return "<img  data-mfp-src=" + path + " src=" + path + " class='mfp-image image-link cursorHand' height=70 width=100 />";
        }
    }
}