using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    /// <summary>
    /// Job Applicant Search Response
    /// </summary>
    public class JobApplicantResponse
    {
        public JobApplicantResponse()
        {
            JobApplicants = new List<JobApplicant>();
        }
        /// <summary>
        /// List of Job Applicants
        /// </summary>
        public IEnumerable<JobApplicant> JobApplicants { get; set; }

        /// <summary>
        /// Total Count
        /// </summary>
        public int TotalCount { get; set; }
    }
}
