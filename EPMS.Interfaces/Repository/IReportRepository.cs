using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.Repository
{
    public interface IReportRepository : IBaseRepository<Report, long>
    {
        ProjectReportRequestResponse GetProjectsReports(ProjectReportSearchRequest projectReportSearchRequest);
    }
}
