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
            GraphItems = new List<GraphItem>();
        }
        public long ReportId { get; set; }
        
        public IList<WebsiteModels.Project> Projects { get; set; }
        public IList<ProjectTask> ProjectTasks { get; set; }

        //Grpah related Data
        public long GrpahStartTimeStamp { get; set; }
        public long GrpahEndTimeStamp { get; set; }
        public List<GraphItem> GraphItems { get; set; } 
    }
    public class GraphItem
    {
        public GraphItem()
        {
            ItemValue = new List<GraphLabel>();
        }
        public string ItemLabel { get; set; }
        public List<GraphLabel> ItemValue { get; set; }
    }
    public class GraphLabel
    {
        public GraphLabel()
        {
            data=new List<GraphLabelData>();
        }
        public string label { get; set; }
        public List<GraphLabelData> data { get; set; } 
    }
    public class GraphLabelData
    {
        public GraphLabelData()
        {
            dataValue=new List<GraphLabelDataValues>();
        }
        public List<GraphLabelDataValues> dataValue { get; set; }
    }
    public class GraphLabelDataValues
    {
        public long TimeStamp { get; set; }
        public decimal Value { get; set; }
    }
}
