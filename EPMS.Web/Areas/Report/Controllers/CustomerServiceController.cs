using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.RequestModels.Reports;
using EPMS.Web.Controllers;
using EPMS.WebBase.Mvc;
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
        //[SiteAuthorize(PermissionKey = "CustomerServiceReport")]
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

        public ActionResult Index(CustomerServiceReportsSearchRequest searchRequest)
        {
            searchRequest.SearchString = Request["search"];
            var reports = reportService.GetCustomerServiceReports(searchRequest);

            CustomerServiceListViewModel viewModel = new CustomerServiceListViewModel
            {
                Reports = reports.Reports.Select(x=>x.CreateProjectReportFromServerToClient()),
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