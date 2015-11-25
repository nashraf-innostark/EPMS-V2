using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.RequestModels.Reports;
using EPMS.Models.ResponseModels.ReportsResponseModels;
using EPMS.Web.Controllers;
using EPMS.WebBase.Mvc;
using EPMS.WebModels.ModelMappers;
using EPMS.WebModels.ModelMappers.PMS;
using EPMS.WebModels.ViewModels.Reports;
using EPMS.WebModels.WebsiteModels;
using Rotativa;

namespace EPMS.Web.Areas.Report.Controllers
{
    public class TaskController : BaseController
    {
        private readonly IProjectTaskService taskService;
        private readonly IReportService reportService;
        private TaskReportDetailsResponse GetReportDetails(TaskReportsCreateViewModel viewModel)
        {
            var request = new TaskReportCreateOrDetailsRequest
            {
                ProjectId = viewModel.ProjectId,
                TaskId = viewModel.TaskId,
                ReportId = viewModel.ReportId,
                RequesterId = Session["UserID"].ToString()
            };
            if (viewModel.ReportId == 0)
                request.IsCreate = true;
            TaskReportDetailsResponse response = reportService.SaveAndGetTaskReportDetails(request);
            return response;
        }

        public TaskController(IProjectTaskService taskService, IReportService reportService)
        {
            this.taskService = taskService;
            this.reportService = reportService;
        }

        // GET: Report/Task
        [SiteAuthorize(PermissionKey = "CreateTaskReport")]
        public ActionResult Create()
        {
            var response = taskService.GetAllProjectsAndTasks();
            TaskReportsCreateViewModel model = new TaskReportsCreateViewModel
            {
                Tasks = response.Tasks.Select(x => x.CreateFoDropDown()).ToList(),
                Projects = response.Projects.Select(x=>x.CreateForDashboardDDL()).ToList()
            };
            return View(model);
        }

        [SiteAuthorize(PermissionKey = "ViewTaskReport")]
        public ActionResult Details(TaskReportsCreateViewModel viewModel)
        {
            TaskReportDetailsResponse response = GetReportDetails(viewModel);
            TaskReportDetailVeiwModel detailViewModel = new TaskReportDetailVeiwModel
            {
                ReportId = viewModel.ReportId != 0 ? viewModel.ReportId : response.ReportId,
                ProjectTasks = response.ProjectTasks.Select(x => x.CreateFromServerToClientLv()).ToList(),
                SubTasks = response.SubTasks.Select(x => x.CreateFromServerToClientLv()).ToList()
            };
            SetGraphData(detailViewModel);
            ViewBag.QueryString = "?ReportId=" + detailViewModel.ReportId;
            if ((viewModel.ProjectId == 0 && viewModel.TaskId == 0 && viewModel.ReportId == 0))
            {
                return RedirectToAction("All", new {ReportId = detailViewModel.ReportId});
            }
            
            return View(detailViewModel);
        }
        [AllowAnonymous]
        public ActionResult GeneratePdf(TaskReportsCreateViewModel viewModel)
        {
            //Dictionary<string, string> cookies = (Dictionary<string, string>)Session["Cookies"];
            return new ActionAsPdf("ReportAsPdf", new { ReportId = viewModel.ReportId }) { FileName = "Task_Report.pdf" };
        }
        [AllowAnonymous]
        public ActionResult ReportAsPdf(TaskReportsCreateViewModel viewModel)
        {
            TaskReportDetailsResponse response = GetReportDetails(viewModel);
            TaskReportDetailVeiwModel detailViewModel = new TaskReportDetailVeiwModel
            {
                ReportId = viewModel.ReportId,
                ProjectTasks = response.ProjectTasks.Select(x => x.CreateFromServerToClientLv()).ToList(),
                SubTasks = response.SubTasks.Select(x => x.CreateFromServerToClientLv()).ToList()
            };
            SetGraphData(detailViewModel);
            var status = SetGraphImage(detailViewModel.ReportId);
            detailViewModel.ImageSrc = SetGraphImage(detailViewModel.ReportId) != null ? status : "" ;
            return View(detailViewModel);
            //return new RazorPDF.PdfResult(detailViewModel, "ReportAsPdf");
        }

        [AllowAnonymous]
        public ActionResult GeneratePdfAll(long? ReportId)
        {
            //Dictionary<string, string> cookies = (Dictionary<string, string>)Session["Cookies"];
            return new ActionAsPdf("ReportAsPdfAll", new { ReportId = ReportId }) { FileName = "Task_Report.pdf" };
        }

        [AllowAnonymous]
        public ActionResult ReportAsPdfAll(long? ReportId)
        {
            IEnumerable<ProjectTask> tasksList = new List<ProjectTask>();
            var request = new TaskReportCreateOrDetailsRequest();
            if (ReportId != null)
            {
                request.ReportId = (long)ReportId;
                var response = reportService.SaveAndGetTaskReportDetails(request);
                tasksList = response.ProjectTasks.Any() ? response.ProjectTasks.Select(x => x.CreateFromServerToClientLv()) : new List<ProjectTask>();
            }
            return View(tasksList);
        }

        private static long GetJavascriptTimestamp(DateTime input)
        {
            TimeSpan span = new TimeSpan(DateTime.Parse("1/1/1970").Ticks);
            DateTime time = input.Subtract(span);
            return (time.Ticks / 10000);
        }
        private TaskReportDetailVeiwModel SetGraphData(TaskReportDetailVeiwModel detailVeiwModel)
        {
            var task = detailVeiwModel.ProjectTasks.FirstOrDefault();
            if (task != null)
            {
                detailVeiwModel.GrpahStartTimeStamp = GetJavascriptTimestamp(DateTime.ParseExact(task.StartDate.ToString(), "dd/MM/yyyy", new CultureInfo("en")));
                detailVeiwModel.GrpahEndTimeStamp = GetJavascriptTimestamp(DateTime.ParseExact(task.EndDate.ToString(), "dd/MM/yyyy", new CultureInfo("en")));
                
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
                                            Value = task.TotalCost
                                        },
                                        new GraphLabelDataValues
                                        {
                                            TimeStamp = detailVeiwModel.GrpahEndTimeStamp,
                                            Value = task.TotalCost
                                        }
                                    }
                                }
                            }
                        }
                    }
                });
            }

            foreach (var projectTask in detailVeiwModel.SubTasks)
            {
                detailVeiwModel.GraphItems.Add(new GraphItem
                {
                    ItemLabel = System.Threading.Thread.CurrentThread.CurrentCulture.ToString() == "en" ? "SubTask_" + projectTask.TaskNameE : projectTask.TaskNameA,
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

        private string SetGraphImage(long reportId)
        {
            string curFile = Server.MapPath(ConfigurationManager.AppSettings["ReportImage"]) + "report_" + reportId + ".png";
            if (System.IO.File.Exists(curFile))
            {
                return "../.." + ConfigurationManager.AppSettings["ReportImage"] + "report_" + reportId + ".png";
            }
            return null;
        }

        public ActionResult All(long? ReportId)
        {
            IEnumerable<ProjectTask> tasksList = new List<ProjectTask>();
            var request = new TaskReportCreateOrDetailsRequest();
            if (ReportId != null)
            {
                request.ReportId = (long)ReportId;
                request.RequesterId = Session["UserID"].ToString();
                request.RoleId = Session["RoleId"].ToString();
                var response = reportService.SaveAndGetTaskReportDetails(request);
                tasksList = response.ProjectTasks.Any() ? response.ProjectTasks.Select(x => x.CreateFromServerToClientLv()) : new List<ProjectTask>();
            }
            ViewBag.ReportId = ReportId;
            return View(tasksList);
        }

        public ActionResult TestTable()
        {
            return View();
        }
    }
}