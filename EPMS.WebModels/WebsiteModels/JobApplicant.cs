using System;
using System.ComponentModel.DataAnnotations;

namespace EPMS.WebModels.WebsiteModels
{
    public class JobApplicant
    {
        public long ApplicantId { get; set; }
        public long JobOfferedId { get; set; }
        [Required(ErrorMessage = "Applicant First Name is required")]
        public string ApplicantFirstNameE { get; set; }
        [Required(ErrorMessage = "Applicant Middle Name is required")]
        public string ApplicantMiddleNameE { get; set; }
        [Required(ErrorMessage = "Applicant First Name Arabic is required")]
        public string ApplicantFirstNameA { get; set; }
        [Required(ErrorMessage = "Applicant Middle Name Arabic is required")]
        public string ApplicantMiddleNameA { get; set; }
        public string ApplicantFamilyNameE { get; set; }
        public string ApplicantFamilyNameA { get; set; }
        public long? DepartmentId { get; set; }
        public byte? ApplicantSex { get; set; }
        public string ApplicantNationality { get; set; }
        public string DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }
        public string PassportNumber { get; set; }
        public int? NoOfFamilyMembers { get; set; }
        public string LanguagesKnown { get; set; }
        public bool EmployedNow { get; set; }
        public bool GetMonthlyPayment { get; set; }
        public bool GovernmentEmployeeEver { get; set; }
        public string GovernmentAreaWorked { get; set; }
        public bool GovernmentJobOfficial { get; set; }
        public string ReasonOfLeaving { get; set; }
        public string JobLeavingDate { get; set; }
        public bool AcceptAgreement { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string DepartmentNameE { get; set; }
        public string DepartmentNameA { get; set; }
        public string JobDescriptionE { get; set; }
        public string JobDescriptionA { get; set; }
        public string CvPath { get; set; }
    }
}