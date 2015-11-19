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
    public class QuotationInvoiceController : BaseController
    {
        #region Private

        private readonly IEmployeeService employeeService;
        private readonly IReportService reportService;

        #endregion

        #region Constructor

        public QuotationInvoiceController(IEmployeeService employeeService, IReportService reportService)
        {
            this.employeeService = employeeService;
            this.reportService = reportService;
        }

        #endregion

        #region Public

        #region Create
        public ActionResult Create()
        {
            QuotationInvoiceViewModel viewModel = new QuotationInvoiceViewModel
            {
                Employees = employeeService.GetAll().Select(x => x.CreateFromServerToClient())
            };
            return View(viewModel);
        }

        #endregion

        #region Detail

        public ActionResult Detail(QuotationInvoiceViewModel quotationInvoiceViewModel)
        {
            QuotationInvoiceDetailRequest request = new QuotationInvoiceDetailRequest
            {
                EmployeeId = quotationInvoiceViewModel.EmployeeId,
                StartDate = DateTime.ParseExact(quotationInvoiceViewModel.StartDate, "dd/MM/yyyy", new CultureInfo("en")),
                EndDate = DateTime.ParseExact(quotationInvoiceViewModel.EndDate, "dd/MM/yyyy", new CultureInfo("en")),
            };

            var refrel = Request.UrlReferrer;
            if (refrel != null && refrel.ToString().Contains("Report/QuotationInvoice/Create"))
                request.IsCreate = true;
            var response = reportService.SaveAndGetQuotationInvoiceReport(request);
            quotationInvoiceViewModel.InvoicesCount = response.InvoicesCount;
            quotationInvoiceViewModel.QuotationsCount = response.QuotationsCount;

            return View(quotationInvoiceViewModel);
        }

        #endregion


        #endregion
    }
}