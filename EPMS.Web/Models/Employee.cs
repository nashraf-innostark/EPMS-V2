using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using DataAnnotationsExtensions;
using EPMS.Models.DomainModels;

namespace EPMS.Web.Models
{
    public class Employee
    {
        public long EmployeeId { get; set; }
        [Required(ErrorMessage = "Employee Name is required.")]
        [Display(Name = "Employee Name")]
        public string EmployeeNameE { get; set; }
        [Required(ErrorMessage = "Employee Name is required.")]
        [Display(Name = "Employee Name")]
        public string EmployeeNameA { get; set; }
        public string EmployeeImagePath { get; set; }
        [Required(ErrorMessage = "Job Title is required.")]
        public long? JobTitleId { get; set; }
        public string EmployeeJobId { get; set; }
        [Integer(ErrorMessage = "Mobile Number needs to be number")]
        [Required(ErrorMessage = "Mobile Number is required.")]
        public string EmployeeMobileNum { get; set; }
        [Integer(ErrorMessage = "LandLine Number needs to be number")]
        [Required(ErrorMessage = "LandLine Number is required.")]
        public string EmployeeLandlineNum { get; set; }
        [Required(ErrorMessage = "Marital Status is required.")]
        public byte? MaritalStatus { get; set; }
        [Required(ErrorMessage = "Date of Birth is required.")]
        public DateTime? EmployeeDOB { get; set; }
        public string EmpDateOfBirthArabic { get; set; }

        [Required(ErrorMessage = "Natiionality is required.")]
        public short? EmployeeNationality { get; set; }
        [Required(ErrorMessage = "Iqama ID is required.")]
        public int? EmployeeIqama { get; set; }
        [Required(ErrorMessage = "Iqama Issue Date is required.")]
        public DateTime? EmployeeIqamaIssueDt { get; set; }
        public DateTime? EmployeeIqamaIssueDtAr { get; set; }
        [Required(ErrorMessage = "Iqama Expiry Date is required.")]
        public DateTime? EmployeeIqamaExpiryDt { get; set; }
        public DateTime? EmployeeIqamaExpiryDtAr { get; set; }
        [Required(ErrorMessage = "Passport Number is required.")]
        public string EmployeePassportNum { get; set; }
        [Required(ErrorMessage = "Passport Expiry Date is required.")]
        public DateTime? EmployeePassportExpiryDt { get; set; }
        public DateTime? EmployeePassportExpiryDtAr { get; set; }
        [Display(Name = "Extra Information")]
        public string EmployeeDetailsE { get; set; }
        [Display(Name = "Full Name")]
        public string EmployeeFullName { get; set; }
        public DateTime? RecCreatedDt { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecLastUpdatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public string Email { get; set; }
        [Display(Name = "Extra Information")]
        public string EmployeeDetailsA { get; set; }


        //[Display(Name = "Image")]
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public HttpPostedFileBase UploadImage { get; set; }

        public IEnumerable<Allowance> Allowances { get; set; }
        public JobTitle JobTitle { get; set; }
        public IEnumerable<Request> EmployeeRequests { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<AspNetUser> AspNetUsers { get; set; }
    }
}