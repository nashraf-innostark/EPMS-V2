using System;

namespace EPMS.Models.DomainModels
{
    public class ApplicantExperience
    {
        public long ExperienceId { get; set; }
        public long? ApplicantId { get; set; }
        public string JobTitle { get; set; }
        public string Position { get; set; }
        public decimal? Salary { get; set; }
        public string CompanyName { get; set; }
        public string TypeOfWork { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string ReasonToLeave { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }

        public virtual JobApplicant JobApplicant { get; set; }
    }
}
