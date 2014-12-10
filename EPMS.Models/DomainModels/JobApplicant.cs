using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPMS.Models.DomainModels
{
    public class JobApplicant
    {
        public long ApplicantId { get; set; }
        public string ApplicantName { get; set; }
        public string ApplicantMobile { get; set; }
        public string ApplicantEmail { get; set; }
        public long JobOfferedId { get; set; }
        public Nullable<int> ApplicantAge { get; set; }
        public string ApplicantCvPath { get; set; }
        public string RecCreatedBy { get; set; }
        public Nullable<System.DateTime> RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public Nullable<System.DateTime> RecLastUpdatedDt { get; set; }

        public virtual JobOffered JobOffered { get; set; }
    }
}
