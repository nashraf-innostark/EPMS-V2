using System;
using System.Configuration;
using System.Globalization;
using EPMS.Models.DomainModels;

namespace EPMS.Web.ModelMappers
{
    public static class EmployeeMapper
    {
        public static Employee CreateFrom(this Models.Employee source)
        {
            var caseType = new Employee
            {
                EmployeeId = source.EmployeeId,
                EmployeeFirstName = source.EmployeeFirstName ?? "",
                EmployeeMiddleName = source.EmployeeMiddleName ?? "",
                EmployeeLastName = source.EmployeeLastName ?? "",
                EmployeeImagePath = source.EmployeeImagePath,
                EmployeeIqama = source.EmployeeIqama ?? 0,
                EmployeeIqamaIssueDt = source.EmployeeIqamaIssueDt ?? DateTime.Now,
                EmployeeIqamaExpiryDt = source.EmployeeIqamaExpiryDt ?? DateTime.Now,
                EmployeeDOB = source.EmployeeDOB,
                EmpDateOfBirthArabic = source.EmpDateOfBirthArabic,
                EmployeeLandlineNum = source.EmployeeLandlineNum ?? "",
                MaritalStatus = source.MaritalStatus,
                EmployeeMobileNum = source.EmployeeMobileNum ?? "",
                JobTitleId = source.JobTitleId,
                EmployeeNationality = source.EmployeeNationality,
                EmployeePassportNum = source.EmployeePassportNum ?? "",
                EmployeePassportExpiryDt = source.EmployeePassportExpiryDt ?? DateTime.Now,
                EmployeeDetails = source.EmployeeDetails,
                RecCreatedBy = source.RecCreatedBy ?? "",
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy ?? "",
                RecLastUpdatedDt = source.RecLastUpdatedDt,
                EmployeeFullName = source.EmployeeFirstName + " " + source.EmployeeMiddleName + " " + source.EmployeeLastName,
                Email = source.Email,
            };
            return caseType;
        }
        public static Models.Employee CreateFrom(this Employee source)
        {
            return new Models.Employee
            {
                EmployeeId = source.EmployeeId,
                EmployeeFirstName = source.EmployeeFirstName ?? "",
                EmployeeMiddleName = source.EmployeeMiddleName ?? "",
                EmployeeLastName = source.EmployeeLastName ?? "",
                EmployeeImagePath = source.EmployeeImagePath,
                EmployeeIqama = source.EmployeeIqama ?? 0,
                EmployeeIqamaIssueDt = source.EmployeeIqamaIssueDt ?? DateTime.Now,
                EmployeeIqamaExpiryDt = source.EmployeeIqamaExpiryDt ?? DateTime.Now,
                EmployeeDOB = source.EmployeeDOB,
                EmpDateOfBirthArabic = source.EmpDateOfBirthArabic,
                EmployeeLandlineNum = source.EmployeeLandlineNum ?? "",
                MaritalStatus = source.MaritalStatus,
                EmployeeMobileNum = source.EmployeeMobileNum ?? "",
                JobTitleId = source.JobTitleId,
                EmployeeNationality = source.EmployeeNationality,
                EmployeePassportNum = source.EmployeePassportNum ?? "",
                EmployeePassportExpiryDt = source.EmployeePassportExpiryDt ?? DateTime.Now,
                EmployeeDetails = source.EmployeeDetails,
                JobTitleName = source.JobTitle.JobTitleName ?? "",
                DepartmentName = source.JobTitle.Department.DepartmentName ?? "",
                RecCreatedBy = source.RecCreatedBy ?? "",
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy ?? "",
                RecLastUpdatedDt = source.RecLastUpdatedDt,
                EmployeeFullName = source.EmployeeFirstName + " " + source.EmployeeMiddleName + " " + source.EmployeeLastName,
                Email = source.Email,
            };

        }
        public static Models.Employee CreateFromWithImage(this Employee source)
        {
            return new Models.Employee
            {
                EmployeeId = source.EmployeeId,
                EmployeeFirstName = source.EmployeeFirstName ?? "",
                EmployeeMiddleName = source.EmployeeMiddleName ?? "",
                EmployeeLastName = source.EmployeeLastName ?? "",
                EmployeeImagePath = source.EmployeeImagePath == null ? "" : ImageUrl(source.EmployeeImagePath),
                EmployeeIqama = source.EmployeeIqama ?? 0,
                EmployeeIqamaIssueDt = source.EmployeeIqamaIssueDt ?? DateTime.Now,
                EmployeeIqamaExpiryDt = source.EmployeeIqamaExpiryDt ?? DateTime.Now,
                EmployeeDOB = source.EmployeeDOB,
                EmpDateOfBirthArabic = source.EmpDateOfBirthArabic,
                EmployeeLandlineNum = source.EmployeeLandlineNum ?? "",
                MaritalStatus = source.MaritalStatus,
                EmployeeMobileNum = source.EmployeeMobileNum ?? "",
                JobTitleId = source.JobTitleId,
                EmployeeNationality = source.EmployeeNationality,
                EmployeePassportNum = source.EmployeePassportNum ?? "",
                EmployeePassportExpiryDt = source.EmployeePassportExpiryDt ?? DateTime.Now,
                EmployeeDetails = source.EmployeeDetails,
                JobTitleName = source.JobTitle.JobTitleName ?? "",
                DepartmentName = source.JobTitle.Department.DepartmentName ?? "",
                RecCreatedBy = source.RecCreatedBy ?? "",
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy ?? "",
                RecLastUpdatedDt = source.RecLastUpdatedDt,
                EmployeeFullName = source.EmployeeFirstName + " " + source.EmployeeMiddleName + " " + source.EmployeeLastName,
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