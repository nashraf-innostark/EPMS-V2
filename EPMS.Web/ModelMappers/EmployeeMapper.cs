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
        public static AreasModel.Employee CreateFromWithImage(this Employee source, string UserName)
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
                //EmpImage = source.EmpImage == null ? "" : ImageUrl(UserName, source.EmpImage),
                //EmpIqama = source.EmpIqama ?? 0,
                //IqamaIssueDate = source.IqamaIssueDate ?? DateTime.Now,
                //IqamaExpiryDate = source.IqamaExpiryDate ?? DateTime.Now,
                //EmpDateOfBirth = source.EmpDateOfBirth.ToShortDateString(),
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
        private static string ImageUrl(string userName, string imageName)
        {
            string path = (ConfigurationManager.AppSettings["SiteURL"] + ConfigurationManager.AppSettings["EmployeeImage"] + userName + "/" + imageName).Replace("~", "");

            return "<img  data-mfp-src=" + path + " src=" + path + " class='mfp-image image-link cursorHand' height=70 width=100 />";
        }
    }
}