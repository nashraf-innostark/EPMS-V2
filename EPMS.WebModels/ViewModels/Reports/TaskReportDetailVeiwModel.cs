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
            GraphItems = new List<GraphItem>();
        }
        public long ReportId { get; set; }
        public string ImageSrc { get; set; }
        public IList<ProjectTask> ProjectTasks { get; set; }
        public IList<ProjectTask> SubTasks { get; set; }

        //Grpah related Data
        public long GrpahStartTimeStamp { get; set; }
        public long GrpahEndTimeStamp { get; set; }
        public List<GraphItem> GraphItems { get; set; } 
    }
}
