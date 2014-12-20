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
        [Required(ErrorMessage = "First Name is required.")]
        public string EmployeeFirstName { get; set; }
        [Required(ErrorMessage = "Middle Name is required.")]
        public string EmployeeMiddleName { get; set; }
        [Required(ErrorMessage = "Last Name is required.")]
        public string EmployeeLastName { get; set; }
        public string EmployeeImagePath { get; set; }
        public long? JobTitleId { get; set; }
        public string EmployeeJobId { get; set; }
        [StringLength(20)]
        [Integer(ErrorMessage = "Mobile Number needs to be number")]
        [Required(ErrorMessage = "Mobile Number is required.")]
        public string EmployeeMobileNum { get; set; }
        [StringLength(20)]
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
        [Required(ErrorMessage = "Iqama Expiry Date is required.")]
        public DateTime? EmployeeIqamaExpiryDt { get; set; }
        [Required(ErrorMessage = "Passport ID is required.")]
        public string EmployeePassportNum { get; set; }
        [Required(ErrorMessage = "Passport Expiry Date is required.")]
        public DateTime? EmployeePassportExpiryDt { get; set; }
        public string EmployeeDetails { get; set; }
        public string EmployeeFullName { get; set; }
        public string JobTitleName { get; set; }
        public string DepartmentName { get; set; }
        public DateTime? RecCreatedDt { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecLastUpdatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public string Email { get; set; }


        //[Display(Name = "Image")]
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public HttpPostedFileBase UploadImage { get; set; }

        public virtual ICollection<Allowance> Allowances { get; set; }
        public virtual JobTitle JobTitle { get; set; }
        public virtual ICollection<EmployeeRequest> EmployeeRequests { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
    }
}