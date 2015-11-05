using System.Collections.Generic;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Implementation.Services
{
    public class ReportService : IReportService
    {

        #region Private
        private readonly IReportRepository reportRepository;
        #endregion

        #region Constructor
        public ReportService(IReportRepository reportRepository)
        {
            this.reportRepository = reportRepository;
        }
        #endregion

        #region Public
        public bool AddReport(Report report)
        {
            reportRepository.Add(report);
            reportRepository.SaveChanges();
            return true;
        }

        public ProjectReportRequestResponse GetProjectsReports(ProjectReportSearchRequest projectReportSearchRequest)
        {
            return reportRepository.GetProjectsReports(projectReportSearchRequest);
        }

        #endregion
    }
}
