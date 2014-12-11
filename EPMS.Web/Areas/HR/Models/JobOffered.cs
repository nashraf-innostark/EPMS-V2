using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPMS.Web.Areas.HR.Models
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
        public virtual JobTitle JobTitle { get; set; }
    }
}