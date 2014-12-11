using System;
using System.Collections.Generic;

namespace EPMS.Models.DomainModels
{
    public class Employee
    {
        public long EmployeeId { get; set; }
        public string EmployeeFirstName { get; set; }
        public string EmployeeMiddleName { get; set; }
        public string EmployeeLastName { get; set; }
        public string EmployeeImagePath { get; set; }
        public long? JobTitleId { get; set; }
        public string EmployeeJobId { get; set; }
        public string EmployeeMobileNum { get; set; }
        public string EmployeeLandlineNum { get; set; }
        public byte? MaritalStatus { get; set; }
        // ReSharper disable once InconsistentNaming
        public DateTime? EmployeeDOB { get; set; }
        public string EmpDateOfBirthArabic { get; set; }
        public short? EmployeeNationality { get; set; }
        public int? EmployeeIqama { get; set; }
        public DateTime? EmployeeIqamaIssueDt { get; set; }
        public DateTime? EmployeeIqamaExpiryDt { get; set; }
        public string EmployeePassportNum { get; set; }
        public DateTime? EmployeePassportExpiryDt { get; set; }
        public string EmployeeDetails { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime? RecLastUpdatedDt { get; set; }
    
        public virtual ICollection<Allowance> Allowances { get; set; }
        public virtual JobTitle JobTitle { get; set; }
        public virtual ICollection<EmployeeRequest> EmployeeRequests { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
