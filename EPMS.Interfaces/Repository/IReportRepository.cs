﻿using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels.Reports;
using EPMS.Models.ResponseModels.ReportsResponseModels;

namespace EPMS.Interfaces.Repository
{
    public interface IReportRepository : IBaseRepository<Report, long>
    {
        TaskReportsListRequestResponse GetTasksReports(TaskReportSearchRequest taskReportSearchRequest);
        ReportsListRequestResponse GetWarehousesReports(WarehouseReportSearchRequest searchRequest);
        ReportsListRequestResponse GetVendorsReports(VendorReportSearchRequest searchRequest);

        #region Projects Report
        ReportsListRequestResponse GetProjectsReports(ProjectReportSearchRequest projectReportSearchRequest);
        #endregion
    }
}
