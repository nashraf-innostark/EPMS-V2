using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.IServices
{
    public interface IReportService
    {
        /// <summary>
        /// Add Report
        /// </summary>
        bool AddReport(Report report);
        ProjectReportRequestResponse GetProjectsReports(ProjectReportSearchRequest projectReportSearchRequest);
    }
}
