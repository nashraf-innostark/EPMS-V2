using System;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.DomainModels;
using EPMS.Web.Controllers;
using EPMS.Web.ViewModels.Complaint;
using Microsoft.AspNet.Identity;

namespace EPMS.Web.Areas.CMS.Controllers
{
    public class ComplaintController : BaseController
    {
        #region Constructor and Services
        private readonly IComplaintService complaintService;
        private readonly IAspNetUserService userService;
        public ComplaintController(IComplaintService complaintService, IAspNetUserService userService)
        {
            this.complaintService = complaintService;
            this.userService = userService;
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
            //if (User.Identity.GetUserId() != null)
            //{
            //    if (id != null)
            //    {
            //        var complaint = complaintService.FindComplaintById((long)id);
            //        //Check if current request is related to logedin user.
            //        if (request.EmployeeId == currentUser.EmployeeId || ViewBag.UserRole == "Admin")
            //        {
            //            requestViewModel.Request = request.CreateFromServerToClient();
            //            requestViewModel.RequestDetail = request.RequestDetails.FirstOrDefault().CreateFromServerToClient();
            //            requestViewModel.RequestDesc = requestViewModel.RequestDetail.RequestDesc;
            //            if (request.RequestDetails.Count > 1)
            //                requestViewModel.RequestReply = request.RequestDetails.Single(x => x.RowVersion == 1).CreateFromServerToClient();
            //        }
            //    }
            //    else if (currentUser.EmployeeId > 0)
            //    {
            //        requestViewModel.Request.RequestDate = DateTime.Now;
            //        requestViewModel.Request.EmployeeId = Convert.ToInt64(currentUser.EmployeeId);
            //        requestViewModel.Request.EmployeeNameA = currentUser.Employee.EmployeeNameA;
            //        requestViewModel.Request.EmployeeNameE = currentUser.Employee.EmployeeNameE;
            //        requestViewModel.Request.DepartmentNameA = currentUser.Employee.JobTitle.Department.DepartmentNameA;
            //        requestViewModel.Request.DepartmentNameE = currentUser.Employee.JobTitle.Department.DepartmentNameE;
            //    }
            //}
            //if (requestViewModel.RequestDetail.IsApproved)
            //    ViewBag.MessageVM = new MessageViewModel { Message = Resources.HR.Request.RequestAccepted, IsInfo = true };
            return View(requestViewModel);
        }
        [HttpPost]
        public ActionResult Create()
        {
            return View();
        }
    }
}