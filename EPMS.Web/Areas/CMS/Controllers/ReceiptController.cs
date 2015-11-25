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
using EPMS.WebModels.ViewModels.Receipt;

namespace EPMS.Web.Areas.CMS.Controllers
{
    public class ReceiptController : BaseController
    {
        #region Private

        private readonly IReceiptService receiptService;

        #endregion

        #region Constructor

        public ReceiptController(IReceiptService receiptService)
        {
            this.receiptService = receiptService;
        }

        #endregion

        #region Public

        #region Index

        public ActionResult Index()
        {
            return View(new ReceiptListViewModel
            {
                Receipts = receiptService.GetAll().Select(x => x.CreateFromServerToClient())
            });
        }

        #endregion

        #region Detail

        public ActionResult Detail(long id)
        {
            ReceiptViewModel viewModel = new ReceiptViewModel();
            ReceiptResponse response = receiptService.GetReceiptDetails(id);

            viewModel.Receipt = response.Receipt.CreateFromServerToClient();
            viewModel.Invoice = response.Invoice.CreateFromServerToClient();
            viewModel.Quotation = response.Quotation.CreateForInvoice();
            viewModel.Customer = response.Customer.CreateFromServerToClient();
            viewModel.CompanyProfile = response.CompanyProfile.CreateFromServerToClientForQuotation();

            ViewBag.LogoPath = ConfigurationManager.AppSettings["CompanyLogo"] + viewModel.CompanyProfile.CompanyLogoPath;
            ViewBag.EmployeeName = User.Identity.Name;

            return View(viewModel);
        }

        #endregion

        #region Create

        public ActionResult Create(int installmentId, long invoiceId)
        {
            ReceiptViewModel viewModel = new ReceiptViewModel();
            viewModel.Receipt.InstallmentNumber = installmentId;
            viewModel.Invoice.InvoiceId = invoiceId;

            return View(viewModel);
        }

        #endregion

        #endregion
    }
}