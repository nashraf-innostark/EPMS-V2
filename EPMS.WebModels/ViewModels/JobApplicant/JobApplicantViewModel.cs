using System.Collections.Generic;

namespace EPMS.WebModels.ViewModels.JobApplicant
{
    public class JobApplicantViewModel
    {
        public JobApplicantViewModel()
        {
            JobOffered = new WebsiteModels.JobOffered();
            JobTitle = new WebsiteModels.JobTitle();
            JobApplicant = new WebsiteModels.JobApplicant();
            Qualifications = new List<WebsiteModels.ApplicantQualification>();
            Experiences = new List<WebsiteModels.ApplicantExperience>();
        }
        public WebsiteModels.JobOffered JobOffered { get; set; }
        public WebsiteModels.JobTitle JobTitle { get; set; }
        public WebsiteModels.JobApplicant JobApplicant { get; set; }
        public IEnumerable<WebsiteModels.JobApplicant> JobApplicantList { get; set; }
        public IEnumerable<WebsiteModels.JobOffered> JobOfferedList { get; set; }
        public IEnumerable<WebsiteModels.JobTitle> JobTitleList { get; set; }
        public IEnumerable<WebsiteModels.Department> Departments { get; set; }
        public IList<WebsiteModels.ApplicantQualification> Qualifications { get; set; }
        public IList<WebsiteModels.ApplicantExperience> Experiences { get; set; }
    }
}