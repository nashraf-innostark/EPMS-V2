using System;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.DomainModels;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.Complaint;
using Microsoft.AspNet.Identity;

namespace EPMS.Web.Areas.CMS.Controllers
{
    public class ComplaintController : BaseController
    {
        #region Constructor and Services
        private readonly IComplaintService complaintService;
        private readonly IAspNetUserService userService;
        private readonly IDepartmentService departmentService;

        public ComplaintController(IComplaintService complaintService, IAspNetUserService userService, IDepartmentService departmentService)
        {
            this.complaintService = complaintService;
            this.userService = userService;
            this.departmentService = departmentService;
        }

        #endregion
        // GET: CMS/Complaint
        public ActionResult Index()
        {
            return View();
        }
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
                }
                requestViewModel.Departments = departmentService.GetAll().Select(x => x.CreateFrom());
            }
            return View(requestViewModel);
        }
        [HttpPost]
        public ActionResult Create()
        {
            return View();
        }
    }
}