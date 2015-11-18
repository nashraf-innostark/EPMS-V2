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
        private readonly ICustomerRepository customerRepository;
        private readonly IQuotationRepository quotationRepository;

        #endregion

        #region Constructor
        public ReportService(IReportRepository reportRepository, IProjectRepository projectRepository, ICustomerRepository customerRepository, IQuotationRepository quotationRepository)
        {
            this.reportRepository = reportRepository;
            this.projectRepository = projectRepository;
            this.customerRepository = customerRepository;
            this.quotationRepository = quotationRepository;
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
                request.ReportId = projectNewReport.ReportId;
            }
            else
            {
                var report = reportRepository.Find(request.ReportId);
                request.ProjectId = (long)report.ProjectId;
            }
            var response = projectRepository.GetProjectReportDetails(request).ToList();
            return new ProjectReportDetailsResponse
            {
                ReportId = request.ReportId,
              Projects  = response,
              ProjectTasks = response.FirstOrDefault().ProjectTasks
            };
        }

        public CustomerReportResponse SaveAndGetCustonerList(CustomerReportDetailRequest request)
        {
            if (request.IsCreate)
            {
                var customerReport = new Report
                {
                    ReportCategoryId = (int) ReportCategory.AllCustomer,
                    ReportCreatedBy = request.RequesterId,
                    ReportCreatedDate = DateTime.Now,
                    ReportFromDate = DateTime.Now,
                    ReportToDate = DateTime.Now
                };
                reportRepository.Add(customerReport);
                reportRepository.SaveChanges();
            }
            else
            {
                var report = reportRepository.Find(request.ReportId);
                request.StartDate = report.ReportFromDate;
                request.EndDate = report.ReportToDate;

            }
            var response = customerRepository.GetCustomerReportList(request);
            return new CustomerReportResponse
            {
                Customers = response
            };
        }

        public QuotationInvoiceReportResponse SaveAndGetQuotationInvoiceReport(QuotationInvoiceDetailRequest request)
        {
            if (request.IsCreate)
            {
                var quotationInvoiceReport = new Report
                {
                    ReportCategoryId = (int)ReportCategory.AllQuotationInvoice,
                    ReportCreatedBy = request.RequesterId,
                    ReportCreatedDate = DateTime.Now,
                    ReportFromDate = DateTime.Now,
                    ReportToDate = DateTime.Now
                };
                reportRepository.Add(quotationInvoiceReport);
                reportRepository.SaveChanges();
            }
            else
            {
                var report = reportRepository.Find(request.ReportId);
                request.StartDate = report.ReportFromDate;
                request.EndDate = report.ReportToDate;
            }
            var quotationCount = quotationRepository.GetAll().Count();

            return new QuotationInvoiceReportResponse
            {
                InvoicesCount = 0,
                QuotationsCount = quotationCount,
            };
        }

        #endregion
    }
}
