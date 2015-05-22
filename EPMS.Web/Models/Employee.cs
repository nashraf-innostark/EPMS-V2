using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EPMS.Web.Models
{
    public class Employee
    {
        public long EmployeeId { get; set; }
        [Required(ErrorMessage = "Employee First Name (English) is required.")]
        [Display(Name = "Employee First Name")]
        [StringLength(100, ErrorMessage = "Cannot exceed 100 characters.")]
        public string EmployeeFirstNameE { get; set; }
        [Required(ErrorMessage = "Employee First Name (Arabic) is required.")]
        [Display(Name = "Employee First Name")]
        [StringLength(100, ErrorMessage = "Cannot exceed 100 characters.")]
        public string EmployeeFirstNameA { get; set; }
        public string EmployeeMiddleNameE { get; set; }
        public string EmployeeMiddleNameA { get; set; }
        [Required(ErrorMessage = "Employee Last Name (English) is required.")]
        [Display(Name = "Employee Last Name")]
        [StringLength(100, ErrorMessage = "Cannot exceed 100 characters.")]
        public string EmployeeLastNameE { get; set; }
        [Required(ErrorMessage = "Employee Last Name (Arabic) is required.")]
        [Display(Name = "Employee Last Name")]
        [StringLength(100, ErrorMessage = "Cannot exceed 100 characters.")]
        public string EmployeeLastNameA { get; set; }
        public string EmployeeImagePath { get; set; }
        public long? JobTitleId { get; set; }
        public string EmployeeJobId { get; set; }
        [Required(ErrorMessage = "Mobile Number is required.")]
        [Range(1, 10000000000000000000, ErrorMessage = "Please enter a valid number between 1 to 20.")]
        public string EmployeeMobileNum { get; set; }
        [Range(1, 10000000000000000000, ErrorMessage = "Please enter a valid number between 1 to 20.")]
        public string EmployeeLandlineNum { get; set; }
        public byte? MaritalStatus { get; set; }
        public string EmployeeDOB { get; set; }
        public string EmpDateOfBirthArabic { get; set; }
        public string EmployeeNationality { get; set; }
        [Range(1, 10000000000000000000, ErrorMessage = "Please enter a valid number between 1 to 20.")]
        public string EmployeeIqama { get; set; }
        public string EmployeeIqamaIssueDt { get; set; }
        public string EmployeeIqamaIssueDtAr { get; set; }
        public string EmployeeIqamaExpiryDt { get; set; }
        public string EmployeeIqamaExpiryDtAr { get; set; }
        public string EmployeePassportNum { get; set; }
        public string EmployeePassportExpiryDt { get; set; }
        public string EmployeePassportExpiryDtAr { get; set; }
        [StringLength(1000, ErrorMessage = "Cannot exceed 1000 characters.")]
        public string EmployeeDetailsE { get; set; }
        [StringLength(1000, ErrorMessage = "Cannot exceed 1000 characters.")]
        public string EmployeeDetailsA { get; set; }
        public string EmployeeFullNameE { get; set; }
        public string EmployeeFullNameA { get; set; }
        public DateTime? RecCreatedDt { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecLastUpdatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }
        public long PrevJobTitleId { get; set; }
        public bool? IsActivated { get; set; }

        public IEnumerable<Allowance> Allowances { get; set; }
        public JobTitle JobTitle { get; set; }
    }
}