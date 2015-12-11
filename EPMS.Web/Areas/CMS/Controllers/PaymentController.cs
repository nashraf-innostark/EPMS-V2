using System;
using System.Configuration;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.Common;
using EPMS.Web.Controllers;
using EPMS.WebModels.ModelMappers;
using EPMS.WebModels.ViewModels.Common;
using EPMS.WebModels.ViewModels.Payment;
using EPMS.WebModels.WebsiteModels;

namespace EPMS.Web.Areas.CMS.Controllers
{
    public class PaymentController : BaseController
    {
        #region Private

        private readonly IInvoiceService invoiceService;
        private readonly IReceiptService receiptService;

        #endregion

        #region Constructor

        public PaymentController(IInvoiceService invoiceService, IReceiptService receiptService)
        {
            this.invoiceService = invoiceService;
            this.receiptService = receiptService;
        }

        #endregion

        #region Public
        public ActionResult Index(long? id, string ins)
        {
            if (id != null)
            {
                ViewBag.InvoiceId = (long) id;
                ViewBag.Ins = ins;
                PaypalViewModel model = new PaypalViewModel
                {
                    InvoiceId = (long) id,
                    Name = "Installment " + ins,
                    InstallmentNo = Convert.ToInt32(ins),
                    Paypal = new Paypal
                    {
                        cmd = "_cart",
                        business = ConfigurationManager.AppSettings["BusinessAccountKey"],
                        upload = "1"
                    }
                };
                bool useSandbox = Convert.ToBoolean(ConfigurationManager.AppSettings["UseSandbox"]);
                ViewBag.actionURL = useSandbox ? "https://www.sandbox.paypal.com/cgi-bin/webscr" : "https://www.paypal.com/cgi-bin/webscr";
                model.Paypal.cancel_return = ConfigurationManager.AppSettings["CancelURL"];
                model.Paypal.return_url = ConfigurationManager.AppSettings["ReturnURL"];
                model.Paypal.notify_url = ConfigurationManager.AppSettings["NotifyURL"];
                model.Paypal.currency_code = ConfigurationManager.AppSettings["CurrencyCode"];

                Invoice invoice = invoiceService.FindInvoiceById((long)id).CreateForPayment(ins);
                //ViewBag.Items = invoice.Quotation.QuotationItemDetails.ToList();
                model.Amount = Math.Round(invoice.Quotation.GrandTotal, 2, MidpointRounding.AwayFromZero);
                return View(model);
            }
            return View();
        }

        public ActionResult PostTotPaypal(long id, string ins)
        {
            PaypalViewModel model = new PaypalViewModel
            {
                InvoiceId = id,
                Name = "Installment " + ins,
                InstallmentNo = Convert.ToInt32(ins),
                Paypal = new Paypal
                {
                    cmd = "_cart",
                    business = ConfigurationManager.AppSettings["BusinessAccountKey"],
                    upload = "1"
                }
            };
            bool useSandbox = Convert.ToBoolean(ConfigurationManager.AppSettings["UseSandbox"]);
            ViewBag.actionURL = useSandbox ? "https://www.sandbox.paypal.com/cgi-bin/webscr" : "https://www.paypal.com/cgi-bin/webscr";
            model.Paypal.cancel_return = ConfigurationManager.AppSettings["CancelURL"];
            model.Paypal.return_url = ConfigurationManager.AppSettings["ReturnURL"];
            model.Paypal.notify_url = ConfigurationManager.AppSettings["NotifyURL"];
            model.Paypal.currency_code = ConfigurationManager.AppSettings["CurrencyCode"];

            Invoice invoice = invoiceService.FindInvoiceById(id).CreateForPayment(ins);
            //ViewBag.Items = invoice.Quotation.QuotationItemDetails.ToList();
            model.Amount = Math.Round(invoice.Quotation.GrandTotal, 2, MidpointRounding.AwayFromZero);
            return View(model);
        }

        public ActionResult RedirectFromPaypal()
        {
            var response = Request.QueryString;
            string transactionId = response["tx"];
            string status = response["st"];
            decimal amount = Convert.ToDecimal(response["amt"]);
            string currencyCode = response["cc"];
            string data = response["cm"];
            long invoiceId = Convert.ToInt64(data.Split('-')[0]);
            int installmentNo = Convert.ToInt32(data.Split('-')[1]);
            EPMS.Models.DomainModels.Receipt receiptToAdd = new EPMS.Models.DomainModels.Receipt
            { 
                InvoiceId = invoiceId,
                AmountPaid = amount,
                InstallmentNumber = installmentNo,
                PaymentType = (short)PaymentType.Paypal,
                IsPaid = status == "Completed",
                Paypal = new EPMS.Models.DomainModels.Paypal
                {
                    TransactionId = transactionId,
                    CurrencyCode = currencyCode
                }
            };
            long receiptId = receiptService.AddReceipt(receiptToAdd);
            if (receiptId > 0)
            {
                TempData["message"] = new MessageViewModel
                {
                    Message = WebModels.Resources.CMS.Receipt.AddMessage,
                    IsSaved = true
                };
                return RedirectToAction("Detail", "Receipt", new { id = receiptId, area = "CMS" });
            }
            TempData["message"] = new MessageViewModel
            {
                Message = WebModels.Resources.CMS.Receipt.ErrorMessage,
                IsError = true
            };
            return RedirectToAction("Index", "Receipt" , new { area = "CMS" });
        }
        public ActionResult NotifyFromPaypal()
        {
            return View();
        }
        public ActionResult CancelFromPaypal()
        {
            return View();
        }

        public ActionResult Manual(long id, string ins, string type)
        {
            EPMS.Models.DomainModels.Receipt receiptToAdd = new EPMS.Models.DomainModels.Receipt
            {
                InvoiceId = id,
                InstallmentNumber = Convert.ToInt32(ins),
                IsPaid = false
            };
            if (type == "1")
            {
                receiptToAdd.PaymentType = (short) PaymentType.OffLine;
            }
            else if (type == "2")
            {
                receiptToAdd.PaymentType = (short) PaymentType.OnDelivery;
            }
            long receiptId = receiptService.AddReceipt(receiptToAdd);
            if (receiptId > 0)
            {
                TempData["message"] = new MessageViewModel
                {
                    Message = WebModels.Resources.CMS.Receipt.AddMessage,
                    IsSaved = true
                };
                return RedirectToAction("Detail", "Invoice", new { area = "CMS", id= id });
            }
            TempData["message"] = new MessageViewModel
            {
                Message = WebModels.Resources.CMS.Receipt.ErrorMessage,
                IsError = true
            };
            return RedirectToAction("Detail", "Invoice", new { area = "CMS", id = id });
        }

        #endregion
    }
}