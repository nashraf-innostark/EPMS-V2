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

        private readonly IDepartmentService departmentService;
        private readonly IJobOfferedService jobOfferedService;
        private readonly IOrdersService ordersService;
        private readonly IEmployeeRequestService employeeRequestService;
        private readonly IEmployeeService employeeService;
        private readonly ICustomerService customerService;
        private readonly IComplaintService complaintService;

        /// <summary>
        /// Dashboard constructor
        /// </summary>
        /// <param name="departmentService"></param>
        /// <param name="jobOfferedService"></param>
        /// <param name="ordersService"></param>
        /// <param name="employeeRequestService"></param>
        /// <param name="employeeService"></param>
        /// <param name="customerService"></param>
        /// <param name="complaintService"></param>
        public DashboardController(IDepartmentService departmentService,IJobOfferedService jobOfferedService,IOrdersService ordersService,IEmployeeRequestService employeeRequestService, IEmployeeService employeeService, ICustomerService customerService, IComplaintService complaintService)
        {
            this.departmentService = departmentService;
            this.jobOfferedService = jobOfferedService;
            this.ordersService = ordersService;
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

            #region Orders Widget
            dashboardViewModel.Orders = GetOrders(requester,0);//0 means all
            #endregion

            #region Recruitment Widget
            dashboardViewModel.Recruitments = GetRecruitments();
            #endregion
            #region Recent Employees Widget
            dashboardViewModel.Departments = GetAllDepartments();
            dashboardViewModel.EmployeesRecent = GetRecentEmployees(requester);
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
        private IEnumerable<DashboardModels.Department> GetAllDepartments()
        {
            return departmentService.GetAll().Select(x => x.CreateForDashboard());
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
        /// <summary>
        /// Load recent 5 orders
        /// </summary>
        /// <returns></returns>
        private IEnumerable<DashboardModels.Order> GetOrders(string requester,int status)
        {
            return ordersService.GetRecentOrders(requester,status).Select(x => x.CreateForDashboard());
        }
        /// <summary>
        /// Load recent jobs offered
        /// </summary>
        /// <returns></returns>
        private IEnumerable<DashboardModels.Recruitment> GetRecruitments()
        {
            return jobOfferedService.GetRecentJobOffereds().Select(x => x.CreateForDashboard());
        }
        /// <summary>
        /// Load recent registered employees
        /// </summary>
        /// <param name="requester"></param>
        /// <returns></returns>
        private IEnumerable<DashboardModels.Employee> GetRecentEmployees(string requester)
        {
            return employeeService.GetRecentEmployees(requester).Select(x => x.CreateForDashboard());
        }
        #endregion

        #region Ajax response Methods
        /// <summary>
        /// Load employee requests
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpGet]
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
        public JsonResult LoadComplaints(long customerId)
        {
            var requester = customerId == 0 ? Session["RoleName"].ToString() == "Admin" ? "Admin" : Session["CustomerID"].ToString() : customerId.ToString();
            var employeeRequests = GetComplaints(requester);
            return Json(employeeRequests, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Load orders
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="orderStatus"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult LoadOrders(long customerId, int orderStatus)
        {
            var requester = customerId == 0 ? Session["RoleName"].ToString() == "Admin" ? "Admin" : Session["CustomerID"].ToString() : customerId.ToString();
            var orders = GetOrders(requester, orderStatus);
            return Json(orders, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Load recent recruitments
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult LoadRecruitments()
        {
            var recruitments = GetRecruitments();
            return Json(recruitments, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Load recent employees
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult LoadRecentEmployees(long departmentId)
        {
            var requester = departmentId == 0 ? "Admin" : departmentId.ToString();
            var recentEmployees = GetRecentEmployees(requester);
            return Json(recentEmployees, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}