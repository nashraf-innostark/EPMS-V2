using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Transactions;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.Common;
using EPMS.Models.DomainModels;
using EPMS.Models.ModelMapers;
using EPMS.Models.RequestModels.Reports;
using EPMS.Models.ResponseModels.ReportsResponseModels;
using Customer = EPMS.Models.DashboardModels.Customer;

namespace EPMS.Implementation.Services
{
    public class ReportService : IReportService
    {
        #region Private

        private readonly IInventoryItemRepository inventoryItemRepository;
        private readonly IReportRepository reportRepository;
        private readonly IRFQRepository rfqRepository;
        private readonly IProjectRepository projectRepository;
        private readonly ICustomerService customerService;
        private readonly ICustomerRepository customerRepository;
        private readonly IQuotationRepository quotationRepository;
        private readonly IProjectTaskRepository taskRepository;
        private readonly IWarehouseRepository warehouseRepository;
        private readonly IEmployeeRepository employeeRepository;
        private readonly IOrdersRepository ordersRepository;
        private readonly IInvoiceRepository invoiceRepository;
        private readonly IReportQuotaionInvoiceRepository qIRepository;

        #endregion

        #region Constructor

        public ReportService(IReportRepository reportRepository,IRFQRepository rfqRepository, IProjectRepository projectRepository,ICustomerService customerService,
            ICustomerRepository customerRepository, IQuotationRepository quotationRepository,
            IProjectTaskRepository taskRepository, IWarehouseRepository warehouseRepository, IEmployeeRepository employeeRepository, IOrdersRepository ordersRepository, IInvoiceRepository invoiceRepository, IReportQuotaionInvoiceRepository qIRepository)
        {
            this.reportRepository = reportRepository;
            this.rfqRepository = rfqRepository;
            this.projectRepository = projectRepository;
            this.customerService = customerService;
            this.customerRepository = customerRepository;
            this.quotationRepository = quotationRepository;
            this.taskRepository = taskRepository;
            this.warehouseRepository = warehouseRepository;
            this.employeeRepository = employeeRepository;
            this.ordersRepository = ordersRepository;
            this.invoiceRepository = invoiceRepository;
            this.qIRepository = qIRepository;
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

        public ReportsListRequestResponse GetRFQOrdersReports(WarehouseReportSearchRequest searchRequest)
        {
            return reportRepository.GetRFQOrdersReports(searchRequest);
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
            QuotationInvoiceReportResponse qIResponse;
            Report newReport;
            if (request.IsCreate)
            {
                newReport = new Report
                {
                    ReportCategoryId = (int) ReportCategory.AllQuotationInvoice,
                    EmployeeId = request.EmployeeId,
                    ReportCreatedBy = request.RequesterId,
                    ReportCreatedDate = DateTime.Now,
                    ReportFromDate = request.StartDate,
                    ReportToDate = request.EndDate,
                    ReportQuotationInvoices = new List<ReportQuotationInvoice>()
                };
            }
            else
            {
                newReport = reportRepository.Find(request.ReportId);

                request.StartDate = newReport.ReportFromDate;
                request.EndDate = newReport.ReportToDate;
                request.EmployeeId = Convert.ToInt64(newReport.EmployeeId);
                return new QuotationInvoiceReportResponse
                {
                    Report = newReport
                };
            }

            var employee = employeeRepository.Find(request.EmployeeId);
            var empId = employee.AspNetUsers.FirstOrDefault().Id;
            var quotations = quotationRepository.GetAll().Where(x => x.RecCreatedBy == empId);
            var invoices = invoiceRepository.GetAll().Where(x => x.RecCreatedBy == empId);

            //Save Report
            var QuotationInvoiceReport = new ReportQuotationInvoice
            {
                EmployeeId = employee.EmployeeId,
                EmployeeNameE = employee.EmployeeFirstNameE + " " + employee.EmployeeMiddleNameE + " " + employee.EmployeeLastNameE,
                EmployeeNameA = employee.EmployeeFirstNameA + " " + employee.EmployeeMiddleNameA + " " + employee.EmployeeLastNameA,
                NoOfInvoices = invoices.Count(),
                NoOfQuotations = quotations.Count(),
                ReportQuotationInvoiceItems = new Collection<ReportQuotationInvoiceItem>()
            };

            //Save Report Items from Quotations
            var quotationGroups = quotations.Where(i => i.RecCreatedDate != null)
                    .OrderBy(i => i.RecCreatedDate.Month)
                    .GroupBy(i => i.RecCreatedDate.Month).ToArray();
            foreach (var quotationGroup in quotationGroups)
            {
                QuotationInvoiceReport.ReportQuotationInvoiceItems .Add(new ReportQuotationInvoiceItem
                {
                    IsQuotationItem = true,
                    TotalPrice = quotationGroup.Sum(x => x.QuotationItemDetails.Sum(y => y.TotalPrice)).ToString(),
                    MonthTimeStamp = GetJavascriptTimestamp(quotationGroup.FirstOrDefault().RecCreatedDate).ToString()
                });
            }

            //Save Report Items from Invoices
            var invoiceGroups = invoices.Where(i => i.RecCreatedDt != null)
                    .OrderBy(i => i.RecCreatedDt.Month)
                    .GroupBy(i => i.RecCreatedDt.Month).ToArray();
            foreach (var invoiceGroup in invoiceGroups)
            {
                QuotationInvoiceReport.ReportQuotationInvoiceItems.Add(new ReportQuotationInvoiceItem
                {
                    IsInvoiceItem = true,
                    TotalPrice = invoiceGroup.Sum(x => x.Quotation.QuotationItemDetails.Sum(y => y.TotalPrice)).ToString(),
                    MonthTimeStamp = GetJavascriptTimestamp(invoiceGroup.FirstOrDefault().RecCreatedDt).ToString()
                });
            }

            newReport.ReportQuotationInvoices.Add(QuotationInvoiceReport);
            reportRepository.Add(newReport);
            reportRepository.SaveChanges();


            return new QuotationInvoiceReportResponse
            {
                Quotations = quotations,
                Invoices = invoices,
                EmployeeNameE = employee.EmployeeFirstNameE + " " + employee.EmployeeMiddleNameE + " " + employee.EmployeeLastNameE,
                EmployeeNameA = employee.EmployeeFirstNameA + " " + employee.EmployeeMiddleNameA + " " + employee.EmployeeLastNameA,
                InvoicesCount = invoices.Count(),
                QuotationsCount = quotations.Count(),
                StartDate = request.StartDate.ToShortDateString(),
                EndDate = request.EndDate.ToShortDateString(),
                ReportId = newReport.ReportId
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
        public long SaveQOReport(QOReportCreateOrDetailsRequest request)
        {
            var newReport = new Report
            {
                ReportCreatedBy = request.RequesterId,
                ReportCreatedDate = DateTime.Now,
                ReportFromDate = DateTime.Now,
                ReportToDate = DateTime.Now,
                ReportQuotationOrders = new List<ReportQuotationOrder>()
            };
            if (request.CustomerId > 0)
            {
                newReport.ReportCategoryId = (int)ReportCategory.CustomerQO;
                newReport.CustomerId = request.CustomerId;

                //Fetch data for Report 
                var customerQuotations = rfqRepository.GetAllRFQsByCustomerId(request).ToList();
                var customerOrders = ordersRepository.GetOrdersByCustomerId(request).ToList();
                var customer = customerService.FindCustomerById(request.CustomerId);
                //reportQuotationOrder Report

                var reportQuotationOrder = new ReportQuotationOrder
                {
                    CustomerId = request.CustomerId,
                    NoOfOrders = customerOrders.Count(),
                    NoOfRFQ = customerQuotations.Count(),
                    CustomerNameE = customer.CustomerNameE,
                    CustomerNameA = customer.CustomerNameA,
                    ReportQuotationOrderItems = new List<ReportQuotationOrderItem>()
                };

                //Items of Quotations for reportQuotationOrder
                var itemsOfQuot = customerQuotations.OrderBy(x => x.RecCreatedDate).GroupBy(x => x.RecCreatedDate.Month).ToList();
                foreach (var quot in itemsOfQuot)
                {
                    reportQuotationOrder.ReportQuotationOrderItems.Add(new ReportQuotationOrderItem
                    {
                        IsQuotationReport = true,
                        TotalPrice = quot.Sum(x => x.RFQItems.Sum(y => y.TotalPrice)).ToString(),
                        MonthTimeStamp = GetJavascriptTimestamp(quot.FirstOrDefault().RecCreatedDate).ToString()
                    });
                }

                //Items of Orders for reportQuotationOrder
                var itemsOfOrders = customerOrders.OrderBy(x => x.RecCreatedDate).GroupBy(x => x.RecCreatedDate.Month).ToList();
                foreach (var order in itemsOfOrders)
                {
                    reportQuotationOrder.ReportQuotationOrderItems.Add(new ReportQuotationOrderItem
                    {
                        IsOrderReport = true,
                        TotalPrice = order.Sum(x => x.Quotation.QuotationItemDetails.Sum(y => y.TotalPrice)).ToString(),
                        MonthTimeStamp = GetJavascriptTimestamp(order.FirstOrDefault().RecCreatedDate).ToString()
                    });
                }
                newReport.ReportQuotationOrders.Add(reportQuotationOrder);
                //Save Report and its data
                reportRepository.Add(newReport);
                reportRepository.SaveChanges();
            }
            else
            {
                newReport.ReportCategoryId = (int)ReportCategory.AllCustomersQO;
                //Fetch data for Report 
                var customers = customerService.GetAll().ToList();
                foreach (var customer in customers)
                {
                    request.CustomerId = customer.CustomerId;

                    var customerQuotations = rfqRepository.GetAllRFQsByCustomerId(request).ToList();
                    var customerOrders = ordersRepository.GetOrdersByCustomerId(request).ToList();

                    //reportQuotationOrder Report

                    var reportQuotationOrder = new ReportQuotationOrder
                    {
                        CustomerId = request.CustomerId,
                        NoOfOrders = customerOrders.Count(),
                        NoOfRFQ = customerQuotations.Count(),
                        CustomerNameE = customer.CustomerNameE,
                        CustomerNameA = customer.CustomerNameA,
                    };

                    newReport.ReportQuotationOrders.Add(reportQuotationOrder);
                }
                
                //Save Report and its data
                reportRepository.Add(newReport);
                reportRepository.SaveChanges();
            }

            

            return newReport.ReportId;
        }

        public long SaveQIReport(QuotationInvoiceDetailRequest request)
        {
            var newReport = new Report
            {
                ReportCreatedBy = request.RequesterId,
                ReportCreatedDate = DateTime.Now,
                ReportFromDate = DateTime.Now,
                ReportToDate = DateTime.Now,
                ReportQuotationInvoices = new List<ReportQuotationInvoice>()
            };
            if (request.EmployeeId > 0)
            {
                newReport.ReportCategoryId = (int)ReportCategory.QuotationInvoice;
                newReport.EmployeeId = request.EmployeeId;

                var employee = employeeRepository.Find(request.EmployeeId);
                var empId = employee.AspNetUsers.FirstOrDefault().Id;
                var quotations = quotationRepository.GetAll().Where(x => x.RecCreatedBy == empId);
                var invoices = invoiceRepository.GetAll().Where(x => x.RecCreatedBy == empId);

                var reportQuotationInvoice = new ReportQuotationInvoice()
                {
                    EmployeeId = request.EmployeeId,
                    NoOfQuotations = quotations.Count(),
                    NoOfInvoices = invoices.Count(),
                    EmployeeNameE = employee.EmployeeFirstNameE + " " + employee.EmployeeMiddleNameE + " " + employee.EmployeeLastNameE,
                    EmployeeNameA = employee.EmployeeFirstNameA + " " + employee.EmployeeMiddleNameA + " " + employee.EmployeeLastNameA,
                    ReportQuotationInvoiceItems = new List<ReportQuotationInvoiceItem>()
                };

                //Items of Quotations for reportQuotationOrder
                var itemsOfQuot = quotations.OrderBy(x => x.RecCreatedDate).GroupBy(x => x.RecCreatedDate.Month).ToList();
                foreach (var quot in itemsOfQuot)
                {
                    reportQuotationInvoice.ReportQuotationInvoiceItems.Add(new ReportQuotationInvoiceItem
                    {
                        IsQuotationItem = true,
                        TotalPrice = quot.Sum(x => x.QuotationItemDetails.Sum(y => y.TotalPrice)).ToString(),
                        MonthTimeStamp = GetJavascriptTimestamp(quot.FirstOrDefault().RecCreatedDate).ToString()
                    });
                }

                //Items of Orders for reportQuotationOrder
                var itemsOfOrders = invoices.OrderBy(x => x.RecCreatedDt).GroupBy(x => x.RecCreatedDt.Month).ToList();
                foreach (var order in itemsOfOrders)
                {
                    reportQuotationInvoice.ReportQuotationInvoiceItems.Add(new ReportQuotationInvoiceItem
                    {
                        IsInvoiceItem = true,
                        TotalPrice = order.Sum(x => x.Quotation.QuotationItemDetails.Sum(y => y.TotalPrice)).ToString(),
                        MonthTimeStamp = GetJavascriptTimestamp(order.FirstOrDefault().RecCreatedDt).ToString()
                    });
                }
                newReport.ReportQuotationInvoices.Add(reportQuotationInvoice);
                //Save Report and its data
                reportRepository.Add(newReport);
                reportRepository.SaveChanges();
            }
            else
            {
                newReport.ReportCategoryId = (int)ReportCategory.AllQuotationInvoice;
                //Fetch data for Report 
                var employees = employeeRepository.GetAll().ToList();
                foreach (var employee in employees)
                {
                    request.EmployeeId = employee.EmployeeId;
                    var empId = employee.AspNetUsers.FirstOrDefault().Id;

                    var quotations = quotationRepository.GetAll().Where(x => x.RecCreatedBy == empId);
                    var invoices = invoiceRepository.GetAll().Where(x => x.RecCreatedBy == empId);

                    //reportQuotationOrder Report

                    var reportQuotationInvoice = new ReportQuotationInvoice
                    {
                        EmployeeId = request.EmployeeId,
                        NoOfQuotations = quotations.Count(),
                        NoOfInvoices = invoices.Count(),
                        EmployeeNameE = employee.EmployeeFirstNameE + " " + employee.EmployeeMiddleNameE + " " + employee.EmployeeLastNameE,
                        EmployeeNameA = employee.EmployeeFirstNameA + " " + employee.EmployeeMiddleNameA + " " + employee.EmployeeLastNameA,
                    };

                    newReport.ReportQuotationInvoices.Add(reportQuotationInvoice);
                }

                //Save Report and its data
                reportRepository.Add(newReport);
                reportRepository.SaveChanges();
            }
            return newReport.ReportId;
        }

        public IEnumerable<ReportQuotationOrder> GetQOReport(long reportId)
        {
            return reportRepository.Find(reportId).ReportQuotationOrders.ToList();
        }

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

        public IEnumerable<ReportQuotationInvoice> GetQIReport(long reportId)
        {
            return reportRepository.Find(reportId).ReportQuotationInvoices.ToList();
        }

        #endregion
        private static long GetJavascriptTimestamp(DateTime input)
        {
            TimeSpan span = new TimeSpan(DateTime.Parse("1/1/1970").Ticks);
            DateTime time = input.Subtract(span);
            return (time.Ticks / 10000);
        }
    }
}