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
        public string EmpFirstNameE { get; set; }
        public string EmpFirstNameA { get; set; }
        public string EmpMiddleNameE { get; set; }
        public string EmpMiddleNameA { get; set; }
        public string EmpLastNameE { get; set; }
        public string EmpLastNameA { get; set; }
        public long JobId { get; set; }
        public string EmpImage { get; set; }
        public string EmpMobileNumber { get; set; }
        public string EmpLandlineNumber { get; set; }
        public string EmpMaritalStatus { get; set; }
        public DateTime EmpDateOfBirth { get; set; }
        public string EmpDateOfBirthArabic { get; set; }
        public string Nationality { get; set; }
        public Nullable<long> EmpIqama { get; set; }
        public Nullable<System.DateTime> IqamaIssueDate { get; set; }
        public Nullable<System.DateTime> IqamaExpiryDate { get; set; }
        public Nullable<long> PassportId { get; set; }
        public Nullable<System.DateTime> PassportExpiryDate { get; set; }
        public string ExtraInfo { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        public virtual JobTitle JobTitle { get; set; }

        public virtual JobTitle Department { get; set; }
    }
}
