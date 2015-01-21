﻿using System;
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
                    if (project.OrderId > 0)
                    {
                        //projectViewModel.Installment = quotationService..Select(x => x.CreateFromServerToClient());
                    }

                    var projectTasks = projectTaskService.GetTasksByProjectId((long)id);
                    if (projectTasks != null)
                    {
                        projectViewModel.ProjectTasks = projectTasks.Select(x => x.CreateFromServerToClient());
                    }
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
                    
                    TempData["message"] = new MessageViewModel
                    {
                        Message = Resources.CMS.Complaint.ComplaintRepliedMsg,
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
                    
                    
                    //Save file names in db
                    if (!string.IsNullOrEmpty(projectViewModel.DocsNames))
                    {
                        var projectDocuments = projectViewModel.DocsNames.Substring(0, projectViewModel.DocsNames.Length - 2).Split('~').ToList();
                        foreach (var projectDocument in projectDocuments)
                        {
                            ProjectDocument doc = new ProjectDocument();
                            doc.FileName = projectDocument;
                            doc.ProjectId = projectViewModel.Project.ProjectId;
                            doc.RecCreatedBy = Session["UserID"].ToString();
                            doc.RecCreatedDate = DateTime.Now;
                            doc.RecLastUpdatedBy = Session["UserID"].ToString();
                            doc.RecLastUpdatedDate = DateTime.Now;
                            projectDocumentService.AddProjectDocument(doc);
                        }
                    }

                    //TempData["message"] = new MessageViewModel
                    //{
                    //    Message = Resources.CMS.Complaint.ComplaintCreatedMsg,
                    //    IsUpdated = true
                    //};
                }
            }
            catch (Exception e)
            {
                return View(projectViewModel);
            }
            return RedirectToAction("OnGoing", "Project");
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