using System;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.DomainModels;
using EPMS.Models.ResponseModels;
using EPMS.Web.Controllers;
using EPMS.WebBase.Mvc;
using EPMS.WebModels.ModelMappers;
using EPMS.WebModels.ViewModels.Common;
using EPMS.WebModels.ViewModels.Complaint;
using Microsoft.AspNet.Identity;

namespace EPMS.Web.Areas.CMS.Controllers
{
    [Authorize]
    [SiteAuthorize(PermissionKey = "CS", IsModule = true)]
    public class ComplaintController : BaseController
    {
        #region Constructor and Services
        private readonly IComplaintService complaintService;
        private readonly IAspNetUserService userService;
        private readonly IDepartmentService departmentService;
        private readonly IOrdersService ordersService;

        public ComplaintController(IComplaintService complaintService, IAspNetUserService userService, IDepartmentService departmentService,IOrdersService ordersService)
        {
            this.complaintService = complaintService;
            this.userService = userService;
            this.departmentService = departmentService;
            this.ordersService = ordersService;
        }

        #endregion

        #region Index
        // GET: CMS/Complaint
        [SiteAuthorize(PermissionKey = "ComplaintIndex")]
        public ActionResult Index()
        {
            ViewBag.MessageVM = TempData["MessageVm"] as MessageViewModel;
            AspNetUser currentUser = userService.FindById(User.Identity.GetUserId());
            ComplaintViewModel viewModel= new ComplaintViewModel
            {
                Complaints =
                    currentUser.AspNetRoles.FirstOrDefault().Name == "Customer"
                        ? complaintService.LoadAllComplaintsByCustomerId(Convert.ToInt64(currentUser.CustomerId))
                            .Select(x => x.CreateFromServerToClient())
                        : complaintService.LoadAllComplaints()
                            .OrderByDescending(x => x.ComplaintDate)
                            .Select(x => x.CreateFromServerToClient())
            };
            object userPermissionSet = System.Web.HttpContext.Current.Session["UserPermissionSet"];
            string[] userPermissionsSet = (string[])userPermissionSet;
            if (userPermissionsSet.Contains("ComplaintCreate"))
                ViewBag.CanCreate = true;
            else
                ViewBag.CanCreate = false;
            return View(viewModel);
        }
        #endregion
        
        #region Create
        [SiteAuthorize(PermissionKey = "ComplaintCreate,ComplaintDetails")]
        public ActionResult Create(long? id)
        {
            ComplaintViewModel requestViewModel = new ComplaintViewModel();
            ComplaintResponse response = new ComplaintResponse();
            var userId = User.Identity.GetUserId();
            if (userId == null)
                RedirectToAction("Login", "Account");
            AspNetUser currentUser = userService.FindById(User.Identity.GetUserId());
            ViewBag.UserRole = currentUser.AspNetRoles.FirstOrDefault().Name;
            if (User.Identity.GetUserId() != null)
            {
                if (id != null)
                {
                    response = complaintService.GetComplaintResponse((long)id, 0,Session["RoleName"].ToString());
                    if (response.Complaint != null)
                    {
                        requestViewModel.Complaint = response.Complaint.CreateFromServerToClient();

                        requestViewModel.Orders = response.Orders.Where(x => x.OrderId.Equals(requestViewModel.Complaint.OrderId)).Select(x => x.CreateFromServerToClient());
                        requestViewModel.Departments = response.Departments.Where(x => x.DepartmentId == requestViewModel.Complaint.DepartmentId).Select(x => x.CreateFrom());
                        requestViewModel.DeptId = requestViewModel.Complaint.DepartmentId;
                        requestViewModel.OdrId = requestViewModel.Complaint.OrderId;
                        if (!requestViewModel.Complaint.IsReplied)
                            ViewBag.MessageVM = new MessageViewModel { Message = EPMS.WebModels.Resources.CMS.Complaint.NotReplyInfoMsg, IsInfo = true };
                    }
                }
                else if (Session["RoleName"].ToString()=="Customer")
                {
                    response = complaintService.GetComplaintResponse(0, (long)currentUser.CustomerId, Session["RoleName"].ToString());
                    requestViewModel.Complaint.ComplaintDateString = DateTime.Now.ToShortDateString();
                    requestViewModel.Complaint.ClientName = currentUser.Customer.CustomerNameE;
                    requestViewModel.Complaint.CustomerId = Convert.ToInt64(currentUser.CustomerId);
                    requestViewModel.Departments = response.Departments.Select(x => x.CreateFrom());
                    requestViewModel.Orders = response.Orders.Select(x => x.CreateFromServerToClient());
                }
                else
                {
                    return RedirectToAction("Index", "UnauthorizedRequest", new { area = "" });
                }
            }
            return View(requestViewModel);
        }
        [HttpPost]
        [ValidateInput(false)]//this is due to CK Editor
        [SiteAuthorize(PermissionKey = "ComplaintCreate")]
        public ActionResult Create(ComplaintViewModel viewModel)
        {
            try
            {
                if (viewModel.Complaint.ComplaintId > 0)//Update
                {
                    viewModel.Complaint.RecLastUpdatedBy = User.Identity.GetUserId();
                    viewModel.Complaint.RecLastUpdatedDt = DateTime.Now;
                    viewModel.Complaint.Description = viewModel.Complaint.ComplaintDesc;
                    viewModel.Complaint.IsReplied = true;
                    viewModel.Complaint.DepartmentId = viewModel.DeptId;
                    viewModel.Complaint.OrderId = viewModel.OdrId;
                    //Update Complaint to Db
                    complaintService.UpdateComplaint(viewModel.Complaint.CreateFromClientToServer());
                    TempData["message"] = new MessageViewModel
                    {
                        Message = EPMS.WebModels.Resources.CMS.Complaint.ComplaintRepliedMsg,
                        IsUpdated = true
                    };
                }
                else//New
                {
                    viewModel.Complaint.RecCreatedBy = User.Identity.GetUserId();
                    viewModel.Complaint.RecCreatedDt = DateTime.Now;
                    viewModel.Complaint.RecLastUpdatedBy = User.Identity.GetUserId();
                    viewModel.Complaint.RecLastUpdatedDt = DateTime.Now;
                    viewModel.Complaint.ComplaintDate = DateTime.Now;
                    //Add Complaint to Db
                    complaintService.AddComplaint(viewModel.Complaint.CreateFromClientToServer());
                    TempData["message"] = new MessageViewModel
                    {
                        Message = EPMS.WebModels.Resources.CMS.Complaint.ComplaintCreatedMsg,
                        IsUpdated = true
                    };
                }
            }
            catch (Exception e)
            {

            }
            return RedirectToAction("Index", "Complaint");
        }
        #endregion
    }
}