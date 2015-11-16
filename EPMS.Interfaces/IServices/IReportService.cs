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
        ReportsListRequestResponse GetProjectsReports(ProjectReportSearchRequest projectReportSearchRequest);
        ReportsListRequestResponse GetWarehousesReports(WarehouseReportSearchRequest searchRequest);
        ProjectReportDetailsResponse SaveAndGetProjectReportDetails(ProjectReportCreateOrDetailsRequest request);
        WarehouseReportDetailsResponse SaveAndGetWarehouseReportDetails(WarehouseReportCreateOrDetailsRequest request);
        IEnumerable<Project> SaveAndGetAllProjectsReport(ProjectReportCreateOrDetailsRequest request);
        TaskReportsListRequestResponse GetTasksReports(TaskReportSearchRequest taskReportSearchRequest);
        TaskReportDetailsResponse SaveAndGetTaskReportDetails(TaskReportCreateOrDetailsRequest request);
    }
}
