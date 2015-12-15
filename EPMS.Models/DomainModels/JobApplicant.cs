using System;
using System.Collections.Generic;

namespace EPMS.Models.DomainModels
{
    public class JobApplicant
    {
        public long ApplicantId { get; set; }
        public long JobOfferedId { get; set; }
        public string ApplicantFirstNameE { get; set; }
        public string ApplicantMiddleNameE { get; set; }
        public string ApplicantFirstNameA { get; set; }
        public string ApplicantMiddleNameA { get; set; }
        public string ApplicantFamilyNameE { get; set; }
        public string ApplicantFamilyNameA { get; set; }
        public long? DepartmentId { get; set; }
        public byte? ApplicantSex { get; set; }
        public string ApplicantNationality { get; set; }
        public DateTime? DateOfBirth { get; set; }
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
        public DateTime? JobLeavingDate { get; set; }
        public bool AcceptAgreement { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string CvPath { get; set; }

        public virtual ICollection<ApplicantExperience> ApplicantExperiences { get; set; }
        public virtual ICollection<ApplicantQualification> ApplicantQualifications { get; set; }
        public virtual Department Department { get; set; }
        public virtual JobOffered JobOffered { get; set; }
    }
}
