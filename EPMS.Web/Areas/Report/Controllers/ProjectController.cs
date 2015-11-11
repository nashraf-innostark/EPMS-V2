﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.RequestModels.Reports;
using EPMS.Web.Controllers;
using EPMS.WebModels.ModelMappers;
using EPMS.WebModels.ModelMappers.PMS;
using EPMS.WebModels.ViewModels.Reports;
using Rotativa;

namespace EPMS.Web.Areas.Report.Controllers
{
    public class ProjectController : BaseController
    {
        private readonly IProjectService projectService;
        private readonly IReportService reportService;

        public ProjectController(IProjectService projectService, IReportService reportService)
        {
            this.projectService = projectService;
            this.reportService = reportService;
        }

        // GET: Report/Project
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            ProjectsReportsCreateViewModel projectsReportsCreateViewModel=new ProjectsReportsCreateViewModel
            {
                Projects = projectService.GetAllProjects().ToList().Select(x => x.CreateForDashboardDDL()).ToList()
            };
            return View(projectsReportsCreateViewModel);
        }

        public ActionResult Details(ProjectsReportsCreateViewModel projectsReportsCreateViewModel)
        {
            ProjectReportDetailVeiwModel detailVeiwModel = new ProjectReportDetailVeiwModel();

            var request = new ProjectReportCreateOrDetailsRequest
            {
                ProjectId = projectsReportsCreateViewModel.ProjectId,
                ReportId = projectsReportsCreateViewModel.ReportId,
                RequesterRole = "Admin",
                RequesterId = Session["UserID"].ToString()
            };

            //Check if request came from "Report Create Page"
            var refrel=Request.UrlReferrer;
            if (refrel != null && refrel.ToString().Contains("Report/Project/Create"))
                request.IsCreate = true;
            if (projectsReportsCreateViewModel.ProjectId > 0 || projectsReportsCreateViewModel.ReportId > 0)
            {
                var response = reportService.SaveAndGetProjectReportDetails(request);
                detailVeiwModel.Projects = response.Projects.Select(x => x.CreateForReportDetails()).ToList();
                detailVeiwModel.ProjectTasks = response.ProjectTasks.Select(x => x.CreateForReport()).ToList();
                detailVeiwModel.ReportId = response.ReportId;
            }
            SetGraphData(detailVeiwModel);
            return View(detailVeiwModel);
        }
        private static long GetJavascriptTimestamp(DateTime input)
        {
            TimeSpan span = new TimeSpan(DateTime.Parse("1/1/1970").Ticks);
            DateTime time = input.Subtract(span);
            return (time.Ticks / 10000);
        }
        private ProjectReportDetailVeiwModel SetGraphData(ProjectReportDetailVeiwModel detailVeiwModel)
        {
            var project = detailVeiwModel.Projects.FirstOrDefault();
            detailVeiwModel.GrpahStartTimeStamp = GetJavascriptTimestamp(DateTime.ParseExact(project.StartDate.ToString(), "dd/MM/yyyy", new CultureInfo("en")));
            detailVeiwModel.GrpahEndTimeStamp = GetJavascriptTimestamp(DateTime.ParseExact(project.EndDate.ToString(), "dd/MM/yyyy", new CultureInfo("en")));

            detailVeiwModel.GraphItems.Add(new GraphItem
            {
               ItemLabel = "Price",
               ItemValue = new List<GraphLabel>
                {
                    new GraphLabel
                    {
                        label = "Price",
                        data = new List<GraphLabelData>
                        {
                            new GraphLabelData
                            {
                                dataValue = new List<GraphLabelDataValues>
                                {
                                    new GraphLabelDataValues
                                    {
                                        TimeStamp = detailVeiwModel.GrpahStartTimeStamp,
                                        Value = project.Price
                                    },
                                    new GraphLabelDataValues
                                    {
                                        TimeStamp = detailVeiwModel.GrpahEndTimeStamp,
                                        Value = project.Price
                                    }
                                }
                            }
                        }
                    }
                }
            });
            detailVeiwModel.GraphItems.Add(new GraphItem
            {
                ItemLabel = "Cost",
                ItemValue = new List<GraphLabel>
                {
                    new GraphLabel
                    {
                        label = "Cost",
                        data = new List<GraphLabelData>
                        {
                            new GraphLabelData
                            {
                                dataValue = new List<GraphLabelDataValues>
                                {
                                    new GraphLabelDataValues
                                    {
                                        TimeStamp = detailVeiwModel.GrpahStartTimeStamp,
                                        Value = project.OtherCost
                                    },
                                    new GraphLabelDataValues
                                    {
                                        TimeStamp = detailVeiwModel.GrpahEndTimeStamp,
                                        Value = project.OtherCost
                                    }
                                }
                            }
                        }
                    }
                }
            });
            
            foreach (var projectTask in detailVeiwModel.ProjectTasks)
            {
                detailVeiwModel.GraphItems.Add(new GraphItem
                {
                    ItemLabel = "Task",
                    ItemValue = new List<GraphLabel>
                    {
                        new GraphLabel
                        {
                            label = System.Threading.Thread.CurrentThread.CurrentCulture.ToString() == "en" ? projectTask.TaskNameE : projectTask.TaskNameA,
                            data = new List<GraphLabelData>
                            {
                                new GraphLabelData
                                {
                                    dataValue = new List<GraphLabelDataValues>
                                    {
                                        new GraphLabelDataValues
                                        {
                                            TimeStamp = GetJavascriptTimestamp(DateTime.ParseExact(projectTask.StartDate.ToString(), "dd/MM/yyyy", new CultureInfo("en"))),
                                            Value = projectTask.TotalCost
                                        },
                                        new GraphLabelDataValues
                                        {
                                            TimeStamp = GetJavascriptTimestamp(DateTime.ParseExact(projectTask.EndDate.ToString(), "dd/MM/yyyy", new CultureInfo("en"))),
                                            Value = projectTask.TotalCost
                                        }
                                    }
                                }
                            }
                        }
                    }
                });
            }
            return detailVeiwModel;
        }
        public ActionResult GeneratePDF(ProjectsReportsCreateViewModel projectsReportsCreateViewModel)
        {
            return new ActionAsPdf("Create") { FileName = "ProjectDetailedReport.pdf" };
        }
    }
}