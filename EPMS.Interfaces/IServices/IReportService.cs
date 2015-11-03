using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface IReportService
    {
        /// <summary>
        /// Add Report
        /// </summary>
        bool AddReport(Report report);
    }
}
