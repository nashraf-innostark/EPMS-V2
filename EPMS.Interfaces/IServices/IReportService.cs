﻿using System.Collections.Generic;
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
        ReportsListRequestResponse GetInventoryItemsReports(WarehouseReportSearchRequest searchRequest);
        ReportsListRequestResponse GetVendorsReports(VendorReportSearchRequest searchRequest);
        ProjectReportDetailsResponse SaveAndGetProjectReportDetails(ProjectReportCreateOrDetailsRequest request);
        CustomerReportResponse SaveAndGetCustomerList(CustomerReportDetailRequest request);
        QuotationInvoiceReportResponse SaveAndGetQuotationInvoiceReport(QuotationInvoiceDetailRequest request);
        WarehouseReportDetailsResponse SaveAndGetWarehouseReportDetails(WarehouseReportCreateOrDetailsRequest request);
        IEnumerable<ReportProject> SaveAndGetAllProjectsReport(ProjectReportCreateOrDetailsRequest request);
        long SaveInventoryItemsReport(InventoryItemReportCreateOrDetailsRequest request);
        IEnumerable<ReportInventoryItem> GetInventoryItemsReport(long reportId);
        TaskReportsListRequestResponse GetTasksReports(TaskReportSearchRequest taskReportSearchRequest);
        TaskReportDetailsResponse SaveAndGetTaskReportDetails(TaskReportCreateOrDetailsRequest request);
        IEnumerable<ProjectTask> GetAllProjectTasks(TaskReportCreateOrDetailsRequest request);
        CustomerReportListResponse GetQuotationInvoiceReports(CustomerServiceReportsSearchRequest request);
        CustomerReportListResponse GetAllCustoemrReport(CustomerServiceReportsSearchRequest request);
        long SaveQOReport(QOReportCreateOrDetailsRequest request);
        IEnumerable<ReportQuotationOrder> GetQOReport(long reportId);
    }
}
