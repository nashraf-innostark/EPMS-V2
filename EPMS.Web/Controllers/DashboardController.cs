using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPMS.Implementation.Identity;
using EPMS.Interfaces.IServices;
using EPMS.Models.DomainModels;
using EPMS.Web.ModelMappers;
using EPMS.Web.ViewModels;
using EPMS.Web.ViewModels.Dashboard;
using EPMS.WebBase.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace EPMS.Web.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly IEmployeeRequestService employeeRequestService;

        public DashboardController(IEmployeeRequestService employeeRequestService)
        {
            this.employeeRequestService = employeeRequestService;
        }

        // GET: Dashboard
        public ActionResult Index()
        {
            AspNetUser user = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(User.Identity.GetUserId());
            var employeeId = user.EmployeeId;
            var userRole = user.AspNetRoles.FirstOrDefault(x => x.Name.Equals("Admin"));
            var requester = userRole != null && userRole.Name == "Admin" ? "Admin" : employeeId.ToString();
            DashboardViewModel dashboardViewModel=new DashboardViewModel();

            dashboardViewModel.EmployeeRequests = GetEmployeeRequests(requester);
            return View(dashboardViewModel);
        }

        private IEnumerable<EmployeeRequestsViewModel> GetEmployeeRequests(string requester)
        {
            return employeeRequestService.LoadAllRequests(requester).Select(x => x.CreateForDashboard());
        }

        [HttpGet]
        [SiteAuthorize(PermissionKey = "RequestIndex")]
        public JsonResult GetEmployeeRequests(long? employeeId)
        {
            var employeeRequests = GetEmployeeRequests(employeeId);
            return Json(employeeRequests, JsonRequestBehavior.AllowGet);
        }
    }
}