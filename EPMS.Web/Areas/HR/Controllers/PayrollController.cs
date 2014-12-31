using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPMS.Implementation.Identity;
using EPMS.Implementation.Services;
using EPMS.Interfaces.IServices;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Web.ViewModels.Payroll;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using EPMS.Web.ModelMappers;

namespace EPMS.Web.Areas.HR.Controllers
{
    public class PayrollController : Controller
    {
        private readonly IEmployeeRequestService EmployeeRequestService;
        private readonly IEmployeeService EmployeeService;
        private readonly IAspNetUserService AspNetUserService;

        #region Constructor

        public PayrollController(IEmployeeRequestService employeeRequestService, IEmployeeService employeeService, IAspNetUserService aspNetUserService)
        {
            EmployeeRequestService = employeeRequestService;
            EmployeeService = employeeService;
            AspNetUserService = aspNetUserService;
        }

        #endregion

        #region Public

        // GET: HR/Payroll
        public ActionResult Index()
        {
            AspNetUser result = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(User.Identity.GetUserId());
            var userRole = result.AspNetRoles.FirstOrDefault();
            if (userRole != null && userRole.Name == "Admin")
            {
                PayrollSearchRequest payrollSearchRequest = new PayrollSearchRequest();
                PayrollViewModel viewModel = new PayrollViewModel
                {
                    SearchRequest = payrollSearchRequest
                };
                return View(viewModel);
            }
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        [HttpPost]
        public ActionResult Index(PayrollSearchRequest searchRequest)
        {
            searchRequest.UserId = Guid.Parse(User.Identity.GetUserId());
            AspNetUser result = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(User.Identity.GetUserId());
            var userRole = result.AspNetRoles.FirstOrDefault();
            if (userRole != null && userRole.Name == "Admin")
            {
                searchRequest.Requester = "Admin";
            }
            else
            {
                searchRequest.Requester = AspNetUserService.FindById(User.Identity.GetUserId()).EmployeeId.ToString();
            }
            var employeeRequestResponse = EmployeeRequestService.LoadAllMonetaryRequests();
            var requests = employeeRequestResponse.Select(x => x.CreateFromServerToClientPayroll());
            PayrollViewModel viewModel = new PayrollViewModel
            {
                SearchRequest = searchRequest,
                aaData = requests
            };
            return View(viewModel);
        }

        #endregion
    }
}