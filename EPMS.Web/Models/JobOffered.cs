using System;
using System.Collections.Generic;

namespace EPMS.Web.Models
{
    public class JobOffered
    {
        public long JobOfferedId { get; set; }
        public long JobTitleId { get; set; }
        public string JobDescription { get; set; }
        public bool ShowBasicSalary { get; set; }
        public bool IsOpen { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime? RecLastUpdatedDt { get; set; }

        public virtual ICollection<JobApplicant> JobApplicants { get; set; }
        public JobTitle JobTitle { get; set; }
    }
}