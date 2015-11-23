using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.RequestModels.Reports;
using EPMS.Web.Controllers;
using EPMS.WebModels.ModelMappers.Reports;
using EPMS.WebModels.ViewModels.Reports;

namespace EPMS.Web.Areas.Report.Controllers
{
    public class CustomerServiceController : BaseController
    {
        #region Private

        private readonly IReportService reportService;

        #endregion

        #region Constructor

        public CustomerServiceController(IReportService reportService)
        {
            this.reportService = reportService;
        }

        #endregion

        #region Public

        #region Index

        public ActionResult Index()
        {
            CustomerServiceReportsSearchRequest searchRequest = new CustomerServiceReportsSearchRequest();
            CustomerServiceListViewModel viewModel = new CustomerServiceListViewModel
            {
                SearchRequest = searchRequest
            };
            return View(viewModel);
        }

        [HttpPost]

        public ActionResult QIIndex(CustomerServiceReportsSearchRequest searchRequest)
        {
            searchRequest.SearchString = Request["search"];
            var reports = reportService.GetQuotationInvoiceReports(searchRequest);

            CustomerServiceListViewModel viewModel = new CustomerServiceListViewModel
            {
                aaData = reports.Reports.Select(x => x.CreateReportFromServerToClient()),
                recordsTotal = reports.TotalCount,
                recordsFiltered = reports.FilteredCount,
                sEcho = searchRequest.sEcho
            };

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AllCustomerIndex(CustomerServiceReportsSearchRequest searchRequest)
        {
            searchRequest.SearchString = Request["search"];
            var reports = reportService.GetAllCustoemrReport(searchRequest);

            CustomerServiceListViewModel viewModel = new CustomerServiceListViewModel
            {
                aaData = reports.Reports.Select(x => x.CreateReportFromServerToClient()),
                recordsTotal = reports.TotalCount,
                recordsFiltered = reports.FilteredCount,
                sEcho = searchRequest.sEcho
            };

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion
    }
}