using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;

namespace EPMS.Web.Models
{
    public class Employee
    {
        public long EmployeeId { get; set; }
        [Required(ErrorMessage = "Employee Name (English) is required.")]
        [Display(Name = "Employee Name")]
        [StringLength(100, ErrorMessage = "Cannot exceed 100 characters.")]
        public string EmployeeNameE { get; set; }
        [Required(ErrorMessage = "Employee Name (Arabic) is required.")]
        [Display(Name = "Employee Name")]
        [StringLength(100, ErrorMessage = "Cannot exceed 100 characters.")]
        public string EmployeeNameA { get; set; }
        public string EmployeeImagePath { get; set; }
        public long? JobTitleId { get; set; }
        public string EmployeeJobId { get; set; }
        [Integer(ErrorMessage = "Mobile Number needs to be number")]
        [Required(ErrorMessage = "Mobile Number is required.")]
        [StringLength(200, ErrorMessage = "Cannot exceed 200 characters.")]
        public string EmployeeMobileNum { get; set; }
        [Integer(ErrorMessage = "LandLine Number needs to be number")]
        [Required(ErrorMessage = "LandLine Number is required.")]
        [StringLength(200, ErrorMessage = "Cannot exceed 200 characters.")]
        public string EmployeeLandlineNum { get; set; }
        public byte? MaritalStatus { get; set; }
        [Required(ErrorMessage = "Date of Birth is required.")]
        public string EmployeeDOB { get; set; }
        public string EmpDateOfBirthArabic { get; set; }
        public short? EmployeeNationality { get; set; }
        [Required(ErrorMessage = "Iqama Number Or National ID Number is required.")]
        public int? EmployeeIqama { get; set; }
        public string EmployeeIqamaIssueDt { get; set; }
        public string EmployeeIqamaIssueDtAr { get; set; }
        [Required(ErrorMessage = "Iqama Expiry Date is required.")]
        public string EmployeeIqamaExpiryDt { get; set; }
        public string EmployeeIqamaExpiryDtAr { get; set; }
        [Required(ErrorMessage = "Passport ID is required.")]
        [StringLength(200, ErrorMessage = "Cannot exceed 200 characters.")]
        public string EmployeePassportNum { get; set; }
        [Required(ErrorMessage = "Passport Expiry Date is required.")]
        public string EmployeePassportExpiryDt { get; set; }
        public string EmployeePassportExpiryDtAr { get; set; }
        [StringLength(1000, ErrorMessage = "Cannot exceed 1000 characters.")]
        public string EmployeeDetailsE { get; set; }
        public string EmployeeFullName { get; set; }
        public DateTime? RecCreatedDt { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecLastUpdatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string Email { get; set; }
        public string EmployeeDetailsA { get; set; }

        public IEnumerable<Allowance> Allowances { get; set; }
        public JobTitle JobTitle { get; set; }
    }
}