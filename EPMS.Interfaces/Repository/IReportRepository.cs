using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels.Reports;
using EPMS.Models.ResponseModels.ReportsResponseModels;

namespace EPMS.Interfaces.Repository
{
    public interface IReportRepository : IBaseRepository<Report, long>
    {
        ReportsListRequestResponse GetProjectsReports(ProjectReportSearchRequest projectReportSearchRequest);
        TaskReportsListRequestResponse GetTasksReports(TaskReportSearchRequest taskReportSearchRequest);
        ReportsListRequestResponse GetWarehousesReports(WarehouseReportSearchRequest searchRequest);
        ReportsListRequestResponse GetVendorsReports(VendorReportSearchRequest searchRequest);
        CustomerReportListResponse GetQuotationInvoiceReports(CustomerServiceReportsSearchRequest request);
        CustomerReportListResponse GetAllCustoemrReport(CustomerServiceReportsSearchRequest request);
    }
}
