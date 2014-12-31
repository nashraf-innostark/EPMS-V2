using System.Collections.Generic;

namespace EPMS.Web.ViewModels.JobOffered
{
    public class JobOfferedViewModel
    {
        public JobOfferedViewModel()
        {
            JobTitle = new Models.JobTitle();
        }
        public Models.JobOffered JobOffered { get; set; }
        public Models.JobTitle JobTitle { get; set; }
        public IEnumerable<Models.JobOffered> JobOfferedList { get; set; }
        public IEnumerable<Models.JobTitle> JobTitleList { get; set; }
        public long SelectedJobTitle { get; set; }

    }
}