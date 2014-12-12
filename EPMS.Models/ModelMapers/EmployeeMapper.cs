using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ModelMapers
{
    public static class EmployeeMapper
    {
        public static void UpdateTo(this Employee source, Employee target)
        {
            //target.EmployeeId = source.EmployeeId;
            //target.Emp = source.EmpFirstNameA;
            //target.EmpFirstNameE = source.EmpFirstNameE;
            //target.EmpMiddleNameA = source.EmpMiddleNameA;
            //target.EmpMiddleNameE = source.EmpMiddleNameE;
            //target.EmpLastNameA = source.EmpLastNameA;
            //target.EmpLastNameE = source.EmpLastNameE;
            //target.EmpImage = source.EmpImage;
            //target.EmpIqama = source.EmpIqama;
            //target.IqamaIssueDate = source.IqamaIssueDate;
            //target.IqamaExpiryDate = source.IqamaExpiryDate;
            //target.EmpDateOfBirth = source.EmpDateOfBirth;
            //target.EmpDateOfBirthArabic = source.EmpDateOfBirthArabic;
            //target.EmpLandlineNumber = source.EmpLandlineNumber;
            //target.EmpMaritalStatus = source.EmpMaritalStatus;
            //target.EmpMobileNumber = source.EmpMobileNumber;
            //target.Nationality = source.Nationality;
            //target.JobId = source.JobId;
            //target.PassportId = source.PassportId;
            //target.PassportExpiryDate = source.PassportExpiryDate;
            //target.ExtraInfo = source.ExtraInfo;
            //target.CreatedBy = source.CreatedBy;
            //target.CreatedDate = source.CreatedDate;
            //target.UpdatedBy = source.UpdatedBy;
            //target.UpdatedDate = source.UpdatedDate;
        }
        public static Employee ServerToServer(this Employee source)
        {
            return new Employee
            {
                EmployeeId = source.EmployeeId,
                EmployeeFirstName = source.EmployeeFirstName,
                EmployeeMiddleName = source.EmployeeMiddleName,
                EmployeeLastName = source.EmployeeLastName,
                EmployeeFullName = source.EmployeeFirstName + " " + source.EmployeeMiddleName + " " + source.EmployeeLastName,
                EmployeeImagePath = source.EmployeeImagePath,
                JobTitleId = source.JobTitleId,
                EmployeeJobId = source.EmployeeJobId,
                EmployeeMobileNum = source.EmployeeMobileNum,
                EmployeeLandlineNum = source.EmployeeLandlineNum,
                MaritalStatus = source.MaritalStatus,
                EmployeeDOB = source.EmployeeDOB,
                EmpDateOfBirthArabic = source.EmpDateOfBirthArabic,
                EmployeeNationality = source.EmployeeNationality,
                EmployeeIqama = source.EmployeeIqama,
                EmployeeIqamaIssueDt = source.EmployeeIqamaIssueDt,
                EmployeeIqamaExpiryDt = source.EmployeeIqamaExpiryDt,
                EmployeePassportNum = source.EmployeePassportNum,
                EmployeePassportExpiryDt = source.EmployeePassportExpiryDt,
                EmployeeDetails = source.EmployeeDetails,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt
            };
        }
    }
}
