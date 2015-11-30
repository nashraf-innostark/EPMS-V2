using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.RequestModels.Reports;
using EPMS.Web.Controllers;
using EPMS.WebModels.ModelMappers;
using EPMS.WebModels.ViewModels.Reports;

namespace EPMS.Web.Areas.Report.Controllers
{
    public class CustomerController : BaseController
    {

        #region Private

        private readonly IReportService reportService;

        #endregion

        #region Constructor
        public CustomerController(IReportService reportService)
        {
            this.reportService = reportService;
        }

        #endregion

        #region Public
        public ActionResult Create()
        {
            CustomerCreateViewModel viewModel = new CustomerCreateViewModel();
            return View(viewModel);
        }

        public ActionResult Detail(CustomerCreateViewModel customerCreateViewModel)
        {
            CustomerReportDetailRequest request = new CustomerReportDetailRequest();
            request.ReportId = customerCreateViewModel.ReportId;
            request.RequesterRole = "Admin";
            request.RequesterId = Session["UserID"].ToString();
            request.StartDate = customerCreateViewModel.StartDate != null ? DateTime.ParseExact(customerCreateViewModel.StartDate, "dd/MM/yyyy", new CultureInfo("en")) : new DateTime();
            request.EndDate = customerCreateViewModel.EndDate != null ? DateTime.ParseExact(customerCreateViewModel.EndDate, "dd/MM/yyyy", new CultureInfo("en")) : new DateTime();

            var refrel = Request.UrlReferrer;
            if (refrel != null && refrel.ToString().Contains("Report/Customer/Create"))
                request.IsCreate = true;
            var response = reportService.SaveAndGetCustomerList(request);
            customerCreateViewModel.Customers = response.Customers.Select(x => x.CreateFromServerToClient()).ToList();
            customerCreateViewModel.ReportId = response.ReportId;

            return View(customerCreateViewModel);
        }

        #endregion
    }
}