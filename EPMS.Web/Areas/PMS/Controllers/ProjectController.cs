using System;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers;
using EPMS.Web.ModelMappers.PMS;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.Project;
using EPMS.WebBase.Mvc;

namespace EPMS.Web.Areas.PMS.Controllers
{
    [Authorize]
    public class ProjectController : BaseController
    {
        private readonly IProjectService projectService;
        private readonly ICustomerService customerService;
        private readonly IOrdersService ordersService;

        public ProjectController(IProjectService projectService,ICustomerService customerService,IOrdersService ordersService)
        {
            this.projectService = projectService;
            this.customerService = customerService;
            this.ordersService = ordersService;
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
        public ActionResult Create(long? projectId)
        {
            ProjectViewModel projectViewModel = new ProjectViewModel();
            ViewBag.UserRole = Session["RoleName"].ToString();
            var customers = customerService.GetAll();
            var orders = ordersService.GetAll();
            if (projectId != null)
            {
                var project = projectService.FindProjectById((long)projectId);
                if (project != null)
                {
                    projectViewModel.Project = project.CreateFromServerToClient();
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
        //[HttpPost]
        //[ValidateInput(false)]//this is due to CK Editor
        //public ActionResult Create(ComplaintViewModel viewModel)
        //{
        //    try
        //    {
        //        if (viewModel.Complaint.ComplaintId > 0)//Update
        //        {
        //            viewModel.Complaint.RecLastUpdatedBy = User.Identity.GetUserId();
        //            viewModel.Complaint.RecLastUpdatedDt = DateTime.Now;
        //            viewModel.Complaint.Description = viewModel.Complaint.ComplaintDesc;
        //            viewModel.Complaint.IsReplied = true;
        //            //Update Complaint to Db
        //            complaintService.UpdateComplaint(viewModel.Complaint.CreateFromClientToServer());
        //            TempData["message"] = new MessageViewModel
        //            {
        //                Message = Resources.CMS.Complaint.ComplaintRepliedMsg,
        //                IsUpdated = true
        //            };
        //        }
        //        else//New
        //        {
        //            viewModel.Complaint.RecCreatedBy = User.Identity.GetUserId();
        //            viewModel.Complaint.RecCreatedDt = DateTime.Now;
        //            viewModel.Complaint.RecLastUpdatedBy = User.Identity.GetUserId();
        //            viewModel.Complaint.RecLastUpdatedDt = DateTime.Now;

        //            //Add Complaint to Db
        //            complaintService.AddComplaint(viewModel.Complaint.CreateFromClientToServer());
        //            TempData["message"] = new MessageViewModel
        //            {
        //                Message = Resources.CMS.Complaint.ComplaintCreatedMsg,
        //                IsUpdated = true
        //            };
        //        }
        //    }
        //    catch (Exception e)
        //    {

        //    }
        //    return RedirectToAction("Index", "Complaint");
        //}
        #endregion
    }
}