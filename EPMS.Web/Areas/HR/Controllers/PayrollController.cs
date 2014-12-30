using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPMS.Implementation.Identity;
using EPMS.Interfaces.IServices;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Web.ViewModels.Payroll;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace EPMS.Web.Areas.HR.Controllers
{
    public class PayrollController : Controller
    {
        private readonly IEmployeeRequestService EmployeeRequestService;
        private readonly IEmployeeService EmployeeService;

        #region Constructor

        public PayrollController(IEmployeeRequestService employeeRequestService, IEmployeeService employeeService)
        {
            EmployeeRequestService = employeeRequestService;
            EmployeeService = employeeService;
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
        public ActionResult Index(PayrollSearchRequest payrollSearchRequest)
        {
            payrollSearchRequest.UserId = Guid.Parse(User.Identity.GetUserId());
            var requests = EmployeeRequestService.LoadAllRequests("Admin");
            PayrollViewModel viewModel = new PayrollViewModel
            {
                SearchRequest = payrollSearchRequest
            };
            return View(viewModel);
        }

        #endregion
    }
}