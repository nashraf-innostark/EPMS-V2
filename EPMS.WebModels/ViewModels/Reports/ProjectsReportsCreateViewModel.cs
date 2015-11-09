using System.Collections.Generic;

namespace EPMS.WebModels.ViewModels.Reports
{
    public class ProjectsReportsCreateViewModel
    {
        public ProjectsReportsCreateViewModel()
        {
            Projects = new List<Models.DashboardModels.Project>();
        }

        public long ProjectId { get; set; }
        public long ReportId { get; set; }
        public IList<Models.DashboardModels.Project> Projects { get; set; }
    }
}
