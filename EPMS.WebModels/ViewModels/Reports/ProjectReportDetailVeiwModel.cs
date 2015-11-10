using System.Collections.Generic;
using EPMS.WebModels.WebsiteModels;

namespace EPMS.WebModels.ViewModels.Reports
{
    public class ProjectReportDetailVeiwModel
    {
        public ProjectReportDetailVeiwModel()
        {
            Projects=new List<WebsiteModels.Project>();
            ProjectTasks=new List<ProjectTask>();
        }
        public long ReportId { get; set; }
        public IList<WebsiteModels.Project> Projects { get; set; }
        public IList<ProjectTask> ProjectTasks { get; set; }
    }
}
