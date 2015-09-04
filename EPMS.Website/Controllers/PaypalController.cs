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
        public ActionResult PostTotPaypal(ShoppingCartListViewModel model)
        {
            Paypal paypal = new Paypal
            {
                cmd = "_cart",
                business = ConfigurationManager.AppSettings["BusinessAccountKey"],
                upload = "1"
            };
            bool useSandbox = Convert.ToBoolean(ConfigurationManager.AppSettings["UseSandbox"]);
            ViewBag.actionURL = useSandbox ? "https://www.sandbox.paypal.com/cgi-bin/webscr" : "https://www.paypal.com/cgi-bin/webscr";
            paypal.cancel_return = ConfigurationManager.AppSettings["CancelURL"];
            paypal.return_url = ConfigurationManager.AppSettings["ReturnURL"];
            paypal.notify_url = ConfigurationManager.AppSettings["NotifyURL"];
            paypal.currency_code = ConfigurationManager.AppSettings["CurrencyCode"];

            ViewBag.CartItems = model.ShoppingCarts;
            return View(paypal);
        }

        public ActionResult RedirectFromPaypal()
        {
            var response = Request.QueryString;
            string transaction_id = response["tx"];
            string status = response["st"];
            string amount = response["amt"];
            string currency_code = response["cc"];
            string cm = response["cm"];
            string item_number = response["item_number"];
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