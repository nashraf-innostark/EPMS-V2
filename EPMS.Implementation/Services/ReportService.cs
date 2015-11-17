using System;
using System.Collections.Generic;
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
        private readonly IProjectTaskRepository taskRepository;
        private readonly IWarehouseRepository warehouseRepository;

        #endregion
        #region Constructor
        public ReportService(IReportRepository reportRepository, IProjectRepository projectRepository, IProjectTaskRepository taskRepository, IWarehouseRepository warehouseRepository)
        {
            this.reportRepository = reportRepository;
            this.projectRepository = projectRepository;
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
           
        #endregion
        #region Reports Details Views
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
                        ProjectId = request.ProjectId,
                        ReportCategoryId = (int)ReportCategory.Project,
                        ReportCreatedBy = request.RequesterId,
                        ReportCreatedDate = DateTime.Now,
                        ReportFromDate = DateTime.Now,
                        ReportToDate = DateTime.Now
                    };
                    if (request.ProjectId > 0)
                    {
                        projectNewReport.WarehouseId = request.ProjectId;
                        projectNewReport.ReportCategoryId = (int)ReportCategory.Project;
                    }
                    else
                    {
                        projectNewReport.ReportCategoryId = (int)ReportCategory.AllProjects;
                    }
                    reportRepository.Add(projectNewReport);
                    reportRepository.SaveChanges();
                    request.ReportId = projectNewReport.ReportId;
                    request.ReportCreatedDate = projectNewReport.ReportCreatedDate;
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
                var response = projectRepository.GetProjectReportDetails(request).ToList();
                return new ProjectReportDetailsResponse
                {
                    ReportId = request.ReportId,
                    Projects = response,
                    ProjectTasks = response.FirstOrDefault().ProjectTasks
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
            public IEnumerable<Project> SaveAndGetAllProjectsReport(ProjectReportCreateOrDetailsRequest request)
            {
                var createdBefore = DateTime.Now;
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
                    reportRepository.Add(projectNewReport);
                    reportRepository.SaveChanges();
                    request.ReportId = projectNewReport.ReportId;
                }
                else
                {
                    var report = reportRepository.Find(request.ReportId);
                    createdBefore = report.ReportCreatedDate;
                }
                var response = projectRepository.GetAllProjects(createdBefore).ToList();
                return response;
            }
            public TaskReportDetailsResponse SaveAndGetTaskReportDetails(TaskReportCreateOrDetailsRequest request)
            {
                if (request.IsCreate)
                {
                    CreateTaskReport(request);
                }
                else
                {
                    var report = reportRepository.Find(request.ReportId);
                    if (report.ProjectId != null) request.ProjectId = (long)report.ProjectId;
                    if (report.TaskId != null) request.TaskId = (long)report.TaskId;
                }
                var response = taskRepository.GetTaskReportDetails(request).ToList();
                TaskReportDetailsResponse detailResponse = new TaskReportDetailsResponse
                {
                    ProjectTasks = response.Where(x => x.ParentTask == null),
                    SubTasks = response.Where(x => x.SubTasks != null).SelectMany(x => x.SubTasks)
                };
                return detailResponse;
            }
            private void CreateTaskReport(TaskReportCreateOrDetailsRequest request)
            {
                if (request.ProjectId == 0 && request.TaskId == 0)
                {
                    var taskReportToCreate = new Report
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
                    var taskReportToCreate = new Report
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
                    var taskReportToCreate = new Report
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
            }
        #endregion
    }
}
