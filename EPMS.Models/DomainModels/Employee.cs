using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace EPMS.Models.DomainModels
{
    public class Employee
    {
        public long EmployeeId { get; set; }
        public string EmployeeFirstName { get; set; }
        public string EmployeeMiddleName { get; set; }
        public string EmployeeLastName { get; set; }
        public string EmployeeImagePath { get; set; }
        public Nullable<long> JobTitleId { get; set; }
        public string EmployeeJobId { get; set; }
        public string EmployeeMobileNum { get; set; }
        public string EmployeeLandlineNum { get; set; }
        public byte? MaritalStatus { get; set; }
        public Nullable<System.DateTime> EmployeeDOB { get; set; }
        public string EmpDateOfBirthArabic { get; set; }
        public Nullable<short> EmployeeNationality { get; set; }
        public Nullable<int> EmployeeIqama { get; set; }
        public Nullable<System.DateTime> EmployeeIqamaIssueDt { get; set; }
        public Nullable<System.DateTime> EmployeeIqamaExpiryDt { get; set; }
        public string EmployeePassportNum { get; set; }
        public Nullable<System.DateTime> EmployeePassportExpiryDt { get; set; }
        public string EmployeeDetails { get; set; }
        public string RecCreatedBy { get; set; }
        public Nullable<System.DateTime> RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public Nullable<System.DateTime> RecLastUpdatedDt { get; set; }
    
        public virtual ICollection<Allowance> Allowances { get; set; }
        public virtual JobTitle JobTitle { get; set; }
        public virtual ICollection<EmployeeRequest> EmployeeRequests { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
