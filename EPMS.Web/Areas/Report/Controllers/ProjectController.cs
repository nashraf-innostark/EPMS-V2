using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.RequestModels.Reports;
using EPMS.Web.Controllers;
using EPMS.WebBase.Mvc;
using EPMS.WebModels.ModelMappers;
using EPMS.WebModels.ModelMappers.PMS;
using EPMS.WebModels.ViewModels.Project;
using EPMS.WebModels.ViewModels.Reports;
using EPMS.WebModels.WebsiteModels;
using Rotativa;

namespace EPMS.Web.Areas.Report.Controllers
{
    [Authorize]
    //[SiteAuthorize(PermissionKey = "Reports", IsModule = true)]
    public class ProjectController : BaseController
    {
        private readonly IProjectService projectService;
        private readonly IReportService reportService;

        public ProjectController(IProjectService projectService, IReportService reportService)
        {
            this.projectService = projectService;
            this.reportService = reportService;
        }

        [SiteAuthorize(PermissionKey = "GenerateProjectsReport")]
        public ActionResult All(long? ReportId)
        {
            var request = new ProjectReportCreateOrDetailsRequest();
            if(ReportId!=null)
            {
                request.ReportId = (long)ReportId;
                request.RequesterRole = "Admin";
                request.RequesterId = Session["UserID"].ToString();
                TempData["Projects"] = reportService.SaveAndGetAllProjectsReport(request).ToList().Select(x => x.CreateForReport());
            }
            
            var projects = TempData["Projects"] as IEnumerable<Project>;
            if (projects == null)
                return RedirectToAction("Index", "ProjectsAndTasks");
            return View(TempData["Projects"] as IEnumerable<Project>);
        }
        [SiteAuthorize(PermissionKey = "GenerateProjectsReport")]
        public ActionResult Create()
        {
            CreateViewModel projectsReportsCreateViewModel=new CreateViewModel
            {
                Projects = projectService.GetAllProjects().ToList().Select(x => x.CreateForDashboardDDL()).ToList()
            };
            return View(projectsReportsCreateViewModel);
        }
        [SiteAuthorize(PermissionKey = "DetailsSingleProjectReport")]
        public ActionResult Details(CreateViewModel projectsReportsCreateViewModel)
        {
            var request = new ProjectReportCreateOrDetailsRequest
            {
                ProjectId = projectsReportsCreateViewModel.ProjectId,
                ReportId = projectsReportsCreateViewModel.ReportId,
                RequesterRole = Session["RoleName"].ToString(),
                RequesterId = Session["UserID"].ToString()
            };
            //Check if request came from "Report Create Page"
            //var refrel = Request.UrlReferrer;
            //if (refrel != null && refrel.ToString().Contains("Report/Project/Create"))
            //    request.IsCreate = true;
            if (projectsReportsCreateViewModel.ReportId==0)
                    request.IsCreate = true;

            if (projectsReportsCreateViewModel.ProjectId == 0 && projectsReportsCreateViewModel.ReportId == 0)
            {
                TempData["Projects"] = reportService.SaveAndGetAllProjectsReport(request).ToList().Select(x => x.CreateForReport());

                return RedirectToAction("All");
            }
              
            DetailVeiwModel detailVeiwModel = new DetailVeiwModel();
            
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

        [AllowAnonymous]
        public ActionResult GeneratePdf(TaskReportsCreateViewModel viewModel)
        {
            //Dictionary<string, string> cookies = (Dictionary<string, string>)Session["Cookies"];
            return new ActionAsPdf("ReportAsPdf", new { ReportId = viewModel.ReportId }) { FileName = "Project_Report.pdf" };
        }

        [AllowAnonymous]
        public ActionResult ReportAsPdf(CreateViewModel projectsReportsCreateViewModel)
        {
            var request = new ProjectReportCreateOrDetailsRequest
            {
                ProjectId = projectsReportsCreateViewModel.ProjectId,
                ReportId = projectsReportsCreateViewModel.ReportId,
                RequesterRole = "Admin",
                RequesterId = Session["UserID"].ToString()
            };
            //Check if request came from "Report Create Page"
            //var refrel = Request.UrlReferrer;
            //if (refrel != null && refrel.ToString().Contains("Report/Project/Create"))
            //    request.IsCreate = true;
            if (projectsReportsCreateViewModel.ReportId == 0)
                request.IsCreate = true;

            if (projectsReportsCreateViewModel.ProjectId == 0 && projectsReportsCreateViewModel.ReportId == 0)
            {
                TempData["Projects"] = reportService.SaveAndGetAllProjectsReport(request).ToList().Select(x => x.CreateForReport());

                return RedirectToAction("All");
            }

            DetailVeiwModel detailVeiwModel = new DetailVeiwModel();

            if (projectsReportsCreateViewModel.ProjectId > 0 || projectsReportsCreateViewModel.ReportId > 0)
            {
                var response = reportService.SaveAndGetProjectReportDetails(request);
                detailVeiwModel.Projects = response.Projects.Select(x => x.CreateForReportDetails()).ToList();
                detailVeiwModel.ProjectTasks = response.ProjectTasks.Select(x => x.CreateForReport()).ToList();
                detailVeiwModel.ReportId = response.ReportId;
            }
            SetGraphData(detailVeiwModel);
            var status = SetGraphImage(detailVeiwModel.ReportId);
            detailVeiwModel.ImageSrc = SetGraphImage(detailVeiwModel.ReportId) != null ? status : "";
            return View(detailVeiwModel);
        }
        private string SetGraphImage(long reportId)
        {
            string curFile = Server.MapPath(ConfigurationManager.AppSettings["ReportImage"]) + "report_" + reportId + ".png";
            if (System.IO.File.Exists(curFile))
            {
                return "../.." + ConfigurationManager.AppSettings["ReportImage"] + "report_" + reportId + ".png";
            }
            return null;
        }
        private static long GetJavascriptTimestamp(DateTime input)
        {
            TimeSpan span = new TimeSpan(DateTime.Parse("1/1/1970").Ticks);
            DateTime time = input.Subtract(span);
            return (time.Ticks / 10000);
        }
        private DetailVeiwModel SetGraphData(DetailVeiwModel detailVeiwModel)
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
                    ItemLabel = System.Threading.Thread.CurrentThread.CurrentCulture.ToString() == "en" ? "Task_" + projectTask.TaskNameE : projectTask.TaskNameA,
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
    }
}