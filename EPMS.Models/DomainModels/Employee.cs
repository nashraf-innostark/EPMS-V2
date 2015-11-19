using System;
using System.Collections.Generic;

namespace EPMS.Models.DomainModels
{
    public class Employee
    {
        public long EmployeeId { get; set; }
        public string EmployeeFirstNameE { get; set; }
        public string EmployeeMiddleNameE { get; set; }
        public string EmployeeLastNameE { get; set; }
        public string EmployeeFirstNameA { get; set; }
        public string EmployeeMiddleNameA { get; set; }
        public string EmployeeLastNameA { get; set; }
        public string EmployeeImagePath { get; set; }
        public long? JobTitleId { get; set; }
        public string EmployeeJobId { get; set; }
        public string EmployeeMobileNum { get; set; }
        public string EmployeeLandlineNum { get; set; }
        public byte? MaritalStatus { get; set; }
        public DateTime? EmployeeDOB { get; set; }
        public string EmployeeNationality { get; set; }
        public string EmployeeIqama { get; set; }
        public DateTime? EmployeeIqamaIssueDt { get; set; }
        public DateTime? EmployeeIqamaExpiryDt { get; set; }
        public string EmployeePassportNum { get; set; }
        public DateTime? EmployeePassportExpiryDt { get; set; }
        public string EmployeeDetailsE { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime? RecLastUpdatedDt { get; set; }
        public string Email { get; set; }
        public string EmployeeDetailsA { get; set; }
        public bool? IsActivated { get; set; }

        public virtual ICollection<Allowance> Allowances { get; set; }
        public virtual ICollection<AspNetUser> AspNetUsers { get; set; }
        public virtual JobTitle JobTitle { get; set; }
        public virtual ICollection<EmployeeJobHistory> EmployeeJobHistories { get; set; }
        public virtual ICollection<EmployeeRequest> EmployeeRequests { get; set; }
        public virtual ICollection<MeetingAttendee> MeetingAttendees { get; set; }
        public virtual ICollection<NotificationRecipient> NotificationRecipients { get; set; }
        public virtual ICollection<TaskEmployee> TaskEmployees { get; set; }
        public virtual ICollection<Warehouse> Warehouses { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
    }
}
