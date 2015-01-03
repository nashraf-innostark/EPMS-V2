using System;

namespace EPMS.Web.Models
{
    public class JobApplicant
    {
        public long ApplicantId { get; set; }
        public long JobOfferedId { get; set; }
        public string ApplicantName { get; set; }
        public string ApplicantMobile { get; set; }
        public string ApplicantEmail { get; set; }
        public int? ApplicantAge { get; set; }
        public string ApplicantCvPath { get; set; }
        public bool MaritalStatus { get; set; }
        public string Nationality { get; set; }
        public string IqamaOrNationalIdNo { get; set; }
        public bool DrivingLicense { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime? RecLastUpdatedDt { get; set; }

        public JobOffered JobOffered { get; set; }
    }
}