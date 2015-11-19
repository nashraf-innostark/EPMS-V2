using System.Collections.Generic;

namespace EPMS.WebModels.ViewModels.Reports
{
    public class TaskReportsCreateViewModel
    {
        public long ProjectId { get; set; }
        public long TaskId { get; set; }
        public long ReportId { get; set; }
        public IList<Models.DashboardModels.Project> Projects { get; set; }
        public IList<WebsiteModels.ProjectTaskDropDown> Tasks { get; set; }
    }
}
