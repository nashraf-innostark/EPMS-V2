using System;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Web.Controllers;
using EPMS.WebModels.ModelMappers;
using EPMS.WebModels.ViewModels.Payment;
using EPMS.WebModels.ViewModels.WebsiteClient;
using EPMS.WebModels.WebsiteModels;

namespace EPMS.Web.Areas.CMS.Controllers
{
    public class PaymentController : BaseController
    {
        #region Private

        private readonly IInvoiceService invoiceService;

        #endregion

        #region Constructor

        public PaymentController(IInvoiceService invoiceService)
        {
            this.invoiceService = invoiceService;
        }

        #endregion

        #region Public
        public ActionResult Index(long? id, string ins)
        {
            if (id != null)
            {
                ViewBag.InvoiceId = (long) id;
                ViewBag.Ins = ins;
            }
            return View();
        }

        public ActionResult PostTotPaypal(long id, string ins)
        {
            PaypalViewModel model = new PaypalViewModel
            {
                InvoiceId = id,
                Name = "Installment " + ins,
                InstallmentNo = Convert.ToInt32(ins)
            };
            model.Paypal = new Paypal
            {
                cmd = "_cart",
                business = ConfigurationManager.AppSettings["BusinessAccountKey"],
                upload = "1"
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
            string amount = response["amt"];
            string currencyCode = response["cc"];
            string data = response["cm"];
            long invoiceId = Convert.ToInt64(data.Split('-')[0]);
            int installmentNo = Convert.ToInt32(data.Split('-')[1]);
            EPMS.Models.DomainModels.Receipt receipt = new EPMS.Models.DomainModels.Receipt
            { 
                
            };
            ////string itemNumber = response["item_number"];
            //var cart = cartService.FindByUserCartId(User.Identity.GetUserId());
            //if (cart != null)
            //{
            //    cart.TransactionId = transactionId;
            //    cart.Status = status == "Completed" ? (int)PurchaseStatus.Completed : (int)PurchaseStatus.Error;
            //    cart.AmountPaid = Convert.ToDecimal(amount);
            //    cart.CurrencyCode = currencyCode;
            //    cartService.UpdateShoppingCart(cart);
            //}
            //TempData["message"] = new MessageViewModel
            //{
            //    Message = "\nYour order has been successfully placed. You will be contacted soon by our Team",
            //    IsSaved = true
            //};
            return RedirectToAction("Detail", "Receipt", new { id = 1 });
        }
        public ActionResult NotifyFromPaypal()
        {
            return View();
        }
        public ActionResult CancelFromPaypal()
        {
            return View();
        }

        #endregion
    }
}