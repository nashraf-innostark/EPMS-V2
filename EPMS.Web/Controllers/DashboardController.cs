using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Web.ModelMappers;
using EPMS.Web.ViewModels.Dashboard;
using EPMS.WebBase.Mvc;

namespace EPMS.Web.Controllers
{
    [Authorize]
    public class DashboardController : BaseController
    {
        #region Constructor and Private Services objects
        private readonly IEmployeeRequestService employeeRequestService;
        private readonly IEmployeeService employeeService;
        private readonly ICustomerService customerService;
        private readonly IComplaintService complaintService;

        /// <summary>
        /// Dashboard constructor
        /// </summary>
        /// <param name="employeeRequestService"></param>
        /// <param name="employeeService"></param>
        /// <param name="customerService"></param>
        /// <param name="complaintService"></param>
        public DashboardController(IEmployeeRequestService employeeRequestService, IEmployeeService employeeService, ICustomerService customerService, IComplaintService complaintService)
        {
            this.employeeRequestService = employeeRequestService;
            this.employeeService = employeeService;
            this.customerService = customerService;
            this.complaintService = complaintService;
        }
        #endregion

        #region Action Methods
        /// <summary>
        /// Load dashborad and all its widgets
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var requester = (string)Session["RoleName"] == "Admin" ? "Admin" : Session["EmployeeID"].ToString();
            DashboardViewModel dashboardViewModel = new DashboardViewModel();
            #region Employee Requests Widget
            dashboardViewModel.Employees = GetAllEmployees();
            dashboardViewModel.EmployeeRequests = GetEmployeeRequests(requester);
            #endregion

            #region Complaints Widget
            dashboardViewModel.Complaints = GetComplaints(requester);
            dashboardViewModel.Customers = GetAllCustomers();
            #endregion

            #region Employee Requests Widget
            #endregion
            #region Employee Requests Widget
            #endregion
            #region Employee Requests Widget
            #endregion
            #region Employee Requests Widget
            #endregion
            #region Employee Requests Widget
            #endregion
            #region Employee Requests Widget
            #endregion
            #region Employee Requests Widget
            #endregion
            #region Employee Requests Widget
            #endregion
            #region Employee Requests Widget
            #endregion
            #region Employee Requests Widget
            #endregion
            ViewBag.UserName = Session["FullName"].ToString();
            ViewBag.UserRole = Session["RoleName"].ToString();
            return View(dashboardViewModel);
        }
        #endregion

        #region Methods to load Data from Database
        /// <summary>
        /// Load employee's requests for widget
        /// </summary>
        /// <param name="requester"></param>
        /// <returns></returns>
        private IEnumerable<DashboardModels.EmployeeRequest> GetEmployeeRequests(string requester)
        {
            return employeeRequestService.LoadRequestsForDashboard(requester).Select(x => x.CreateForDashboard());
        }
        /// <summary>
        /// Load all employees for dropdownlist
        /// </summary>
        /// <returns></returns>
        private IEnumerable<DashboardModels.Employee> GetAllEmployees()
        {
            return employeeService.GetAll().Select(x => x.CreateForDashboard());
        }
        /// <summary>
        /// Load all customers for dropdownlist
        /// </summary>
        /// <returns></returns>
        private IEnumerable<DashboardModels.Customer> GetAllCustomers()
        {
            return customerService.GetAll().Select(x => x.CreateForDashboard());
        }
        /// <summary>
        /// Load all complaints for widget
        /// </summary>
        /// <returns></returns>
        private IEnumerable<DashboardModels.Complaint> GetComplaints(string requester)
        {
            return complaintService.LoadComplaintsForDashboard(requester).Select(x => x.CreateForDashboard());
        }
        #endregion

        #region Ajax response Methods
        /// <summary>
        /// Load employee requests
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpGet]
        [SiteAuthorize(PermissionKey = "RequestIndex")]
        public JsonResult LoadEmployeeRequests(long employeeId)
        {
            var requester = employeeId == 0 ? Session["RoleName"].ToString() == "Admin" ? "Admin" : Session["EmployeeID"].ToString() : employeeId.ToString();
            var employeeRequests = GetEmployeeRequests(requester);
            return Json(employeeRequests, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Load complaints
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpGet]
        [SiteAuthorize(PermissionKey = "ComplaintIndex")]
        public JsonResult LoadComplaints(long customerId)
        {
            var requester = customerId == 0 ? Session["RoleName"].ToString() == "Admin" ? "Admin" : Session["CustomerID"].ToString() : customerId.ToString();
            var employeeRequests = GetComplaints(requester);
            return Json(employeeRequests, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}