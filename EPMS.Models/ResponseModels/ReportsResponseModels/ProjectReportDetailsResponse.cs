using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels.ReportsResponseModels
{
    public class ProjectReportDetailsResponse
    {
        public long ReportId { get; set; }
        public IEnumerable<DomainModels.Project> Projects { get; set; }
        public IEnumerable<ProjectTask> ProjectTasks { get; set; }
    }
    public class WarehouseReportDetailsResponse
    {
        public long ReportId { get; set; }
        public IEnumerable<WarehouseReportDetails> Warehouses { get; set; }
    }
}
