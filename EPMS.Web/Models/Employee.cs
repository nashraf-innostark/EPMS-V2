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
        public string EmployeeNameE { get; set; }
        [Required(ErrorMessage = "Employee Name (Arabic) is required.")]
        [Display(Name = "Employee Name")]
        public string EmployeeNameA { get; set; }
        public string EmployeeImagePath { get; set; }
        public long? JobTitleId { get; set; }
        public string EmployeeJobId { get; set; }
        [Integer(ErrorMessage = "Mobile Number needs to be number")]
        [Required(ErrorMessage = "Mobile Number is required.")]
        public string EmployeeMobileNum { get; set; }
        [Integer(ErrorMessage = "LandLine Number needs to be number")]
        [Required(ErrorMessage = "LandLine Number is required.")]
        public string EmployeeLandlineNum { get; set; }
        public byte? MaritalStatus { get; set; }
        [Required(ErrorMessage = "Date of Birth is required.")]
        public DateTime? EmployeeDOB { get; set; }
        public string EmpDateOfBirthArabic { get; set; }
        public short? EmployeeNationality { get; set; }
        [Required(ErrorMessage = "Iqama Number Or National ID Number is required.")]
        public int? EmployeeIqama { get; set; }
        public DateTime? EmployeeIqamaIssueDt { get; set; }
        public DateTime? EmployeeIqamaIssueDtAr { get; set; }
        [Required(ErrorMessage = "Iqama Expiry Date is required.")]
        public DateTime? EmployeeIqamaExpiryDt { get; set; }
        public DateTime? EmployeeIqamaExpiryDtAr { get; set; }
        [Required(ErrorMessage = "Passport ID is required.")]
        public string EmployeePassportNum { get; set; }
        [Required(ErrorMessage = "Passport Expiry Date is required.")]
        public DateTime? EmployeePassportExpiryDt { get; set; }
        public DateTime? EmployeePassportExpiryDtAr { get; set; }
        public string EmployeeDetailsE { get; set; }
        public string EmployeeFullName { get; set; }
        public DateTime? RecCreatedDt { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecLastUpdatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public string Email { get; set; }
        public string EmployeeDetailsA { get; set; }

        public IEnumerable<Allowance> Allowances { get; set; }
        public JobTitle JobTitle { get; set; }
    }
}