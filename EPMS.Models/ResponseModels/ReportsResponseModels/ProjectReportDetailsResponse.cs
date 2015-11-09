using System.Collections.Generic;

namespace EPMS.Models.ResponseModels.ReportsResponseModels
{
    public class ProjectReportDetailsResponse
    {
        public long ReportId { get; set; }
        public IEnumerable<DomainModels.Project> Projects { get; set; }
        public IEnumerable<DomainModels.ProjectTask> ProjectTasks { get; set; }
    }
}
