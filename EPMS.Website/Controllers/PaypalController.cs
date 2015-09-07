using System;
using System.Configuration;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.WebModels.ViewModels.Common;
using EPMS.WebModels.ViewModels.WebsiteClient;
using EPMS.WebModels.WebsiteModels;
using Microsoft.AspNet.Identity;

namespace EPMS.Website.Controllers
{
    public class PaypalController : Controller
    {
        #region Private
        private readonly IShoppingCartService cartService;
        
        #endregion

        #region Constructor

        public PaypalController(IShoppingCartService cartService)
        {
            this.cartService = cartService;
        }

        #endregion

        //// GET: Paypal
        //public ActionResult Index()
        //{
        //    ViewBag.MessageVM = TempData["message"] as MessageViewModel;
        //    return View();
        //}

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

            ViewBag.CartItem = model.ShoppingCart;
            return View(paypal);
        }

        public ActionResult RedirectFromPaypal()
        {
            var response = Request.QueryString;
            string transactionId = response["tx"];
            string status = response["st"];
            string amount = response["amt"];
            string currencyCode = response["cc"];
            //string cm = response["cm"];
            //string itemNumber = response["item_number"];
            var cart = cartService.FindByUserCartId(User.Identity.GetUserId());
            if (cart != null)
            {
                cart.TransactionId = transactionId;
                cart.Status = status == "Completed" ? true : false;
                cart.AmountPaid = Convert.ToDecimal(amount);
                cart.CurrencyCode = currencyCode;
                cartService.UpdateShoppingCart(cart);
            }
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