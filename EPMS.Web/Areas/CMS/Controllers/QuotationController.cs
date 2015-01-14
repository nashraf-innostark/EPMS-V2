using System.Linq;
using System.Web.Mvc;
using EPMS.Implementation.Services;
using EPMS.Interfaces.IServices;
using EPMS.Web.ModelMappers;
using EPMS.Web.ViewModels.Quotation;
using Microsoft.AspNet.Identity;

namespace EPMS.Web.Areas.CMS.Controllers
{
    [Authorize]
    public class QuotationController : Controller
    {
        #region Private
        private readonly ICustomerService CustomerService;
        private readonly IAspNetUserService AspNetUserService;
        private readonly IOrdersService OrdersService;
        private readonly IQuotationService QuotationService;
        #endregion

        #region Constructor

        public QuotationController(ICustomerService customerService, IAspNetUserService aspNetUserService, IOrdersService ordersService, IQuotationService quotationService)
        {
            CustomerService = customerService;
            AspNetUserService = aspNetUserService;
            OrdersService = ordersService;
            QuotationService = quotationService;
        }

        #endregion

        #region Public
        // GET: CMS/Quotation
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Create(long? id)
        {
            QuotationCreateViewModel viewModel = new QuotationCreateViewModel();
            if (id == null)
            {
                var customers = CustomerService.GetAll();
                viewModel.Customers = customers.Select(x => x.CreateFromServerToClient());
                var users = AspNetUserService.FindById(User.Identity.GetUserId());
                var direction = Resources.Shared.Common.TextDirection;
                if (users.Employee != null && direction == "ltr")
                {
                    viewModel.CreatedByEmployee = users.Employee.EmployeeId;
                    viewModel.CreatedByName = users.Employee.EmployeeNameE;
                }
                if (users.Employee != null && direction == "rtl")
                {
                    viewModel.CreatedByEmployee = users.Employee.EmployeeId;
                    viewModel.CreatedByName = users.Employee.EmployeeNameA;
                }
            }
            // Mapper
            //viewModel = QuotationService.FindQuotationById(id);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(QuotationCreateViewModel model)
        {
            return RedirectToAction("Create");
            //return RedirectToAction("Index");
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