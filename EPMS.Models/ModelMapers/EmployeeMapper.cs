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
            target.EmployeeId = source.EmployeeId;
            target.EmployeeFirstName = source.EmployeeFirstName;
            target.EmployeeMiddleName = source.EmployeeMiddleName;
            target.EmployeeLastName = source.EmployeeLastName;
            target.EmployeeFullName = source.EmployeeFirstName + " " + source.EmployeeMiddleName + " " + source.EmployeeLastName;
            target.EmployeeImagePath = source.EmployeeImagePath;
            target.JobTitleId = source.JobTitleId;
            target.EmployeeJobId = source.EmployeeJobId;
            target.EmployeeMobileNum = source.EmployeeMobileNum;
            target.EmployeeLandlineNum = source.EmployeeLandlineNum;
            target.MaritalStatus = source.MaritalStatus;
            target.EmployeeDOB = source.EmployeeDOB;
            target.EmpDateOfBirthArabic = source.EmpDateOfBirthArabic;
            target.EmployeeNationality = source.EmployeeNationality;
            target.EmployeeIqama = source.EmployeeIqama;
            target.EmployeeIqamaIssueDt = source.EmployeeIqamaIssueDt;
            target.EmployeeIqamaExpiryDt = source.EmployeeIqamaExpiryDt;
            target.EmployeePassportNum = source.EmployeePassportNum;
            target.EmployeePassportExpiryDt = source.EmployeePassportExpiryDt;
            target.EmployeeDetails = source.EmployeeDetails;
            target.RecCreatedBy = source.RecCreatedBy;
            target.RecCreatedDt = source.RecCreatedDt;
            target.RecLastUpdatedBy = source.RecLastUpdatedBy;
            target.RecLastUpdatedDt = source.RecLastUpdatedDt;
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
