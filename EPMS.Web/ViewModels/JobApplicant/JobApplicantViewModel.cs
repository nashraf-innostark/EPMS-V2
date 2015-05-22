using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPMS.Web.Models;

namespace EPMS.Web.ViewModels.JobApplicant
{
    public class JobApplicantViewModel
    {
        public JobApplicantViewModel()
        {
            JobOffered = new Models.JobOffered();
            JobTitle = new Models.JobTitle();
            JobApplicant = new Models.JobApplicant();
            Qualifications = new List<ApplicantQualification>();
            Experiences = new List<ApplicantExperience>();
        }
        public Models.JobOffered JobOffered { get; set; }
        public Models.JobTitle JobTitle { get; set; }
        public Models.JobApplicant JobApplicant { get; set; }
        public IEnumerable<Models.JobApplicant> JobApplicantList { get; set; }
        public IEnumerable<Models.JobOffered> JobOfferedList { get; set; }
        public IEnumerable<Models.JobTitle> JobTitleList { get; set; }
        public IEnumerable<Models.Department> Departments { get; set; }
        public IList<ApplicantQualification> Qualifications { get; set; }
        public IList<ApplicantExperience> Experiences { get; set; }
    }
}