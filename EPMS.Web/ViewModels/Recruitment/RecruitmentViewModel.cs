using System.Collections.Generic;
using EPMS.Web.Models;

namespace EPMS.Web.ViewModels.Recruitment
{
    public class RecruitmentViewModel
    {
        public RecruitmentViewModel()
        {
            JobOffered = new Models.JobOffered();
        }
        public Models.JobOffered JobOffered { get; set; }
        public IEnumerable<Models.JobOffered> JobsOffered { get; set; }
    }
}