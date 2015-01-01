using System.Collections.Generic;
using EPMS.Web.Models;

namespace EPMS.Web.ViewModels.Recruitment
{
    public class RecruitmentViewModel
    {
        public RecruitmentViewModel()
        {
            JobOffered=new JobOffered();
        }
        public JobOffered JobOffered { get; set; }
        public IEnumerable<JobOffered> JobsOffered { get; set; }
    }
}