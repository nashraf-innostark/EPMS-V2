using System.Collections.Generic;

namespace EPMS.Models.ResponseModels.ReportsResponseModels
{
    public class TaskReportDetailsResponse
    {
        public long ReportId { get; set; }
        public IEnumerable<DomainModels.ReportProjectTask> ProjectTasks { get; set; }
        public IEnumerable<DomainModels.ReportProjectTask> SubTasks { get; set; }
    }
}
