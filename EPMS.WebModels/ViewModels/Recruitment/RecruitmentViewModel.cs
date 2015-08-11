using System.Collections.Generic;

namespace EPMS.WebModels.ViewModels.Recruitment
{
    public class RecruitmentViewModel
    {
        public RecruitmentViewModel()
        {
            JobOffered = new WebsiteModels.JobOffered();
        }
        public WebsiteModels.JobOffered JobOffered { get; set; }
        public IEnumerable<WebsiteModels.JobOffered> JobsOffered { get; set; }
    }
}