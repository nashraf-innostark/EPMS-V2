using System.Collections.Generic;
using EPMS.WebModels.WebsiteModels;

namespace EPMS.WebModels.ViewModels.Reports
{
    public class TaskReportDetailVeiwModel
    {
        public TaskReportDetailVeiwModel()
        {
            ProjectTasks = new List<ProjectTask>();
            SubTasks = new List<ProjectTask>();
        }
        public IList<ProjectTask> ProjectTasks { get; set; }
        public IList<ProjectTask> SubTasks { get; set; }
    }
}
