using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
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
        private readonly IEmployeeRepository employeeRepository;

        #endregion

        #region Constructor

        public ReportService(IReportRepository reportRepository, IProjectRepository projectRepository,
            ICustomerRepository customerRepository, IQuotationRepository quotationRepository,
            IProjectTaskRepository taskRepository, IWarehouseRepository warehouseRepository, IEmployeeRepository employeeRepository)
        {
            this.inventoryItemRepository = inventoryItemRepository;
            this.reportRepository = reportRepository;
            this.projectRepository = projectRepository;
            this.customerRepository = customerRepository;
            this.quotationRepository = quotationRepository;
            this.taskRepository = taskRepository;
            this.warehouseRepository = warehouseRepository;
            this.employeeRepository = employeeRepository;
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
                    ReportCategoryId = (int) ReportCategory.AllQuotationInvoice,
                    EmployeeId = request.EmployeeId,
                    ReportCreatedBy = request.RequesterId,
                    ReportCreatedDate = DateTime.Now,
                    ReportFromDate = request.StartDate,
                    ReportToDate = request.EndDate
                };
                reportRepository.Add(quotationInvoiceReport);
                reportRepository.SaveChanges();
            }
            else
            {
                var report = reportRepository.Find(request.ReportId);
                request.StartDate = report.ReportFromDate;
                request.EndDate = report.ReportToDate;
                request.EmployeeId = Convert.ToInt64(report.EmployeeId);
            }

            //Invoice Module under development
            var invoicesCount = 0;

            var employee = employeeRepository.Find(request.EmployeeId);
            var empId = employee.AspNetUsers.FirstOrDefault().Id;
            IEnumerable<Quotation> quotations = quotationRepository.GetAll().Where(x => x.RecCreatedBy == empId);
            
            return new QuotationInvoiceReportResponse
            {
                Quotations = quotations,
                EmployeeNameE = employee.EmployeeFirstNameE + " " + employee.EmployeeMiddleNameE + " " + employee.EmployeeLastNameE,
                EmployeeNameA = employee.EmployeeFirstNameA + " " + employee.EmployeeMiddleNameA + " " + employee.EmployeeLastNameA,
                InvoicesCount = invoicesCount,
                QuotationsCount = quotations.Count(),
                StartDate = request.StartDate.ToShortDateString(),
                EndDate = request.EndDate.ToShortDateString()
            };
        }

        public CustomerReportListResponse GetQuotationInvoiceReports(CustomerServiceReportsSearchRequest request)
        {
            return reportRepository.GetQuotationInvoiceReports(request);
        }

        public CustomerReportListResponse GetAllCustoemrReport(CustomerServiceReportsSearchRequest request)
        {
            return reportRepository.GetQuotationInvoiceReports(request);
        }

        #endregion


        #region Create Reports and Details Views
        public bool AddReport(Report report)
        {
            reportRepository.Add(report);
            reportRepository.SaveChanges();
            return true;
        }
        public ProjectReportDetailsResponse SaveAndGetProjectReportDetails(ProjectReportCreateOrDetailsRequest request)
        {
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

                var response = projectRepository.GetProjectReportDetails(request).ToList();
                projectNewReport.ReportProjects = response.Select(x => x.MapProjectToReportProject()).ToList();
                reportRepository.Add(projectNewReport);
                reportRepository.SaveChanges();
                request.ReportId = projectNewReport.ReportId;
            }
            else
            {
                var report = reportRepository.Find(request.ReportId);
                if (report.ProjectId != null)
                {
                    request.ProjectId = (long)report.ProjectId;
                    request.ReportCreatedDate = report.ReportCreatedDate;
                }
            }

            return new ProjectReportDetailsResponse
            {
                ReportId = request.ReportId,
                //Projects = response,
                //ProjectTasks = response.FirstOrDefault().ProjectTasks
            };
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

        public IEnumerable<ReportProject> SaveAndGetAllProjectsReport(ProjectReportCreateOrDetailsRequest request)
        {
            var createdBefore = DateTime.Now;
                var resultResponse = new List<ReportProject>();
            if (request.IsCreate)
            {
                var projectNewReport = new Report
                {
                    ReportCategoryId = (int) ReportCategory.AllProjects,
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
                    newReport.InventoryItemId = request.ItemId;
                }
                else
                {
                    newReport.ReportCategoryId = (int)ReportCategory.AllItems;
                }

                //Fetch Report data
                var response = inventoryItemRepository.GetInventoryItemReportDetails(request).ToList();
                newReport.ReportInventoryItems = request.ItemId > 0 ?
                newReport.ReportInventoryItems = response.SelectMany(x => x.ItemVariations.Select(y=>y.MapInventoryItemVariationToReportInventoryItem())).ToList() :
                newReport.ReportInventoryItems = response.Select(x => x.MapInventoryItemToReportInventoryItem()).ToList()
                ;

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
                    ReportCategoryId = (int) ReportCategory.AllProjectsAllTasks,
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
                    ReportCategoryId = (int) ReportCategory.ProjectAllTasks,
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
                    ReportCategoryId = (int) ReportCategory.ProjectTask,
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