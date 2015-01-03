using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPMS.Web.ViewModels.JobApplicant
{
    public class JobApplicantViewModel
    {
        public JobApplicantViewModel()
        {
            JobOffered = new Models.JobOffered();
            JobTitle = new Models.JobTitle();
            JobApplicant = new Models.JobApplicant();
        }
        public Models.JobOffered JobOffered { get; set; }
        public Models.JobTitle JobTitle { get; set; }
        public Models.JobApplicant JobApplicant { get; set; }
        public IEnumerable<Models.JobApplicant> JobApplicantList { get; set; }
        public IEnumerable<Models.JobOffered> JobOfferedList { get; set; }
        public IEnumerable<Models.JobTitle> JobTitleList { get; set; }
    }
}