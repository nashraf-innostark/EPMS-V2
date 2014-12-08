using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DataAnnotationsExtensions;

namespace EPMS.Web.Models
{
    public class Employee
    {
        public long? EmployeeId { get; set; }
        [Required(ErrorMessage = "First Name is required.")]
        public string EmpFirstNameE { get; set; }
        public string EmpFirstNameA { get; set; }
        [Required(ErrorMessage = "Middle Name is required.")]
        public string EmpMiddleNameE { get; set; }
        public string EmpMiddleNameA { get; set; }
        [Required(ErrorMessage = "Last Name is required.")]
        public string EmpLastNameE { get; set; }
        public string EmpLastNameA { get; set; }
        public long JobId { get; set; }
        public string EmpImage { get; set; }
        [StringLength(20)]
        [Integer(ErrorMessage = "Mobile Number needs to be number")]
        public string EmpMobileNumber { get; set; }
        [StringLength(20)]
        [Integer(ErrorMessage = "LandLine Number needs to be number")]
        public string EmpLandlineNumber { get; set; }
        [Required(ErrorMessage = "Marital Status is required.")]
        public string EmpMaritalStatus { get; set; }
        [Required(ErrorMessage = "Date of Birth is required.")]
        public string EmpDateOfBirth { get; set; }
        public DateTime? EmpDateOfBirthArabic { get; set; }

        [Required(ErrorMessage = "Natiionality is required.")]
        public string Nationality { get; set; }
        [Required(ErrorMessage = "Iqama ID is required.")]
        public long? EmpIqama { get; set; }
        [Required(ErrorMessage = "Iqama Issue Date is required.")]
        public DateTime IqamaIssueDate { get; set; }
        [Required(ErrorMessage = "Iqama Expiry Date is required.")]
        public DateTime IqamaExpiryDate { get; set; }
        [Required(ErrorMessage = "Passport ID is required.")]
        public long PassportId { get; set; }
        [Required(ErrorMessage = "Passport Expiry Date is required.")]
        public DateTime PassportExpiryDate { get; set; }
        public string ExtraInfo { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        //[Display(Name = "Image")]
        public string ImageName { get; set; }
        public string ImagePath { get; set; }

        public HttpPostedFileBase UploadImage { get; set; }

        public virtual string Department { get; set; }

        public virtual string JobTitle { get; set; }
        public string EmpFullName { get; set; }

        //public virtual JobTitle JobTitle { get; set; }

    }
}