using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.DomainModels;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers;
using EPMS.Web.ModelMappers.PMS;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.Project;
using EPMS.WebBase.Mvc;
using Microsoft.AspNet.Identity;

namespace EPMS.Web.Areas.PMS.Controllers
{
    [Authorize]
    public class ProjectController : BaseController
    {
        private readonly IQuotationService quotationService;
        private readonly IProjectTaskService projectTaskService;
        private readonly IProjectService projectService;
        private readonly ICustomerService customerService;
        private readonly IOrdersService ordersService;
        private readonly IProjectDocumentService projectDocumentService;

        public ProjectController(IQuotationService quotationService,IProjectTaskService projectTaskService,IProjectService projectService,ICustomerService customerService,IOrdersService ordersService,IProjectDocumentService projectDocumentService)
        {
            this.quotationService = quotationService;
            this.projectTaskService = projectTaskService;
            this.projectService = projectService;
            this.customerService = customerService;
            this.ordersService = ordersService;
            this.projectDocumentService = projectDocumentService;
        }

        #region Loading Lists
        // GET: PMS/Project/OnGoing
        [SiteAuthorize(PermissionKey = "ProjectIndex")]
        public ActionResult OnGoing()
        {
            ProjectListViewModel projectsList=new ProjectListViewModel
            {
                Projects =
                    Session["RoleName"].ToString() == "Customer"
                        ? projectService.LoadAllOnGoingProjectsByCustomerId(
                            Convert.ToInt64(Session["CustomerID"].ToString())).Select(x => x.CreateFromServerToClient())
                        : projectService.LoadAllOnGoingProjects().Select(x => x.CreateFromServerToClient())
            };
            return View(projectsList);
        }
        // GET: PMS/Project/Finished
        [SiteAuthorize(PermissionKey = "ProjectIndex")]
        public ActionResult Finished()
        {
            ProjectListViewModel projectsList = new ProjectListViewModel
            {
                Projects =
                    Session["RoleName"].ToString() == "Customer"
                        ? projectService.LoadAllFinishedProjectsByCustomerId(
                            Convert.ToInt64(Session["CustomerID"].ToString())).Select(x => x.CreateFromServerToClient())
                        : projectService.LoadAllFinishedProjects().Select(x => x.CreateFromServerToClient())
            };
            return View(projectsList);
        }
        #endregion

        #region Create
        [SiteAuthorize(PermissionKey = "ProjectCreate")]
        public ActionResult Create(long? id)
        {
            ProjectViewModel projectViewModel = new ProjectViewModel();
            ViewBag.UserRole = Session["RoleName"].ToString();
            var customers = customerService.GetAll();
            var orders = ordersService.GetAll();
            if (id != null)
            {
                var project = projectService.FindProjectById((long)id);
                if (project != null)
                {
                    projectViewModel.Project = project.CreateFromServerToClient();
                    //Map Installments
                    if (project.OrderId > 0)
                    {
                        var quotation = quotationService.FindQuotationByOrderId(Convert.ToInt64(project.OrderId));
                        if (quotation != null)
                            projectViewModel.Installment = quotation.CreateFromServerToClientLv();
                    }
                    //Map Project Tasks
                    var projectTasks = projectTaskService.GetTasksByProjectId((long)id);
                    if (projectTasks != null)
                    {
                        projectViewModel.ProjectTasks = projectTasks.Select(x => x.CreateFromServerToClient());
                        foreach (var projectTask in projectViewModel.ProjectTasks)
                        {
                            projectViewModel.Project.TotalTasksCost += projectTask.TotalCost;
                            projectViewModel.Project.ProgressTotal += Convert.ToDouble(projectTask.TaskProgress);
                        }
                    }
                    projectViewModel.Project.Profit = (projectViewModel.Project.Price +
                                                       projectViewModel.Project.OtherCost) -
                                                      projectViewModel.Project.TotalTasksCost;
                }
            }
            projectViewModel.Customers = customers.Select(x => x.CreateFromServerToClient());
            projectViewModel.Orders = orders.Select(x => x.CreateFromServerToClient());
            //else
            //{
            //    return RedirectToAction("Index", "UnauthorizedRequest", new { area = "" });
            //}
            //ViewBag.MessageVM = new MessageViewModel { Message = Resources.CMS.Complaint.NotReplyInfoMsg, IsInfo = true };
            return View(projectViewModel);
        }
        [HttpPost]
        [ValidateInput(false)]//this is due to CK Editor
        public ActionResult Create(ProjectViewModel projectViewModel)
        {
            try
            {
                if (projectViewModel.Project.ProjectId > 0)//Update
                {
                    projectViewModel.Project.RecLastUpdatedBy = User.Identity.GetUserId();
                    projectViewModel.Project.RecLastUpdatedDate = DateTime.Now;
                    //Update Project to Db
                    projectViewModel.Project.ProjectId = projectService.UpdateProject(projectViewModel.Project.CreateFromClientToServer());
                    SaveProjectDocuments(projectViewModel);
                    TempData["message"] = new MessageViewModel
                    {
                        Message = Resources.PMS.Project.ProjectUpdatedMsg,
                        IsUpdated = true
                    };
                }
                else//New
                {
                    projectViewModel.Project.RecCreatedBy = User.Identity.GetUserId();
                    projectViewModel.Project.RecCreatedDate = DateTime.Now;
                    projectViewModel.Project.RecLastUpdatedBy = User.Identity.GetUserId();
                    projectViewModel.Project.RecLastUpdatedDate = DateTime.Now;

                    //Add Project to Db
                    projectViewModel.Project.ProjectId=projectService.AddProject(projectViewModel.Project.CreateFromClientToServer());

                    SaveProjectDocuments(projectViewModel);
                    

                    TempData["message"] = new MessageViewModel
                    {
                        Message = Resources.PMS.Project.ProjectCreatedMsg,
                        IsSaved = true
                    };
                }
            }
            catch (Exception e)
            {
                return View(projectViewModel);
            }
            return RedirectToAction("OnGoing", "Project");
        }

        public void SaveProjectDocuments(ProjectViewModel projectViewModel)
        {
            //Save file names in db
            if (!string.IsNullOrEmpty(projectViewModel.DocsNames))
            {
                var projectDocuments = projectViewModel.DocsNames.Substring(0, projectViewModel.DocsNames.Length - 2).Split('~').ToList();
                foreach (var projectDocument in projectDocuments)
                {
                    ProjectDocument doc = new ProjectDocument();
                    doc.FileName = projectDocument;
                    doc.ProjectId = projectViewModel.Project.ProjectId;
                    projectDocumentService.AddProjectDocument(doc);
                }
            }
        }
        public ActionResult UploadDocuments()
        {
            HttpPostedFileBase doc = Request.Files[0];
            var filename = "";
            try
            {
                //Save File to Folder
                if ((doc != null))
                {
                    filename = (DateTime.Now.ToString(CultureInfo.InvariantCulture) + doc.FileName);//concat date time with file name
                    Regex pattern = new Regex("[;|:|,|-|_|+|/| ]");
                    filename = pattern.Replace(filename, "");//remove some characters and spaces from file name
                    var filePathOriginal = Server.MapPath(ConfigurationManager.AppSettings["ProjectDocuments"]);
                    string savedFileName = Path.Combine(filePathOriginal, filename);
                    doc.SaveAs(savedFileName);
                }
            }
            catch (Exception exp)
            {
                return Json(new { response = "Failed to upload. Error: " + exp.Message, status = (int)HttpStatusCode.BadRequest }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { filename = filename, size = doc.ContentLength / 1024 + "KB", response = "Successfully uploaded!", status = (int)HttpStatusCode.OK }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Detail
        [SiteAuthorize(PermissionKey = "ProjectCreate")]
        public ActionResult Details(long? id)
        {
            
            if (id != null)
            {
                var project = projectService.FindProjectById((long)id);
                if (project != null)
                {
                    ProjectViewModel projectViewModel = new ProjectViewModel();
                    ViewBag.UserRole = Session["RoleName"].ToString();
                    var customers = customerService.GetAll();
                    var orders = ordersService.GetAll();
                    projectViewModel.Project = project.CreateFromServerToClient();
                    //Map Installments
                    if (project.OrderId > 0)
                    {
                        var quotation = quotationService.FindQuotationByOrderId(Convert.ToInt64(project.OrderId));
                        if (quotation!=null)
                            projectViewModel.Installment = quotation.CreateFromServerToClientLv();
                    }
                    //Map Project Tasks
                    var projectTasks = projectTaskService.GetTasksByProjectId((long)id);
                    if (projectTasks != null)
                    {
                        projectViewModel.ProjectTasks = projectTasks.Select(x => x.CreateFromServerToClient());
                        foreach (var projectTask in projectViewModel.ProjectTasks)
                        {
                            projectViewModel.Project.TotalTasksCost += projectTask.TotalCost;
                            projectViewModel.Project.ProgressTotal += Convert.ToDouble(projectTask.TaskProgress);
                        }
                    }
                    projectViewModel.Project.Profit = (projectViewModel.Project.Price +
                                                       projectViewModel.Project.OtherCost) -
                                                      projectViewModel.Project.TotalTasksCost;
                    var projectDocument = projectDocumentService.FindProjectDocumentsByProjectId((long)id);
                    var projectDocsNames = projectDocument as IList<ProjectDocument> ?? projectDocument.ToList();
                    if (projectDocsNames.Any())
                    {
                        projectViewModel.ProjectDocsNames = projectDocsNames;
                    }
                    projectViewModel.Customers = customers.Select(x => x.CreateFromServerToClient());
                    projectViewModel.Orders = orders.Select(x => x.CreateFromServerToClient());
                    return View(projectViewModel);
                }
            }
            return RedirectToAction("Index", "UnauthorizedRequest", new { area = "" });
        }
        public FileResult Download(string fileName)
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath(ConfigurationManager.AppSettings["ProjectDocuments"] + fileName));
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
        #endregion

        #region Get Customer Orders
        [HttpGet]
        public JsonResult GetCustomerOrders(long customerId)
        {
            var orders = ordersService.GetOrdersByCustomerId(customerId).Select(x => x.CreateFromServerToClient());
            return Json(orders, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}