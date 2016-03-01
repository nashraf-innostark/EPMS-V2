using System;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.Common;
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
    [SiteAuthorize(PermissionKey = "CS", IsModule = true)]
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
            viewModel.FirstStatus = response.FirstStatus;
            viewModel.FirstType = GetPaymentType(response.FirstType);
            viewModel.SecondReceiptId = response.SecondReceiptId;
            viewModel.SecondStatus = response.SecondStatus;
            viewModel.SecondType = GetPaymentType(response.SecondType);
            viewModel.ThirdReceiptId = response.ThirdReceiptId;
            viewModel.ThirdStatus = response.ThirdStatus;
            viewModel.ThirdType = GetPaymentType(response.ThirdType);
            viewModel.FourthReceiptId = response.FourthReceiptId;
            viewModel.FourthStatus = response.FourthStatus;
            viewModel.FourthType = GetPaymentType(response.FourthType);

            ViewBag.LogoPath = ConfigurationManager.AppSettings["CompanyLogo"] +
                               viewModel.CompanyProfile.CompanyLogoPath;
            ViewBag.EmployeeNameE = response.EmployeeNameE;
            ViewBag.EmployeeNameA = response.EmployeeNameA;
            return View(viewModel);
        }

        #endregion

        #region Create Receipt

        public ActionResult CreateReceipt(Receipt receipt)
        {
            long receiptId;
            receipt.IsPaid = true;
            if (receipt.ReceiptId > 0)
            {
                // update receipt
                EPMS.Models.DomainModels.Receipt receiptToUpdate = new EPMS.Models.DomainModels.Receipt
                {
                    ReceiptId = receipt.ReceiptId,
                    AmountPaid = receipt.AmountPaid,
                    IsPaid = receipt.IsPaid,
                    InvoiceId = receipt.InvoiceId,
                    InstallmentNumber = receipt.InstallmentNumber
                };
                receiptId = receiptService.UpdateReceiptByAdmin(receiptToUpdate);
            }
            else
            {
                // generate receipt
                receipt.PaymentType = (short) PaymentType.OffLine;
                receipt.RecCreatedBy = User.Identity.GetUserId();
                receipt.RecCreatedDt = DateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("en"));
                receipt.RecLastUpdatedBy = User.Identity.GetUserId();
                receipt.RecLastUpdatedDt = DateTime.Now;
                receiptId = receiptService.GenerateReceipt(receipt.CreateFromClientToServer());
            }
            return Json(receiptId);
        }

        #endregion

        private string GetPaymentType(int type)
        {
            switch (type)
            {
                case 1:
                    return WebModels.Resources.WebsiteClient.ShoppingCart.Cart.Pending;
                case 2:
                    return WebModels.Resources.WebsiteClient.ShoppingCart.Cart.Paypal;
                case 3:
                    return WebModels.Resources.WebsiteClient.ShoppingCart.Cart.Offline;
                case 4:
                    return WebModels.Resources.WebsiteClient.ShoppingCart.Cart.OnDelivery;
            }
            return "";
        }
        #endregion
    }
}