using System.Collections.Generic;
using EPMS.Models.RequestModels;

namespace EPMS.WebModels.ViewModels.JobApplicant
{
    /// <summary>
    /// Job Applicants List View Model
    /// </summary>
    public class JobApplicantListViewModel
    {
        /// <summary>
        /// List of Job Applicants
        /// </summary>
        public IEnumerable<WebsiteModels.ApplicantModel> aaData { get; set; }
        public JobApplicantSearchRequest SearchRequest { get; set; }
        
        /// <summary>
        /// Total Records in DB
        /// </summary>
        public int iTotalRecords;

        /// <summary>
        /// Total Records Filtered
        /// </summary>
        public int iTotalDisplayRecords;

        public string sEcho;
    }
}