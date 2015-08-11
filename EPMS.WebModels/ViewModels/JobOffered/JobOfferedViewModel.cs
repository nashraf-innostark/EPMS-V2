using System.Collections.Generic;

namespace EPMS.WebModels.ViewModels.JobOffered
{
    public class JobOfferedViewModel
    {
        public JobOfferedViewModel()
        {
            JobOffered = new WebsiteModels.JobOffered();
            JobTitle = new WebsiteModels.JobTitle();
            Department = new WebsiteModels.Department();
        }
        public WebsiteModels.JobOffered JobOffered { get; set; }
        public WebsiteModels.JobTitle JobTitle { get; set; }
        public WebsiteModels.Department Department { get; set; }
        public IEnumerable<WebsiteModels.JobOffered> JobOfferedList { get; set; }
        public IEnumerable<WebsiteModels.JobTitle> JobTitleList { get; set; }
        public long SelectedJobTitle { get; set; }
        public string DepartmentNameE { get; set; }
        public string DepartmentNameA { get; set; }

        public string JobTitleDescE { get; set; }
        public string JobTitleDescA { get; set; }
        public double? BasicSalary { get; set; }

    }
}