using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels.Reports;
using EPMS.Models.ResponseModels.ReportsResponseModels;

namespace EPMS.Interfaces.Repository
{
    public interface IReportRepository : IBaseRepository<Report, long>
    {
        TaskReportsListRequestResponse GetTasksReports(TaskReportSearchRequest taskReportSearchRequest);
        ReportsListRequestResponse GetWarehousesReports(WarehouseReportSearchRequest searchRequest);
        ReportsListRequestResponse GetInventoryItemsReports(WarehouseReportSearchRequest searchRequest);
        ReportsListRequestResponse GetRFQOrdersReports(WarehouseReportSearchRequest searchRequest);
        ReportsListRequestResponse GetVendorsReports(VendorReportSearchRequest searchRequest);
        CustomerReportListResponse GetQuotationInvoiceReports(CustomerServiceReportsSearchRequest request);
        CustomerReportListResponse GetAllCustoemrReport(CustomerServiceReportsSearchRequest request);
        ReportsListRequestResponse GetProjectsReports(ProjectReportSearchRequest projectReportSearchRequest);
        
    }
}
