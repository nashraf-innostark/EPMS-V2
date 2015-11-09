using System;
using System.Linq;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.Common;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels.Reports;
using EPMS.Models.ResponseModels.ReportsResponseModels;

namespace EPMS.Implementation.Services
{
    public class ReportService : IReportService
    {

        #region Private
        private readonly IReportRepository reportRepository;
        private readonly IProjectRepository projectRepository;

        #endregion

        #region Constructor
        public ReportService(IReportRepository reportRepository, IProjectRepository projectRepository)
        {
            this.reportRepository = reportRepository;
            this.projectRepository = projectRepository;
        }

        #endregion

        #region Public
        public bool AddReport(Report report)
        {
            reportRepository.Add(report);
            reportRepository.SaveChanges();
            return true;
        }

        public ProjectReportsListRequestResponse GetProjectsReports(ProjectReportSearchRequest projectReportSearchRequest)
        {
            return reportRepository.GetProjectsReports(projectReportSearchRequest);
        }

        public ProjectReportDetailsResponse SaveAndGetProjectReportDetails(ProjectReportCreateOrDetailsRequest request)
        {
            if (request.IsCreate)
            {
                var projectNewReport = new Report
                {
                    ProjectId = request.ProjectId,
                    ReportCategoryId = (int) ReportCategory.Project,
                    ReportCreatedBy = request.RequesterId,
                    ReportCreatedDate = DateTime.Now,
                    ReportFromDate = DateTime.Now,
                    ReportToDate = DateTime.Now
                };
                reportRepository.Add(projectNewReport);
                reportRepository.SaveChanges();
            }
            else
            {
                var report = reportRepository.Find(request.ReportId);
                request.ProjectId = (long)report.ProjectId;
            }
            var response = projectRepository.GetProjectReportDetails(request).ToList();
            return new ProjectReportDetailsResponse
            {
              Projects  = response,
              ProjectTasks = response.FirstOrDefault().ProjectTasks
            };
        }
        
        #endregion
    }
}
