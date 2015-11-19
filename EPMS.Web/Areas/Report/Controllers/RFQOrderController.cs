using System;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.Common;
using EPMS.Web.Controllers;
using EPMS.WebBase.Mvc;
using EPMS.WebModels.ModelMappers;
using EPMS.WebModels.ViewModels.Reports;

namespace EPMS.Web.Areas.Report.Controllers
{
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
            WebModels.WebsiteModels.Report report = new WebModels.WebsiteModels.Report();
            if (viewModel.CustomerId > 0)
            {
                report.ReportCategoryId = (int) ReportCategory.Customer;
                report.ReportFromDate = !string.IsNullOrEmpty(viewModel.StartDate) ? DateTime.ParseExact(viewModel.StartDate, "dd/MM/yyyy", new CultureInfo("en")) : DateTime.Now;
                report.ReportToDate = !string.IsNullOrEmpty(viewModel.EndDate) ? DateTime.ParseExact(viewModel.EndDate, "dd/MM/yyyy", new CultureInfo("en")) : DateTime.Now;
            }
            else
            {
                report.ReportCategoryId = (int) ReportCategory.AllCustomer;
                report.ReportFromDate = !string.IsNullOrEmpty(viewModel.StartDate) ? DateTime.ParseExact(viewModel.StartDate, "dd/MM/yyyy", new CultureInfo("en")) : DateTime.Now;
                report.ReportToDate = !string.IsNullOrEmpty(viewModel.EndDate) ? DateTime.ParseExact(viewModel.EndDate, "dd/MM/yyyy", new CultureInfo("en")) : DateTime.Now;
            }

            return RedirectToAction("");
        }

        #endregion
    }
}