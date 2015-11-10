using System.Collections.Generic;

namespace EPMS.Models.ResponseModels.ReportsResponseModels
{
    public class TaskReportDetailsResponse
    {
        public IEnumerable<DomainModels.ProjectTask> ProjectTasks { get; set; }
        public IEnumerable<DomainModels.ProjectTask> SubTasks { get; set; }
    }
}
