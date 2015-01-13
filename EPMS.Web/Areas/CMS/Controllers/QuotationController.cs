using System.Linq;
using System.Web.Mvc;
using EPMS.Implementation.Services;
using EPMS.Interfaces.IServices;
using EPMS.Web.ModelMappers;
using EPMS.Web.ViewModels.Quotation;
using Microsoft.AspNet.Identity;

namespace EPMS.Web.Areas.CMS.Controllers
{
    public class QuotationController : Controller
    {
        #region Private
        private readonly ICustomerService CustomerService;
        private readonly IAspNetUserService AspNetUserService;
        private readonly IOrdersService OrdersService;
        #endregion

        #region Constructor

        public QuotationController(ICustomerService customerService, IAspNetUserService aspNetUserService, IOrdersService ordersService)
        {
            CustomerService = customerService;
            AspNetUserService = aspNetUserService;
            OrdersService = ordersService;
        }

        #endregion

        #region Public
        // GET: CMS/Quotation
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Create()
        {
            QuotationCreateViewModel viewModel = new QuotationCreateViewModel();
            var customers = CustomerService.GetAll();
            viewModel.Customers = customers.Select(x => x.CreateFromServerToClient());
            var users = AspNetUserService.FindById(User.Identity.GetUserId());
            if(users.Employee != null)
                viewModel.EmployeeName = users.Employee.EmployeeNameE;
            return View(viewModel);
        }

        [HttpGet]
        public JsonResult GetCustomerOrders(long customerId)
        {
            var orders = OrdersService.GetOrdersByCustomerId(customerId).Select(x=>x.CreateFromServerToClient());
            return Json(orders, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}