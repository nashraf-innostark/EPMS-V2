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
        public JobOffered JobOffered { get; set; }
        public IEnumerable<JobTitle> JobTitles { get; set; }
        public int TotalCount { get; set; }
        public int TotalRecords { get; set; }
        public int TotalDisplayRecords { get; set; }
    }
}
