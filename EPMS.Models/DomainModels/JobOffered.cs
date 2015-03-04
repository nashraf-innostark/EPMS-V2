using System;
using System.Collections.Generic;

namespace EPMS.Models.DomainModels
{
    public class JobOffered
    {
        public long JobOfferedId { get; set; }
        public long JobTitleId { get; set; }
        public string TitleE { get; set; }
        public string TitleA { get; set; }
        public string DescriptionE { get; set; }
        public string DescriptionA { get; set; }
        public bool ShowBasicSalary { get; set; }
        public bool IsOpen { get; set; }
        public int? NoOfPosts { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime? RecLastUpdatedDt { get; set; }

        public virtual ICollection<JobApplicant> JobApplicants { get; set; }
        public virtual Department Department { get; set; }
        public virtual JobTitle JobTitle { get; set; }
    }
}
