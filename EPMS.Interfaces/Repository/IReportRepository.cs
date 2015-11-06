using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.RequestModels.Reports;
using EPMS.Models.ResponseModels;
using EPMS.Models.ResponseModels.ReportsResponseModels;

namespace EPMS.Interfaces.Repository
{
    public interface IReportRepository : IBaseRepository<Report, long>
    {
        ProjectReportsListRequestResponse GetProjectsReports(ProjectReportSearchRequest projectReportSearchRequest);
        
    }
}
