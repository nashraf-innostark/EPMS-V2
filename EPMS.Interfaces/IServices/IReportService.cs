using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels.Reports;
using EPMS.Models.ResponseModels.ReportsResponseModels;

namespace EPMS.Interfaces.IServices
{
    public interface IReportService
    {
        /// <summary>
        /// Add Report
        /// </summary>
        bool AddReport(Report report);
        ProjectReportsListRequestResponse GetProjectsReports(ProjectReportSearchRequest projectReportSearchRequest);
        ProjectReportDetailsResponse SaveAndGetProjectReportDetails(ProjectReportCreateOrDetailsRequest request);
        IEnumerable<Project> SaveAndGetAllProjectsReport(ProjectReportCreateOrDetailsRequest request);
        TaskReportsListRequestResponse GetTasksReports(TaskReportSearchRequest taskReportSearchRequest);
        TaskReportDetailsResponse SaveAndGetTaskReportDetails(TaskReportCreateOrDetailsRequest request);
    }
}
