using System;
using System.ComponentModel.DataAnnotations;

namespace EPMS.Web.Models
{
    public class JobApplicant
    {
        public long ApplicantId { get; set; }
        public long JobOfferedId { get; set; }
        [Required(ErrorMessageResourceType = typeof (Resources.HR.JobApplicant), ErrorMessageResourceName = "ApplicantNameValidation")]
        public string ApplicantName { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.HR.JobApplicant), ErrorMessageResourceName = "ApplicantMobileValidation")]
        public string ApplicantMobile { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.HR.JobApplicant), ErrorMessageResourceName = "ApplicantEmailValidation")]
        public string ApplicantEmail { get; set; }
        public int? ApplicantAge { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.HR.JobApplicant), ErrorMessageResourceName = "ApplicantCvValidation")]
        public string ApplicantCvPath { get; set; }
        public bool MaritalStatus { get; set; }
        public string Nationality { get; set; }
        public string IqamaOrNationalIdNo { get; set; }
        public bool DrivingLicense { get; set; }
        public string DrivingLicenseStatus { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime? RecLastUpdatedDt { get; set; }
        public string DepartmentNameE { get; set; }
        public string DepartmentNameA { get; set; }
        public string JobDescriptionE { get; set; }
        public string JobDescriptionA { get; set; }
    }
}