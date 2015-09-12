using System;
using System.Configuration;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.Common;
using EPMS.WebModels.ViewModels.Common;
using EPMS.WebModels.ViewModels.WebsiteClient;
using EPMS.WebModels.WebsiteModels;
using Microsoft.AspNet.Identity;

namespace EPMS.Website.Controllers
{
    [Authorize]
    public class PaypalController : BaseController
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
                cart.Status = status == "Completed" ? (int)PurchaseStatus.Completed : (int)PurchaseStatus.Error;
                cart.AmountPaid = Convert.ToDecimal(amount);
                cart.CurrencyCode = currencyCode;
                cartService.UpdateShoppingCart(cart);
            }
            TempData["message"] = new MessageViewModel
            {
                Message = "\nYour order has been successfully placed. You will be contacted soon by our Team", IsSaved = true
            };
            return RedirectToAction("Index","Home");
        }
        public ActionResult NotifyFromPaypal()
        {
            return View();
        }
        public ActionResult CancelFromPaypal()
        {
            return View();
        }

        [HttpPost]
        public ActionResult OfflinePayment(ShoppingCartListViewModel model)
        {
            var cart = cartService.FindByUserCartId(User.Identity.GetUserId());
            if (cart != null)
            {
                cart.Status = (int)PurchaseStatus.InProgress;
                cartService.UpdateShoppingCart(cart);
            }
            TempData["message"] = new MessageViewModel
            {
                Message = "\nYour order has been successfully placed. You will be contacted soon by our Team", IsSaved = true
            };
            return RedirectToAction("Index","Home");
        }

        [HttpPost]
        public ActionResult OnDeliveryPayment(ShoppingCartListViewModel model)
        {
            var cart = cartService.FindByUserCartId(User.Identity.GetUserId());
            if (cart != null)
            {
                cart.Status = (int)PurchaseStatus.InProgress;
                cartService.UpdateShoppingCart(cart);
            }
            TempData["message"] = new MessageViewModel
            {
                Message = "\nYour order has been successfully placed. You will be contacted soon by our Team", IsSaved = true
            };
            return RedirectToAction("Index", "Home");
        }
    }
}