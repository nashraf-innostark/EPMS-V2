using System;
using System.Collections.Generic;
using System.Linq;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.Common;
using EPMS.Models.DomainModels;
using EPMS.Models.ModelMapers;
using EPMS.Models.RequestModels.Reports;
using EPMS.Models.ResponseModels.ReportsResponseModels;

namespace EPMS.Implementation.Services
{
    public class ReportService : IReportService
    {
        #region Private

        private readonly IInventoryItemRepository inventoryItemRepository;
        private readonly IReportRepository reportRepository;
        private readonly IProjectRepository projectRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly IQuotationRepository quotationRepository;
        private readonly IProjectTaskRepository taskRepository;
        private readonly IWarehouseRepository warehouseRepository;

        #endregion
        #region Constructor
        public ReportService(IInventoryItemRepository inventoryItemRepository,IReportRepository reportRepository, IProjectRepository projectRepository, ICustomerRepository customerRepository, IQuotationRepository quotationRepository, IProjectTaskRepository taskRepository, IWarehouseRepository warehouseRepository)
        {
            this.inventoryItemRepository = inventoryItemRepository;
            this.reportRepository = reportRepository;
            this.projectRepository = projectRepository;
            this.customerRepository = customerRepository;
            this.quotationRepository = quotationRepository;
            this.taskRepository = taskRepository;
            this.warehouseRepository = warehouseRepository;
        }

        #endregion
        #region Reports List Views
        public ReportsListRequestResponse GetProjectsReports(ProjectReportSearchRequest projectReportSearchRequest)
        {
            return reportRepository.GetProjectsReports(projectReportSearchRequest);
        }
        public ReportsListRequestResponse GetWarehousesReports(WarehouseReportSearchRequest searchRequest)
        {
            return reportRepository.GetWarehousesReports(searchRequest);
        }

        public ReportsListRequestResponse GetInventoryItemsReports(WarehouseReportSearchRequest searchRequest)
        {
            return reportRepository.GetInventoryItemsReports(searchRequest);
        }

        public ReportsListRequestResponse GetVendorsReports(VendorReportSearchRequest searchRequest)
        {
            return reportRepository.GetVendorsReports(searchRequest);
        }

        public TaskReportsListRequestResponse GetTasksReports(TaskReportSearchRequest taskReportSearchRequest)
        {
            return reportRepository.GetTasksReports(taskReportSearchRequest);
        }

        public IEnumerable<ProjectTask> GetAllProjectTasks(TaskReportCreateOrDetailsRequest request)
        {
            var createdBefore = DateTime.Now;
            var report = reportRepository.Find(request.ReportId);
            if (report != null)
            {
                createdBefore = report.ReportCreatedDate;
            }
            var response = taskRepository.GetAllTasks(createdBefore).ToList();
            return response;
        }

        public CustomerReportResponse SaveAndGetCustomerList(CustomerReportDetailRequest request)
        {
            if (request.IsCreate)
            {
                var customerReport = new Report
                {
                    ReportCategoryId = (int)ReportCategory.AllCustomer,
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

        public CustomerReportListResponse GetCustomerServiceReports(CustomerServiceReportsSearchRequest request)
        {
            return reportRepository.GetCustomerServiceReports(request);
        }

        #endregion
        #region Create Reports and Details Views
        public bool AddReport(Report report)
        {
            reportRepository.Add(report);
            reportRepository.SaveChanges();
            return true;
        }
        public WarehouseReportDetailsResponse SaveAndGetWarehouseReportDetails(WarehouseReportCreateOrDetailsRequest request)
        {
            if (request.IsCreate)
            {
                var newReport = new Report
                {

                    ReportCreatedBy = request.RequesterId,
                    ReportCreatedDate = DateTime.Now,
                    ReportFromDate = DateTime.Now,
                    ReportToDate = DateTime.Now
                };
                if (request.WarehouseId > 0)
                {
                    newReport.WarehouseId = request.WarehouseId;
                    newReport.ReportCategoryId = (int)ReportCategory.Warehouse;
                }
                else
                {
                    newReport.ReportCategoryId = (int)ReportCategory.AllWarehouse;
                }

                reportRepository.Add(newReport);
                reportRepository.SaveChanges();
                request.ReportId = newReport.ReportId;
            }
            else
            {
                var report = reportRepository.Find(request.ReportId);
                if (report.WarehouseId != null)
                {
                    request.ReportCreatedDate = report.ReportCreatedDate;
                    request.WarehouseId = (long)report.WarehouseId;
                }

            }
            var warehouses = warehouseRepository.GetWarehouseReportDetails(request);

            return new WarehouseReportDetailsResponse
            {
                ReportId = request.ReportId,
                Warehouses = warehouses
            };
        }
        public ProjectReportDetailsResponse SaveAndGetProjectReportDetails(ProjectReportCreateOrDetailsRequest request)
        {
                ProjectReportDetailsResponse reportResponse=new ProjectReportDetailsResponse();
            if (request.IsCreate)
            {
                var projectNewReport = new Report
                {
                    ReportCreatedBy = request.RequesterId,
                    ReportCreatedDate = DateTime.Now,
                    ReportFromDate = DateTime.Now,
                    ReportToDate = DateTime.Now
                };
                if (request.ProjectId > 0)
                {
                    projectNewReport.ProjectId = request.ProjectId;
                    projectNewReport.ReportCategoryId = (int)ReportCategory.Project;
                }
                else
                {
                    projectNewReport.ReportCategoryId = (int)ReportCategory.AllProjects;
                }

                request.ReportCreatedDate = projectNewReport.ReportCreatedDate;

                    //Fetch Report data
                var response = projectRepository.GetProjectReportDetails(request).ToList();
                projectNewReport.ReportProjects = response.Select(x => x.MapProjectToReportProject()).ToList();

                    //Save Report and its data
                reportRepository.Add(projectNewReport);
                reportRepository.SaveChanges();


                    //Result Data to be Returned
                request.ReportId = projectNewReport.ReportId;
                    reportResponse.Projects = projectNewReport.ReportProjects;
                    reportResponse.ProjectTasks = projectNewReport.ReportProjectTasks;
            }
            else
            {
                var report = reportRepository.Find(request.ReportId);
                if (report.ProjectId != null)
                {
                    request.ProjectId = (long)report.ProjectId;
                    request.ReportCreatedDate = report.ReportCreatedDate;

                        reportResponse.Projects = report.ReportProjects;
                        reportResponse.ProjectTasks = report.ReportProjectTasks;
                }
            }
                reportResponse.ReportId = request.ReportId;
                return reportResponse;
        }

        public IEnumerable<ReportProject> SaveAndGetAllProjectsReport(ProjectReportCreateOrDetailsRequest request)
        {
            var createdBefore = DateTime.Now;
                var resultResponse = new List<ReportProject>();
            if (request.IsCreate)
            {
                var projectNewReport = new Report
                {
                    ReportCategoryId = (int)ReportCategory.AllProjects,
                    ReportCreatedBy = request.RequesterId,
                    ReportCreatedDate = DateTime.Now,
                    ReportFromDate = DateTime.Now,
                    ReportToDate = DateTime.Now
                };
                if (request.ProjectId > 0)
                {
                    projectNewReport.ProjectId = request.ProjectId;
                    projectNewReport.ReportCreatedDate = request.ReportCreatedDate;
                }
                    var response = projectRepository.GetAllProjects(createdBefore).ToList();
                    projectNewReport.ReportProjects = response.Select(x => x.MapProjectToReportProject()).ToList();
                reportRepository.Add(projectNewReport);
                reportRepository.SaveChanges();
                request.ReportId = projectNewReport.ReportId;
                    resultResponse = projectNewReport.ReportProjects.ToList();
            }
            else
            {
                var report = reportRepository.Find(request.ReportId);
                    resultResponse = report.ReportProjects.ToList();
            }

                return resultResponse;
        }

        public long SaveInventoryItemsReport(InventoryItemReportCreateOrDetailsRequest request)
        {
                var newReport = new Report
                {
                    ReportCreatedBy = request.RequesterId,
                    ReportCreatedDate = DateTime.Now,
                    ReportFromDate = DateTime.Now,
                    ReportToDate = DateTime.Now
                };
                if (request.ItemId > 0)
                {
                    newReport.ReportCategoryId = (int)ReportCategory.Item;
                }
                else
                {
                    newReport.ReportCategoryId = (int)ReportCategory.AllItems;
                }

                //Fetch Report data
                var response = inventoryItemRepository.GetInventoryItemReportDetails(request).ToList();
                newReport.ReportInventoryItems = response.Select(x => x.MapInventoryItemToReportInventoryItem()).ToList();

                //Save Report and its data
                reportRepository.Add(newReport);
                reportRepository.SaveChanges();

                return newReport.ReportId;
        }
        public IEnumerable<ReportInventoryItem> GetInventoryItemsReport(long reportId)
        {
            return reportRepository.Find(reportId).ReportInventoryItems;
        }

        public TaskReportDetailsResponse SaveAndGetTaskReportDetails(TaskReportCreateOrDetailsRequest request)
        {
                TaskReportDetailsResponse detailResponse = new TaskReportDetailsResponse();
            if (request.IsCreate)
            {
                    //CreateTaskReport(request);
                    var taskReportToCreate = new Report
                    {
                        ReportCreatedBy = request.RequesterId,
                        ReportCreatedDate = DateTime.Now,
                        ReportFromDate = DateTime.Now,
                        ReportToDate = DateTime.Now
                    };
                    if (request.ProjectId == 0 && request.TaskId == 0)
                    {
                        taskReportToCreate.ReportCategoryId = (int) ReportCategory.AllProjectsAllTasks;
                    }
                    else if (request.ProjectId > 0 && request.TaskId == 0)
                    {
                        taskReportToCreate.ProjectId = request.ProjectId;
                        taskReportToCreate.ReportCategoryId = (int)ReportCategory.ProjectAllTasks;
                    }
                    else if (request.ProjectId > 0 && request.TaskId > 0)
                    {
                        taskReportToCreate.ProjectId = request.ProjectId;
                        taskReportToCreate.TaskId = request.TaskId;
                        taskReportToCreate.ReportCategoryId = (int)ReportCategory.ProjectTask;
                    }
                    request.ReportCreatedDate = taskReportToCreate.ReportCreatedDate;
                    var response = taskRepository.GetTaskReportDetails(request).ToList();
                    taskReportToCreate.ReportProjectTasks = response.Where(x => x.ParentTask == null).Select(x => x.MapProjectTaskToReportProjectTask()).ToList();

                    reportRepository.Add(taskReportToCreate);
                    reportRepository.SaveChanges();
                    request.ReportId = taskReportToCreate.ReportId;

                    detailResponse.ProjectTasks = response.Where(x => x.ParentTask == null).Select(x => x.MapProjectTaskToReportProjectTask());
                    detailResponse.SubTasks = response.Where(x => x.SubTasks != null).SelectMany(x => x.SubTasks).Select(x => x.MapProjectTaskToReportProjectTask());
            }
            else
            {
                var report = reportRepository.Find(request.ReportId);
                    if (report != null)
                    {
                        if (report.ProjectId != null) request.ProjectId = (long)report.ProjectId;
                        if (report.TaskId != null) request.TaskId = (long)report.TaskId;

                        detailResponse.ProjectTasks = report.ReportProjectTasks.Where(x => x.ReportProjectParentTask == null).ToList();
                        detailResponse.SubTasks = report.ReportProjectTasks.Where(x => x.ReportProjectSubTasks != null).SelectMany(x => x.ReportProjectSubTasks).ToList();
                    }
            }
            detailResponse.ReportId = request.ReportId;
            return detailResponse;
        }
        private void CreateTaskReport(TaskReportCreateOrDetailsRequest request)
        {
                Report taskReportToCreate = new Report();
            if (request.ProjectId == 0 && request.TaskId == 0)
            {
                taskReportToCreate = new Report
                    {
                        ReportCategoryId = (int)ReportCategory.AllProjectsAllTasks,
                        ReportCreatedBy = request.RequesterId,
                        ReportCreatedDate = DateTime.Now,
                        ReportFromDate = DateTime.Now,
                        ReportToDate = DateTime.Now
                    };
                reportRepository.Add(taskReportToCreate);
                reportRepository.SaveChanges();
            }
            else if (request.ProjectId > 0 && request.TaskId == 0)
            {
                taskReportToCreate = new Report
                    {
                        ProjectId = request.ProjectId,
                        ReportCategoryId = (int)ReportCategory.ProjectAllTasks,
                        ReportCreatedBy = request.RequesterId,
                        ReportCreatedDate = DateTime.Now,
                        ReportFromDate = DateTime.Now,
                        ReportToDate = DateTime.Now
                    };
                reportRepository.Add(taskReportToCreate);
                reportRepository.SaveChanges();
            }
            else if (request.ProjectId > 0 && request.TaskId > 0)
            {
                taskReportToCreate = new Report
                    {
                        ProjectId = request.ProjectId,
                        TaskId = request.TaskId,
                        ReportCategoryId = (int)ReportCategory.ProjectTask,
                        ReportCreatedBy = request.RequesterId,
                        ReportCreatedDate = DateTime.Now,
                        ReportFromDate = DateTime.Now,
                        ReportToDate = DateTime.Now
                    };
                reportRepository.Add(taskReportToCreate);
                reportRepository.SaveChanges();
            }
            request.ReportId = taskReportToCreate.ReportId;
            request.ReportCreatedDate = taskReportToCreate.ReportCreatedDate;
        }
        #endregion
    }
}
