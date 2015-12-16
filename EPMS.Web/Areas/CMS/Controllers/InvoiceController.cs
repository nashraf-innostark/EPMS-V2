﻿using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.ResponseModels;
using EPMS.Web.Controllers;
using EPMS.WebBase.Mvc;
using EPMS.WebModels.ModelMappers;
using EPMS.WebModels.ViewModels.Invoice;
using EPMS.WebModels.ViewModels.Receipt;
using EPMS.WebModels.WebsiteModels;
using Microsoft.AspNet.Identity;

namespace EPMS.Web.Areas.CMS.Controllers
{
    public class InvoiceController : BaseController
    {
        #region Private

        private readonly IInvoiceService invoiceService;
        private readonly IReceiptService receiptService;

        #endregion

        #region Constructor

        public InvoiceController(IInvoiceService invoiceService, IReceiptService receiptService)
        {
            this.invoiceService = invoiceService;
            this.receiptService = receiptService;
        }

        #endregion

        #region Public

        #region Index
        [SiteAuthorize(PermissionKey = "InvoiceIndex")]
        public ActionResult Index()
        {
            var role = Session["RoleName"].ToString().ToLower();
            var userId = User.Identity.GetUserId();

            InvoiceListViewModel viewModel = new InvoiceListViewModel
            {
                Invoices = role == "customer"
                    ? invoiceService.GetAll(userId).Select(x => x.CreateFromServerToClient())
                    : invoiceService.GetAll().Select(x => x.CreateFromServerToClient())
            };

            return View(viewModel);
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
                viewModel.CompanyProfile = response.CompanyProfile != null ? response.CompanyProfile.CreateFromServerToClientForQuotation() : new CompanyProfile();

            viewModel.FirstReceiptId = response.FirstReceiptId;
            viewModel.SecondReceiptId = response.SecondReceiptId;
            viewModel.ThirdReceiptId = response.ThirdReceiptId;
            viewModel.FourthReceiptId = response.FourthReceiptId;

            ViewBag.LogoPath = ConfigurationManager.AppSettings["CompanyLogo"] +
                               viewModel.CompanyProfile.CompanyLogoPath;
            ViewBag.EmployeeNameE = response.EmployeeNameE;
            ViewBag.EmployeeNameA = response.EmployeeNameA;
            return View(viewModel);
        }

        #endregion

        #region Create Receipt

        public ActionResult CreateReceipt(int installmentId, long invoiceId, decimal amountPaid)
        {
            ReceiptViewModel viewModel = new ReceiptViewModel
            {
                Receipt =
                {
                    InstallmentNumber = installmentId,
                    InvoiceId = invoiceId,
                    AmountPaid = amountPaid,
                    IsPaid = false
                }
            };

            long receiptId = receiptService.AddReceipt(viewModel.Receipt.CreateFromClientToServer());

            return Json(receiptId);
        }

        #endregion

        #endregion
    }
}