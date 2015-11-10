﻿using System;
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

        #endregion

        #region Constructor
        public ReportService(IReportRepository reportRepository, IProjectRepository projectRepository, IProjectTaskRepository taskRepository)
        {
            this.reportRepository = reportRepository;
            this.projectRepository = projectRepository;
            this.taskRepository = taskRepository;
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
                if (report.TaskId != null) request.TaskId = (long) report.TaskId;
            }
            var response = taskRepository.GetTaskReportDetails(request).ToList();
            TaskReportDetailsResponse detailResponse = new TaskReportDetailsResponse
            {
                ProjectTasks = response.Where(x=>x.ParentTask == null),
                SubTasks = response.Where(x=>x.ParentTask != null)
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
