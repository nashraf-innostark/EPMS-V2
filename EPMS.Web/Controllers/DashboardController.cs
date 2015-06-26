using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Claims;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.Common;
using EPMS.Models.DomainModels;
using EPMS.Models.ResponseModels;
using EPMS.Web.DashboardModels;
using EPMS.Web.ModelMappers;
using EPMS.Web.ModelMappers.Inventory.DIF;
using EPMS.Web.ModelMappers.Inventory.RFI;
using EPMS.Web.ModelMappers.Inventory.RIF;
using EPMS.Web.Models;
using EPMS.Web.ViewModels.Dashboard;
using EPMS.WebBase.Mvc;
using Microsoft.AspNet.Identity;
using Complaint = EPMS.Web.DashboardModels.Complaint;
using Customer = EPMS.Web.DashboardModels.Customer;
using Department = EPMS.Web.DashboardModels.Department;
using Employee = EPMS.Web.DashboardModels.Employee;
using Meeting = EPMS.Web.DashboardModels.Meeting;
using Order = EPMS.Web.DashboardModels.Order;
using Payroll = EPMS.Web.DashboardModels.Payroll;
using EmployeeRequest = EPMS.Web.DashboardModels.EmployeeRequest;

namespace EPMS.Web.Controllers
{
    [Authorize]
    [SiteAuthorize(PermissionKey = "Dashboard", IsModule = true)]
    public class DashboardController : BaseController
    {
        #region Constructor and Private Services objects

        private readonly IWarehouseService warehouseService;
        private readonly IItemReleaseService itemReleaseService;
        private readonly IRIFService rifService;
        private readonly IDIFService difService;
        private readonly ITIRService tirService;
        private readonly IPurchaseOrderService purchaseOrderService;
        private readonly IProjectTaskService projectTaskService;
        private readonly IRFIService rfiService;
        private readonly IMeetingService meetingService;
        private readonly IProjectService projectService;
        private readonly IPayrollService payrollService;
        private readonly IDepartmentService departmentService;
        private readonly IJobOfferedService jobOfferedService;
        private readonly IOrdersService ordersService;
        private readonly IEmployeeRequestService employeeRequestService;
        private readonly IEmployeeService employeeService;
        private readonly ICustomerService customerService;
        private readonly IComplaintService complaintService;
        private readonly IDashboardWidgetPreferencesService PreferencesService;
        private readonly IMenuRightsService menuRightsService;
        private readonly IQuickLaunchItemService quickLaunchItemService;

        /// <summary>
        /// Dashboard constructor
        /// </summary>
        /// <param name="warehouseService"></param>
        /// <param name="itemReleaseService"></param>
        /// <param name="rifService"></param>
        /// <param name="difService"></param>
        /// <param name="tirService"></param>
        /// <param name="purchaseOrderService"></param>
        /// <param name="projectTaskService"></param>
        /// <param name="rfiService"></param>
        /// <param name="meetingService"></param>
        /// <param name="projectService"></param>
        /// <param name="payrollService"></param>
        /// <param name="departmentService"></param>
        /// <param name="jobOfferedService"></param>
        /// <param name="ordersService"></param>
        /// <param name="employeeRequestService"></param>
        /// <param name="employeeService"></param>
        /// <param name="customerService"></param>
        /// <param name="complaintService"></param>
        /// <param name="preferencesService"></param>
        /// <param name="menuRightsService"></param>
        public DashboardController(IWarehouseService warehouseService,IItemReleaseService itemReleaseService,IRIFService rifService,IDIFService difService, ITIRService tirService, IPurchaseOrderService purchaseOrderService, IProjectTaskService projectTaskService,IRFIService rfiService, IMeetingService meetingService, IProjectService projectService, IPayrollService payrollService, IDepartmentService departmentService, IJobOfferedService jobOfferedService, IOrdersService ordersService, IEmployeeRequestService employeeRequestService, IEmployeeService employeeService, ICustomerService customerService, IComplaintService complaintService, IDashboardWidgetPreferencesService preferencesService, IMenuRightsService menuRightsService, IQuickLaunchItemService quickLaunchItemService)
        {
            this.warehouseService = warehouseService;
            this.itemReleaseService = itemReleaseService;
            this.rifService = rifService;
            this.difService = difService;
            this.tirService = tirService;
            this.purchaseOrderService = purchaseOrderService;
            this.projectTaskService = projectTaskService;
            this.rfiService = rfiService;
            this.meetingService = meetingService;
            this.projectService = projectService;
            this.payrollService = payrollService;
            this.departmentService = departmentService;
            this.jobOfferedService = jobOfferedService;
            this.ordersService = ordersService;
            this.employeeRequestService = employeeRequestService;
            this.employeeService = employeeService;
            this.customerService = customerService;
            this.complaintService = complaintService;
            PreferencesService = preferencesService;
            this.menuRightsService = menuRightsService;
            this.quickLaunchItemService = quickLaunchItemService;
        }
        #endregion

        #region Action Methods
        /// <summary>
        /// Load dashborad and all its widgets
        /// </summary>
        /// <returns></returns>
        [SiteAuthorize(PermissionKey = "Dashboard")]
        public ActionResult Index()
        {
            DashboardViewModel dashboardViewModel = new DashboardViewModel();
            object userPermissionSet = System.Web.HttpContext.Current.Session["UserPermissionSet"];
            string[] userPermissionsSet = (string[])userPermissionSet;

            if (Session["RoleName"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            ViewBag.UserRole = Session["RoleName"];


            var requester = string.Empty;

            //if ((string)Session["RoleName"] != "Customer")
            //    requester = (string)Session["RoleName"] == "Admin" ? "Admin" : Session["EmployeeID"].ToString();
            //else
            //    requester = (string)Session["RoleName"] == "Admin" ? "Admin" : Session["CustomerID"].ToString();

            switch ((UserRole)Convert.ToInt32(Session["RoleKey"].ToString()))
            {
                    case UserRole.Admin:
                    requester = "Admin";
                        break;
                    case UserRole.InventoryManager:
                        requester = "Admin";
                        break;
                    case UserRole.WarehouseManager:
                        requester = "Admin";
                        break;
                    case UserRole.Employee:
                        requester = Session["EmployeeID"].ToString();
                        break;
                    case UserRole.Customer:
                        requester = Session["CustomerID"].ToString();
                        break;
            }

            #region Employee Requests Widget

            if (userPermissionsSet.Contains("EmployeeRequestsWidget"))
            {
                dashboardViewModel.Employees = GetAllEmployees();
                dashboardViewModel.EmployeeRequests = GetEmployeeRequests(requester);
            }

            #endregion

            #region Recruitment Widget
            if (userPermissionsSet.Contains("RecruitmentWidget"))
            {
                dashboardViewModel.Recruitments = GetRecruitments();
            }
            #endregion

            #region Recent Employees Widget
            if (userPermissionsSet.Contains("RecentEmployeesWidget"))
            {
                dashboardViewModel.Departments = GetAllDepartments();
                dashboardViewModel.EmployeesRecent = GetRecentEmployees(requester);
            }

            #endregion

            #region Profile Widget
            if (userPermissionsSet.Contains("MyProfileWidget"))
            {
                if (Session["EmployeeID"] != null)
                    dashboardViewModel.Profile = GetMyProfile();
            }
            #endregion

            #region Payroll Widget
            if (userPermissionsSet.Contains("PayrollWidget"))
            {
                if (Session["EmployeeID"] != null)
                    dashboardViewModel.Payroll = GetPayroll(DateTime.Now);
            }

            #endregion

            #region Meeting Widget
            if (userPermissionsSet.Contains("MeetingWidget"))
            {
                dashboardViewModel.Meetings = GetMeetings(requester);
            }
            #endregion

            #region My Tasks
            if (userPermissionsSet.Contains("MyTasksWidget"))
            {
                dashboardViewModel.TaskProjectsDDL = GetTaskProjectsDDL();
                dashboardViewModel.ProjectTasks = GetMyTasks(0);//0 means all projects tasks
            }
            #endregion

            #region Complaints Widget
            if (userPermissionsSet.Contains("ComplaintsWidget"))
            {
                dashboardViewModel.Complaints = GetComplaints(requester);
                dashboardViewModel.Customers = GetAllCustomers();
            }
            #endregion

            #region Orders Widget
            if (userPermissionsSet.Contains("OrdersWidget"))
            {
                dashboardViewModel.Orders = GetOrders(requester, 0);//0 means all
            }
            #endregion

            #region Project Widget
            if (userPermissionsSet.Contains("ProjectWidget"))
            {
                dashboardViewModel.Project = GetProjects(requester, 0);//0 means all projects, 1 means Current project
                dashboardViewModel.ProjectsDDL = GetProjectsDDL(requester, 1);
            }
            #endregion

            #region RFI Widget
            if (userPermissionsSet.Contains("RFIWidget"))
            {
                if (Session["EmployeeID"] != null && requester == Session["EmployeeID"].ToString())//Means employee is here
                {
                    dashboardViewModel.RFI = GetRFI(0, Session["UserID"].ToString());// status 0 means all
                }
                else
                {
                    dashboardViewModel.RFI = GetRFI(0, requester);// status 0 means all
                }
                
            }
            #endregion

            #region RIF Widget
            if (userPermissionsSet.Contains("RIFWidget"))
            {
                if (Session["EmployeeID"] != null && requester == Session["EmployeeID"].ToString())//Means employee is here
                {
                    dashboardViewModel.RIF = GetRIF(0, Session["UserID"].ToString());// status 0 means all
                }
                else
                {
                    dashboardViewModel.RIF = GetRIF(0, requester);// status 0 means all
                }

            }
            #endregion

            #region IRF Widget
            if (userPermissionsSet.Contains("IRFWidget"))
            {
                if (Session["EmployeeID"] != null && requester == Session["EmployeeID"].ToString())//Means employee is here
                {
                    dashboardViewModel.IRF = GetIRF(0, Session["UserID"].ToString());// status 0 means all
                }
                else
                {
                    dashboardViewModel.IRF = GetIRF(0, requester);// status 0 means all
                }

            }
            #endregion

            #region DIF Widget
            if (userPermissionsSet.Contains("DIFWidget"))
            {
                if (Session["EmployeeID"] != null && requester == Session["EmployeeID"].ToString())//Means employee is here
                {
                    dashboardViewModel.DIF = GetDIF(0, Session["UserID"].ToString());// status 0 means all
                }
                else
                {
                    dashboardViewModel.DIF = GetDIF(0, requester);// status 0 means all
                }

            }
            #endregion

            #region TIR Widget
            if (userPermissionsSet.Contains("TIRWidget"))
            {
                if (Session["EmployeeID"] != null && requester == Session["EmployeeID"].ToString())//Means employee is here
                {
                    dashboardViewModel.TIR = GetTIR(0, Session["UserID"].ToString());// status 0 means all
                }
                else
                {
                    dashboardViewModel.TIR = GetTIR(0, requester);// status 0 means all
                }

            }
            #endregion

            #region PO Widget
            if (userPermissionsSet.Contains("POWidget"))
            {
                if (Session["EmployeeID"] != null && requester == Session["EmployeeID"].ToString())//Means employee is here
                {
                    dashboardViewModel.PO = GetPO(0, Session["UserID"].ToString());// status 0 means all
                }
                else
                {
                    dashboardViewModel.PO = GetPO(0, requester);// status 0 means all
                }

            }
            #endregion

            #region Warehouses for DDL
            var warehouses = warehouseService.GetAll().ToList();
            if (warehouses.Any())
                dashboardViewModel.Warehouses = warehouses.Select(x => x.CreateDDL());
            #endregion

            #region Widget Preferences
            dashboardViewModel.QuickLaunchItems = LoadQuickLaunchMenuItems();
            dashboardViewModel.LaunchItems = LoadQuickLaunchUserItems();

            string userId = User.Identity.GetUserId();
            dashboardViewModel.WidgetPreferenceses = PreferencesService.LoadAllPreferencesByUserId(userId).Select(x => x.CreateFromClientToServerWidgetPreferences());
            #endregion


            ViewBag.UserName = Session["FullName"].ToString();
            ViewBag.UserRole = Session["RoleName"].ToString();
            var direction = Resources.Shared.Common.TextDirection;
            ViewBag.ReorderTitle = direction == "ltr" ? "Drag & Drop" : "سحب و افلات";
            ViewBag.ReorderBody = direction == "ltr" ? "Reorder Widgets or Quicklaunch bar items by dragging & dropping them." : ".إعادة ترتيب القطع أو عناصر شريط عن طريق سحب و إفلاتها";
            //dashboardViewModel.UserMac = GetMacAddress();
            return View(dashboardViewModel);
        }
        #endregion

        #region Methods to load Data from Database
        /// <summary>
        /// Load employee's requests for widget
        /// </summary>
        /// <param name="requester"></param>
        /// <returns></returns>
        private IEnumerable<EmployeeRequest> GetEmployeeRequests(string requester)
        {
            return employeeRequestService.LoadRequestsForDashboard(requester).Select(x => x.CreateForDashboard());
        }
        /// <summary>
        /// Load Meetings
        /// </summary>
        /// <param name="requester"></param>
        /// <returns></returns>
        private IEnumerable<Meeting> GetMeetings(string requester)
        {
            return meetingService.LoadMeetingsForDashboard(requester).Select(x => x.CreateForDashboard());
        }
        /// <summary>
        /// Load all employees for dropdownlist
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Employee> GetAllEmployees()
        {
            return employeeService.GetAll().Select(x => x.CreateForDashboard());
        }
        private IEnumerable<Department> GetAllDepartments()
        {
            return departmentService.GetAll().Select(x => x.CreateForDashboard());
        }
        /// <summary>
        /// Load all customers for dropdownlist
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Customer> GetAllCustomers()
        {
            return customerService.GetAll().Select(x => x.CreateForDashboard());
        }
        /// <summary>
        /// Load all complaints for widget
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Complaint> GetComplaints(string requester)
        {
            return complaintService.LoadComplaintsForDashboard(requester).Select(x => x.CreateForDashboard());
        }
        /// <summary>
        /// Load recent 5 orders
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Order> GetOrders(string requester, int status)
        {
            return ordersService.GetRecentOrders(requester, status).Select(x => x.CreateForDashboard());
        }

        private IEnumerable<RFIWidget> GetRFI(int status,string requester, DateTime date=new DateTime())
        {
            var rfis = rfiService.GetRecentRFIs(status,requester,date);
            if(rfis.Any())
                return rfis.Select(x => x.CreateRFIWidget());
            return new List<RFIWidget>();
        }

        private IEnumerable<RIFWidget> GetRIF(int status, string requester, DateTime date = new DateTime())
        {
            var rfis = rifService.GetRecentRIFs(status, requester, date);
            if (rfis.Any())
                return rfis.Select(x => x.CreateWidget());
            return new List<RIFWidget>();
        }

        private IEnumerable<IRFWidget> GetIRF(int status, string requester, DateTime date = new DateTime())
        {
            var rfis = itemReleaseService.GetRecentIRFs(status, requester, date);
            if (rfis.Any())
                return rfis.Select(x => x.CreateWidget());
            return new List<IRFWidget>();
        }

        private IEnumerable<DIFWidget> GetDIF(int status, string requester, DateTime date = new DateTime())
        {
            var rfis = difService.GetRecentDIFs(status, requester, date);
            if (rfis.Any())
                return rfis.Select(x => x.CreateWidget());
            return new List<DIFWidget>();
        }

        private IEnumerable<TIRWidget> GetTIR(int status, string requester, DateTime date = new DateTime())
        {
            var rfis = tirService.GetRecentTIRs(status, requester, date);
            if (rfis.Any())
                return rfis.Select(x => x.CreateWidget());
            return new List<TIRWidget>();
        }

        private IEnumerable<POWidget> GetPO(int status, string requester, DateTime date = new DateTime())
        {
            var rfis = purchaseOrderService.GetRecentPOs(status, requester, date);
            if (rfis.Any())
                return rfis.Select(x => x.CreateWidget());
            return new List<POWidget>();
        }

        private ProjectResponseForDashboard GetProjects(string requester, long projectId)
        {
            return projectService.LoadProjectForDashboard(requester, projectId);
        }
        private IEnumerable<ProjectTaskResponse> GetMyTasks(long projectId)
        {
            if (Session["EmployeeID"] != null)
                return projectTaskService.LoadProjectTasksByEmployeeId(
                    Convert.ToInt64(Session["EmployeeID"].ToString()), projectId) ?? Enumerable.Empty<ProjectTaskResponse>();
            return Enumerable.Empty<ProjectTaskResponse>();
        }

        private IEnumerable<DashboardModels.Project> GetProjectsDDL(string requester, int status)
        {
            return projectService.LoadAllProjects(requester, status).Select(x => x.CreateForDashboardDDL());
        }
        private IEnumerable<DashboardModels.Project> GetTaskProjectsDDL()
        {
            IEnumerable<DashboardModels.Project> list = Enumerable.Empty<DashboardModels.Project>();
            if (Session["EmployeeID"] != null)
            {
                var projectsDdl =
                    projectService.LoadAllProjectsByEmployeeId(Convert.ToInt64(Session["EmployeeID"].ToString()));
                return projectsDdl.Any() ? projectsDdl.Select(x => x.CreateForDashboardDDL()) : list;
            }
            return list;
        }

        /// <summary>
        /// Load recent jobs offered
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Recruitment> GetRecruitments()
        {
            return jobOfferedService.GetRecentJobOffereds().Select(x => x.CreateForDashboard());
        }
        /// <summary>
        /// Load recent registered employees
        /// </summary>
        /// <param name="requester"></param>
        /// <returns></returns>
        private IEnumerable<Employee> GetRecentEmployees(string requester)
        {
            return employeeService.GetRecentEmployees(requester).Select(x => x.CreateForDashboard());
        }
        /// <summary>
        /// Load Profile data from employee table of current user
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        private Profile GetMyProfile()
        {
            return Session["EmployeeID"] != null ? employeeService.FindEmployeeById(Convert.ToInt64(Session["EmployeeID"].ToString())).CreateForDashboardProfile() : null;
        }

        private Payroll GetPayroll(DateTime date)
        {
            return Session["EmployeeID"] != null ? payrollService.LoadPayroll(Convert.ToInt64(Session["EmployeeID"].ToString()), date).CreatePayrollForDashboard() : null;
        }

        /// <summary>
        /// Loads All Quick Launch Items
        /// </summary>
        private IEnumerable<Models.QuickLaunchMenuItems> LoadQuickLaunchMenuItems()
        {
            string userName = HttpContext.User.Identity.Name;
            if (!String.IsNullOrEmpty(userName))
            {
                AspNetUser userResult = UserManager.FindByName(userName);
                if (userResult != null)
                {
                    IList<AspNetRole> roles = userResult.AspNetRoles.ToList();
                    if (roles.Count > 0)
                    {
                        List<QuickLaunchMenuItems> menuItems =
                            menuRightsService.FindMenuItemsByRoleId(roles[0].Id)
                                .Where(menuR => menuR.Menu.IsRootItem == false && menuR.Menu.IsMenuItem == true)
                                .ToList()
                                .Select(menuR => menuR.CreateFrom())
                                .ToList();
                        IEnumerable<QuickLaunchItem> userItems =
                            quickLaunchItemService.FindItemsByEmployeeId(ClaimsPrincipal.Current.Identity.GetUserId());

                        foreach (QuickLaunchItem userItem in userItems)
                        {
                            menuItems.RemoveAll(item => item.MenuID == userItem.MenuId);
                        }

                        return menuItems;
                    }
                }
            }
            return Enumerable.Empty<Models.QuickLaunchMenuItems>();

        }
        public IEnumerable<QuickLaunchUserItems> LoadQuickLaunchUserItems()
        {
            IList<QuickLaunchUserItems> items = new List<QuickLaunchUserItems>();
            items = quickLaunchItemService.FindItemsByEmployeeId(ClaimsPrincipal.Current.Identity.GetUserId()).Select(x => x.CreateForUserItems()).ToList();
            return items;
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
        /// Save Dashboard Widgets Preferences
        /// </summary>
        /// <param name="preferences"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SaveWidgetPreferences(string[] preferences)
        {
            IList<string> userPreferences = preferences.ToList();
            string[] allWidgets =
            {
                "RecruitmentWidget", "EmployeeRequestsWidget","OrdersWidget","ComplaintsWidget", "EmployeesWidget", 
                "PaymentWidget", "AlertsWidget", "MeetingWidget", "MyProfileWidget", "PayrollWidget", "MyTasksWidget", "ProjectWidget"
            };
            foreach (var allWidget in allWidgets)
            {
                if (!userPreferences.Contains(allWidget))
                {
                    userPreferences.Add(allWidget);
                }
            }
            var userId = User.Identity.GetUserId();
            var result = PreferencesService.LoadAllPreferencesByUserId(userId).ToList();
            foreach (var userpreference in result)
            {
                PreferencesService.Deletepreferences(userpreference);
            }
            //if (result.Any())
            //{
                // Add
                int i = 1;
                foreach (var pref in userPreferences)
                {
                    Models.DashboardWidgetPreference preference = new Models.DashboardWidgetPreference { UserId = userId, WidgetId = pref, SortNumber = i };
                    var preferenceToUpdate = preference.CreateFromClientToServerWidgetPreferences();
                    if (PreferencesService.AddPreferences(preferenceToUpdate))
                    {
                        i++;
                    }
                }
                return Json("Success", JsonRequestBehavior.AllowGet);
            //}
            //else
            //{
            //    var userpreferences = PreferencesService.LoadAllPreferencesByUserId(userId).ToList();
            //    // Update
            //    int i = 0;
            //    foreach (var pref in preferences)
            //    {
            //        Models.DashboardWidgetPreference preference = new Models.DashboardWidgetPreference
            //        {
            //            WidgetPerferencesId = userpreferences[i].WidgetPerferencesId,
            //            UserId = userpreferences[i].UserId,
            //            WidgetId = pref,
            //            SortNumber = i + 1
            //        };
            //        var preferenceToUpdate = preference.CreateFromClientToServerWidgetPreferences();
            //        if (PreferencesService.UpdatePreferences(preferenceToUpdate))
            //        {
            //            i++;
            //        }
            //    }
            //    return Json("Success", JsonRequestBehavior.AllowGet);
            //}
            //return Json("Failed", JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveQuickLaunchPrefrences(int[] preferences)
        {
            var userId = User.Identity.GetUserId();
            if (preferences != null)
            {
                quickLaunchItemService.SaveItemPrefrences(userId, preferences);
            }
            return Json("Success", JsonRequestBehavior.AllowGet);
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
        /// <summary>
        /// Load employee's profile
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult LoadMyProfile()
        {
            var myProfile = GetMyProfile();
            return Json(myProfile, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetQuickLaunchUserItem()
        {
            DashboardViewModel dashboardViewModel = new DashboardViewModel();
            dashboardViewModel.LaunchItems = quickLaunchItemService.FindItemsByEmployeeId(ClaimsPrincipal.Current.Identity.GetUserId()).Select(x => x.CreateForUserItems());
            return Json(dashboardViewModel, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Load emloyees' payroll
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult LoadPayroll(int month)
        {
            DateTime filterDateTime = month == 1 ? DateTime.Now : DateTime.Now.AddMonths(-1);
            var payroll = GetPayroll(filterDateTime);
            return Json(payroll, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Load meetings
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult LoadMeetings()
        {
            var requester = Session["RoleName"].ToString() == "Admin" ? "Admin" : Session["EmployeeID"].ToString();
            var meetings = GetMeetings(requester);
            return Json(meetings, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult LoadProjects(long projectId)
        {
            var requester = Session["RoleName"].ToString() == "Admin" ? "Admin" : Session["CustomerID"].ToString();
            var projects = GetProjects(requester, projectId);
            return Json(projects, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult LoadProjectsDDL(int projectStatus)
        {
            var requester = Session["RoleName"].ToString() == "Admin" ? "Admin" : Session["CustomerID"].ToString();
            var projects = GetProjectsDDL(requester, projectStatus);
            return Json(projects, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult LoadMyTasks(long projectId)
        {
            var projects = GetMyTasks(projectId);
            return Json(projects, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveQuickLaunchItems(int[] menuIds)
        {
            IEnumerable<int> idsToBeSaved = null;
            if (menuIds != null)
            {
                idsToBeSaved = menuIds.ToList();
                quickLaunchItemService.SaveItems(idsToBeSaved);
            }
            LoadQuickLaunchUserItems();
            List<QuickLaunchUserItems> menus = new List<QuickLaunchUserItems>();
            foreach (var id in idsToBeSaved)
            {
                QuickLaunchUserItems userItems =
                    quickLaunchItemService.GetItemByUserAndMenuId(ClaimsPrincipal.Current.Identity.GetUserId(), id)
                        .CreateForUserItems();
                menus.Add(userItems);
                //return Json(userItems, JsonRequestBehavior.AllowGet);
            }
            return Json(menus, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteItem(int menuId)
        {
            string userId = ClaimsPrincipal.Current.Identity.GetUserId();
            quickLaunchItemService.DeleteItem(userId, menuId);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Load RFIs
        /// </summary>
        /// <param name="status"></param>
        /// <param name="requester"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult LoadRFI(int status, string requester, string date)
        {
            DateTime rfiCreatedDate = string.IsNullOrEmpty(date) ? new DateTime() : Convert.ToDateTime(date); 
            switch ((UserRole)Convert.ToInt32(Session["RoleKey"].ToString()))
            {
                case UserRole.Employee:
                    var orders = GetRFI(status, Session["UserID"].ToString(), rfiCreatedDate);// status 0 means all
                    return Json(orders, JsonRequestBehavior.AllowGet);
                default:
                    if (string.IsNullOrEmpty(requester))
                        requester = "Admin";
                    orders = GetRFI(status, requester, rfiCreatedDate);// status 0 means all
                     return Json(orders, JsonRequestBehavior.AllowGet);
            }
            return Json(new List<RFIWidget>(), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Get MAC Address
        public static string GetMacAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty)// only return MAC Address from first card  
                {
                    // Get MAC address of Ethernet
                    string networkType = adapter.NetworkInterfaceType.ToString();
                    if (networkType == "Ethernet")
                    {
                        sMacAddress = adapter.GetPhysicalAddress().ToString();
                    }
                }
            } return sMacAddress;
        }
        #endregion
    }
}