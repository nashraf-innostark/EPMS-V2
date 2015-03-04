using System.Collections.Generic;

namespace EPMS.Web.ViewModels.JobOffered
{
    public class JobOfferedViewModel
    {
        public JobOfferedViewModel()
        {
            JobOffered = new Models.JobOffered();
            JobTitle = new Models.JobTitle();
            Department = new Models.Department();
        }
        public Models.JobOffered JobOffered { get; set; }
        public Models.JobTitle JobTitle { get; set; }
        public Models.Department Department { get; set; }
        public IEnumerable<Models.JobOffered> JobOfferedList { get; set; }
        public IEnumerable<Models.JobTitle> JobTitleList { get; set; }
        public long SelectedJobTitle { get; set; }
        public string DepartmentNameE { get; set; }
        public string DepartmentNameA { get; set; }

        public string JobTitleDescE { get; set; }
        public string JobTitleDescA { get; set; }
        public double? BasicSalary { get; set; }

    }
}