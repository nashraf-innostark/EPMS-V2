using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.ResponseModels;
using EPMS.Web.Controllers;
using EPMS.WebModels.ModelMappers;
using EPMS.WebModels.ViewModels.Invoice;
using Microsoft.AspNet.Identity;

namespace EPMS.Web.Areas.CMS.Controllers
{
    public class InvoiceController : BaseController
    {
        #region Private

        private readonly IInvoiceService invoiceService;

        #endregion

        #region Constructor

        public InvoiceController(IInvoiceService invoiceService)
        {
            this.invoiceService = invoiceService;
        }

        #endregion

        #region Public

        #region Index
        public ActionResult Index()
        {
            return View(new InvoiceListViewModel
            {
                Invoices = invoiceService.GetAll().Select(x => x.CreateFromServerToClient())
            });
        }

        #endregion

        #region Detail

        public ActionResult Detail(long id)
        {
            InvoiceViewModel viewModel = new InvoiceViewModel();
            InvoiceResponse response = invoiceService.GetInvoiceDetails(id);

            viewModel.Invoice = response.Invoice.CreateFromServerToClient();
            viewModel.Quotation = response.Quotation.CreateForInvoice();
            viewModel.Customer = response.Customer.CreateFromServerToClient();
            if (response.CompanyProfile != null)
            viewModel.CompanyProfile = response.CompanyProfile.CreateFromServerToClientForQuotation();

            ViewBag.LogoPath = ConfigurationManager.AppSettings["CompanyLogo"] + viewModel.CompanyProfile.CompanyLogoPath;
            ViewBag.EmployeeName = User.Identity.Name;
            return View(viewModel);
        }

        #endregion

        #endregion
    }
}