using System;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.DomainModels;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.Complaint;
using EPMS.WebBase.Mvc;
using Microsoft.AspNet.Identity;

namespace EPMS.Web.Areas.CMS.Controllers
{
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
        // GET: CMS/Complaint
        [SiteAuthorize(PermissionKey = "ComplaintIndex")]
        public ActionResult Index()
        {
            return View();
        }
        [SiteAuthorize(PermissionKey = "ComplaintCreate")]
        public ActionResult Create(long? id)
        {
            ComplaintViewModel requestViewModel = new ComplaintViewModel();
            var userId = User.Identity.GetUserId();
            if (userId == null)
                RedirectToAction("Login", "Account");
            AspNetUser currentUser = userService.FindById(User.Identity.GetUserId());
            ViewBag.UserRole = currentUser.AspNetRoles.FirstOrDefault().Name;
            if (User.Identity.GetUserId() != null)
            {
                if (id != null)
                {
                    var complaint = complaintService.FindComplaintById((long)id);
                    //Check if current request is related to logedin user.
                    if (complaint!=null)
                    {
                        requestViewModel.Complaint = complaint.CreateFromServerToClient();
                    }
                }
                else
                {
                    requestViewModel.Complaint.ComplaintDate = DateTime.Now;
                    if (ViewBag.UserRole != "Admin")
                    {
                        requestViewModel.Complaint.ClientName = currentUser.Customer.CustomerName;
                        requestViewModel.Complaint.CustomerId = Convert.ToInt64(currentUser.CustomerId);
                        requestViewModel.Orders = ordersService.GetOrdersByCustomerId(Convert.ToInt64(currentUser.CustomerId)).Select(x => x.CreateFromServerToClient());
                    }
                }
                requestViewModel.Departments = departmentService.GetAll().Select(x => x.CreateFrom());
            }
            return View(requestViewModel);
        }
        [HttpPost]
        [ValidateInput(false)]//this is due to CK Editor
        public ActionResult Create(ComplaintViewModel viewModel)
        {
            try
            {
                if (viewModel.Complaint.ComplaintId > 0)//Update
                {
                    
                }
                else//New
                {
                    viewModel.Complaint.RecCreatedBy = User.Identity.GetUserId();
                    viewModel.Complaint.RecCreatedDt = DateTime.Now;
                    viewModel.Complaint.RecLastUpdatedBy = User.Identity.GetUserId();
                    viewModel.Complaint.RecLastUpdatedDt = DateTime.Now;

                    //Add Request to Db, and get RequestId
                    complaintService.AddComplaint(viewModel.Complaint.CreateFromClientToServer());
                }
            }
            catch (Exception e)
            {
                
            }
            return RedirectToAction("Index", "Complaint");
        }
    }
}