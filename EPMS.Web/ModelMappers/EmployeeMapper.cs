using System;
using System.Configuration;
using EPMS.Models.DomainModels;
using AreasModel = EPMS.Web.Areas.HR.Models;

namespace EPMS.Web.ModelMappers
{
    public static class EmployeeMapper
    {
        public static Employee CreateFrom(this AreasModel.Employee source)
        {
            var caseType = new Employee
            {
                //EmployeeId = source.EmployeeId ?? 0,
                //EmpFirstNameA = source.EmpFirstNameA,
                //EmpFirstNameE = source.EmpFirstNameE,
                //EmpMiddleNameA = source.EmpMiddleNameA,
                //EmpMiddleNameE = source.EmpMiddleNameE,
                //EmpLastNameA = source.EmpLastNameA,
                //EmpLastNameE = source.EmpLastNameE,
                //EmpImage = source.EmpImage,
                //EmpIqama = source.EmpIqama,
                //IqamaIssueDate = source.IqamaIssueDate,
                //IqamaExpiryDate = source.IqamaExpiryDate,
                //EmpDateOfBirth = Convert.ToDateTime(source.EmpDateOfBirth),
                //EmpDateOfBirthArabic = source.EmpDateOfBirthArabic.HasValue ? source.EmpDateOfBirthArabic.Value.Date.ToString(CultureInfo.InvariantCulture) : string.Empty,
                //EmpLandlineNumber = source.EmpLandlineNumber,
                //EmpMaritalStatus = source.EmpMaritalStatus,
                //EmpMobileNumber = source.EmpMobileNumber,
                //Nationality = source.Nationality,
                //JobId = source.JobId,
                //PassportId = source.PassportId,
                //PassportExpiryDate = source.PassportExpiryDate,
                //ExtraInfo = source.ExtraInfo,
                //CreatedBy = source.CreatedBy,
                //CreatedDate = source.CreatedDate,
                //UpdatedBy = source.UpdatedBy,
                //UpdatedDate = source.UpdatedDate
            };
            return caseType;
        }
        public static AreasModel.Employee CreateFrom(this Employee source)
        {
            return new AreasModel.Employee
            {
                //EmployeeId = source.EmployeeId,
                //EmpFirstNameA = source.EmpFirstNameA ?? "",
                //EmpFirstNameE = source.EmpFirstNameE ?? "",
                //EmpMiddleNameA = source.EmpMiddleNameA ?? "",
                //EmpMiddleNameE = source.EmpMiddleNameE ?? "",
                //EmpLastNameA = source.EmpLastNameA ?? "",
                //EmpLastNameE = source.EmpLastNameE ?? "",
                //EmpImage = source.EmpImage ?? "",
                //EmpIqama = source.EmpIqama ?? 0,
                //IqamaIssueDate = source.IqamaIssueDate ?? DateTime.Now,
                //IqamaExpiryDate = source.IqamaExpiryDate ?? DateTime.Now,
                //EmpDateOfBirth = source.EmpDateOfBirth.ToShortDateString(),
                //EmpDateOfBirthArabic = Convert.ToDateTime(source.EmpDateOfBirth),
                ////EmpDateOfBirthArabic = !string.IsNullOrEmpty(source.EmpDateOfBirthArabic) ? (DateTime.Parse(source.EmpDateOfBirthArabic)).Date : (DateTime?)null,
                //EmpLandlineNumber = source.EmpLandlineNumber ?? "",
                //EmpMaritalStatus = source.EmpMaritalStatus ?? "",
                //EmpMobileNumber = source.EmpMobileNumber ?? "",
                //JobId = source.JobId,
                //Nationality = source.Nationality,
                //PassportId = source.PassportId ?? 0,
                //PassportExpiryDate = source.PassportExpiryDate ?? DateTime.Now,
                //ExtraInfo = source.ExtraInfo,
                //JobTitle = source.JobTitle.JobTitleNameE ?? "",
                //Department = source.JobTitle.Department.DepartmentNameE ?? "",
                //CreatedBy = source.CreatedBy ?? "",
                //CreatedDate = source.CreatedDate,
                //UpdatedBy = source.UpdatedBy ?? "",
                //UpdatedDate = source.UpdatedDate,
                //EmpFullName = source.EmpFirstNameE + " " + source.EmpMiddleNameE + " " + source.EmpLastNameE
            };

        }
        public static AreasModel.Employee CreateFromWithImage(this Employee source, string userName)
        {
            return new AreasModel.Employee
            {
                EmployeeId = source.EmployeeId,
                EmployeeFirstName = source.EmployeeFirstName ?? "",
                EmployeeMiddleName = source.EmployeeMiddleName?? "",
                EmployeeLastName = source.EmployeeLastName?? "",
                ImagePath = source.EmployeeImagePath== null ? "" : ImageUrl(userName, source.EmployeeImagePath),
                EmployeeIqama = source.EmployeeIqama?? 0,
                EmployeeIqamaIssueDt = source.EmployeeIqamaIssueDt ?? DateTime.Now,
                EmployeeIqamaExpiryDt = source.EmployeeIqamaExpiryDt ?? DateTime.Now,
                EmployeeDOB = source.EmployeeDOB,
                EmpDateOfBirthArabic = source.EmpDateOfBirthArabic,
                EmployeeLandlineNum = source.EmployeeLandlineNum?? "",
                MaritalStatus = source.MaritalStatus,
                EmployeeMobileNum = source.EmployeeMobileNum?? "",
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
                EmployeeFullName = source.EmployeeFirstName + " " + source.EmployeeMiddleName + " " + source.EmployeeLastName
            };

        }
        private static string ImageUrl(string userName, string imageName)
        {
            string path = (ConfigurationManager.AppSettings["SiteURL"] + ConfigurationManager.AppSettings["EmployeeImage"] + userName + "/" + imageName).Replace("~", "");

            return "<img  data-mfp-src=" + path + " src=" + path + " class='mfp-image image-link cursorHand' height=70 width=100 />";
        }
    }
}