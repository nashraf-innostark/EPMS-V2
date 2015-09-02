using System;
using System.Configuration;
using System.Data.Entity.Core.Mapping;
using System.Linq;
using System.Web.Mvc;
using EPMS.WebModels.ViewModels.WebsiteClient;
using EPMS.WebModels.WebsiteModels;
using EPMS.Website.Models;

namespace EPMS.Website.Controllers
{
    public class PaypalController : Controller
    {
        // GET: Paypal
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [MultiButton(MatchFormKey = "PaypalAction", MatchFormValue = "Pay with Paypal")]
        public ActionResult PostTotPaypal(ShoppingCartListViewModel model)
        { 
            Paypal paypal = new Paypal
            {
                cmd = "_xclick",
                business = ConfigurationManager.AppSettings["BusinessAccountKey"],
            };
            bool useSandbox = Convert.ToBoolean(ConfigurationManager.AppSettings["UseSandbox"]);
            ViewBag.actionURL = useSandbox ? "https://www.sandbox.paypal.com/cgi-bin/webscr" : "https://www.paypal.com/cgi-bin/webscr";
            paypal.cancel_return = ConfigurationManager.AppSettings["CancelURL"];
            paypal.@return = ConfigurationManager.AppSettings["ReturnURL"];
            paypal.notify_url = ConfigurationManager.AppSettings["NotifyURL"];
            paypal.currency_code = ConfigurationManager.AppSettings["CurrencyCode"];

            paypal.item_name = model.ShoppingCarts.FirstOrDefault().ItemNameEn;
            paypal.amount = model.GrandTotal.ToString();
            paypal.quantity = model.ShoppingCarts.FirstOrDefault().Quantity.ToString();
            ViewBag.CartItems = model.ShoppingCarts;
            return View(paypal);
        }

        public ActionResult RedirectFromPaypal()
        {
            return View();
        }
        public ActionResult NotifyFromPaypal()
        {
            return View();
        }
        public ActionResult CancelFromPaypal()
        {
            return View();
        }
    }
}