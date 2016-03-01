using System;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.Common;
using EPMS.Models.RequestModels.Reports;
using EPMS.Web.Controllers;
using EPMS.WebBase.Mvc;
using EPMS.WebModels.ModelMappers;
using EPMS.WebModels.ViewModels.Reports;

namespace EPMS.Web.Areas.Report.Controllers
{
    [SiteAuthorize(PermissionKey = "CustomerServiceReport", IsModule = true)]
    public class RFQOrderController : BaseController
    {
        #region Private
        private readonly ICustomerService customerService;
        #endregion

        #region Constructor
        public RFQOrderController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }
        #endregion

        #region Public

        // GET: Report/RFQOrder
        [SiteAuthorize(PermissionKey = "RFQOrderPlacedReportCreate")]
        public ActionResult Create()
        {
            RfqOrderCreateViewModel viewModel = new RfqOrderCreateViewModel
            {
                Customers = customerService.GetAll().Select(x=>x.CreateForDashboard()).ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(RfqOrderCreateViewModel viewModel)
        {
            RfqOrderCreateRequest request = new RfqOrderCreateRequest
            {
                CustomerId = viewModel.CustomerId,
                ReportFromDate = !string.IsNullOrEmpty(viewModel.StartDate) ? DateTime.ParseExact(viewModel.StartDate, "dd/MM/yyyy", new CultureInfo("en")) : DateTime.Now,
                ReportToDate = !string.IsNullOrEmpty(viewModel.EndDate) ? DateTime.ParseExact(viewModel.EndDate, "dd/MM/yyyy", new CultureInfo("en")) : DateTime.Now,

            };
            WebModels.WebsiteModels.Report report = new WebModels.WebsiteModels.Report();
            if (viewModel.CustomerId > 0)
            {
                request.ReportCategoryId = (int) ReportCategory.Customer;
            }
            else
            {
                request.ReportCategoryId = (int) ReportCategory.AllCustomer;
            }

            return RedirectToAction("");
        }

        #endregion
    }
}