using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels.ReportsResponseModels
{
    public class ProjectReportDetailsResponse
    {
        public long ReportId { get; set; }
        public IEnumerable<ReportProject> Projects { get; set; }
        public IEnumerable<ReportProjectTask> ProjectTasks { get; set; }
    }
    public class WarehouseReportDetailsResponse
    {
        public long ReportId { get; set; }
        public IEnumerable<WarehouseReportDetails> Warehouses { get; set; }
    }
}
