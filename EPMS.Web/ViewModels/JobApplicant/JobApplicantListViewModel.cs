using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPMS.Models.RequestModels;

namespace EPMS.Web.ViewModels.JobApplicant
{
    /// <summary>
    /// Job Applicants List View Model
    /// </summary>
    public class JobApplicantListViewModel
    {
        /// <summary>
        /// List of Job Applicants
        /// </summary>
        public IEnumerable<Models.ApplicantModel> aaData { get; set; }
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