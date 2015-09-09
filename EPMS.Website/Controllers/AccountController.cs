using System.Collections.Generic;
using System.Reflection;
using System.Web.Security;
using EPMS.Implementation.Identity;
using EPMS.Interfaces.IServices;
using EPMS.Models.Common;
using EPMS.Models.DomainModels;
using EPMS.Models.IdentityModels.ViewModels;
using EPMS.Models.RequestModels;
using EPMS.WebModels.ModelMappers.Website.ShoppingCart;
using WebModels = EPMS.WebModels.WebsiteModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EPMS.WebModels.ViewModels.Common;

namespace EPMS.Website.Controllers
{
    //[Authorize]
    public class AccountController : Controller
    {
        #region Private

        private readonly IAspNetUserService aspNetUserService;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        private readonly IWebsiteCustomerService websiteCustomerService;
        private readonly IShoppingCartService cartService;

        #endregion

        #region Constructor

        public AccountController(IWebsiteCustomerService websiteCustomerService, IAspNetUserService aspNetUserService, IShoppingCartService cartService)
        {
            this.websiteCustomerService = websiteCustomerService;
            this.aspNetUserService = aspNetUserService;
            this.cartService = cartService;
        }

        #endregion

        #region Public

        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
        }

        public ApplicationRoleManager RoleManager
        {
            get { return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>(); }
        }

        #region Change Password
        //public ActionResult ChangePassword()
        //{
        //    return View();
        //}
        //// POST: /Account/ChangePassword
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }
        //    var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
        //    if (result.Succeeded)
        //    {
        //        var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
        //        if (user != null)
        //        {
        //            await SignInAsync(user, false);
        //        }
        //        //return RedirectToAction("Index", new { Message = IdentitySample.Controllers.ManageController.ManageMessageId.ChangePasswordSuccess });
        //        //return RedirectToAction("Index", "Dashboard");
        //        ViewBag.MessageVM = new MessageViewModel { Message = "Password has been updated.", IsUpdated = true };

        //        return View();
        //    }
        //    ViewBag.MessageVM = new MessageViewModel { Message = "Incorrect old Password", IsError = true };
        //    AddErrors(result);
        //    return View(model);
        //}
        private async Task SignInAsync(AspNetUser user, bool isPersistent)
        {
            //AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie);
            //AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, await user.GenerateUserIdentityAsync(UserManager));
            //AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            //var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

            //await SetExternalProperties(identity);

            //AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);

            //await SaveAccessToken(user, identity);
        }
        #endregion

        #region Login & Logoff
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(WebCustomerIdentityViewModel model)
        {
            string returnUrl = Request.UrlReferrer.PathAndQuery;
            try
            {
                var user = await UserManager.FindByNameAsync(model.Login.UserName);
                if (user != null)
                {
                    if (!await UserManager.IsEmailConfirmedAsync(user.Id))
                    {
                        TempData["message"] = new MessageViewModel { Message = "\nEmail not Confirmed", IsError = true };
                        if (string.IsNullOrEmpty(returnUrl))
                            return RedirectToAction("Index", "Home");
                        return RedirectToLocal(returnUrl);
                    }
                }
                // This doen't count login failures towards lockout only two factor authentication
                // To enable password failures to trigger lockout, change to shouldLockout: true
                var result =
                    await
                        SignInManager.PasswordSignInAsync(model.Login.UserName, model.Login.Password, model.Login.RememberMe,
                            shouldLockout: false);

                switch (result)
                {
                    case SignInStatus.Success:
                        {
                            SaveCartToDB(user.Id);
                            if (string.IsNullOrEmpty(returnUrl))
                                return RedirectToAction("Index", "Home");
                            return RedirectToLocal(returnUrl);
                        }
                    default:
                        TempData["message"] = new MessageViewModel { Message = "\nInvalid login attempt.", IsError = true };
                        if (string.IsNullOrEmpty(returnUrl))
                            return RedirectToAction("Index", "Home", new { div = "login_panel", width = "800" });
                        return RedirectToLocal(returnUrl);
                }
            }
            catch (Exception ex)
            {
                if (string.IsNullOrEmpty(returnUrl))
                    return RedirectToAction("Index", "Home", new { div = "login_panel", width = "800" });
                return RedirectToLocal(returnUrl);
            }
        }
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            Session.Abandon();
            if (Request.UrlReferrer != null)
            {
                string returnUrl = Request.UrlReferrer.PathAndQuery;
                return RedirectToLocal(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
        private void SaveCartToDB(string userId)
        {
            Session["ShoppingCartId"] = userId;
            var items = Session["ShoppingCartItems"];
            if (items != null)
            {
                WebModels.WebsiteModels.ShoppingCart cart = (WebModels.WebsiteModels.ShoppingCart)Session["ShoppingCartItems"];
                cart.UserCartId = userId;
                cart.Status = (int)PurchaseStatus.New;
                cart.RecCreatedBy = userId;
                cart.RecCreatedDate = DateTime.Now;
                cart.RecLastUpdatedBy = userId;
                cart.RecLastUpdatedDate = DateTime.Now;
                var cartToSave = cart.CreateFromClientToServer();
                ShoppingCartSearchRequest request = new ShoppingCartSearchRequest
                {
                    UserCartId = userId,
                    ShoppingCart = cartToSave
                };
                cartService.SyncShoppingCart(request);
            }
        }
        #endregion

        #region Register

        [AllowAnonymous]
        public ActionResult Signup()
        {
            var viewModel = new WebCustomerIdentityViewModel
            {
                SignUp = new CustomerSignUpViewModel()
            };
            return View(viewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        //[EPMS.WebBase.Mvc.SiteAuthorize(PermissionKey = "UserAddEdit")]
        public async Task<ActionResult> Signup(WebCustomerIdentityViewModel viewModel)
        {
            if (Request.Form["fromSignUp"] == null || Request.Form["fromSignUp"] != "SignUp")
            {
                return View();
            }
            var users = aspNetUserService.GetAllUsers().ToList();
            if (users.Any())
            {
                var usernames = users.Select(x => x.UserName);
                if (usernames.Contains(viewModel.SignUp.UserName))
                {
                    // it means username is already taken
                    TempData["message"] = new MessageViewModel { Message = "\nUserName already taken. Please try other one.", IsError = true };
                    return RedirectToAction("Index", "Home", new { div = "register_panel", width = "280" });
                }
                var emails = users.Select(x => x.Email);
                if (emails.Contains(viewModel.SignUp.Email))
                {
                    // it means email is already taken
                    TempData["message"] = new MessageViewModel { Message = "\nEmail already taken. Please try other one.", IsError = true };
                    return RedirectToAction("Index", "Home", new { div = "register_panel", width = "280" });
                }
            }

            #region Add Customer

            WebsiteCustomer customer = new WebsiteCustomer
            {
                CustomerNameEn = viewModel.SignUp.CustomerNameEn,
                CustomerNameAr = viewModel.SignUp.CustomerNameEn,
            };
            bool status = websiteCustomerService.AddWebsiteCustomer(customer);

            #endregion

            if (status)
            {
                var user = new AspNetUser { UserName = viewModel.SignUp.UserName, Email = viewModel.SignUp.Email };
                user.WebsiteCustomerId = customer.CustomerId;
                if (!String.IsNullOrEmpty(viewModel.SignUp.Password))
                {
                    var result = await UserManager.CreateAsync(user, viewModel.SignUp.Password);
                    if (result.Succeeded)
                    {
                        var code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code },
                            protocol: Request.Url.Scheme);
                        await
                            UserManager.SendEmailAsync(viewModel.SignUp.Email, "Confirm your account",
                                "Please confirm your account by clicking this link: <a href=\"" + callbackUrl +
                                "\">link</a><br>Your Password is:" + viewModel.SignUp.Password);
                        ViewBag.Link = callbackUrl;

                        string message = "\nConfirmation Email has been sent to " + viewModel.SignUp.Email + " Please verify your account.";
                        TempData["message"] = new MessageViewModel { Message = message, IsSaved = true };

                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region Manage
        public ApplicationSignInManager SignInManager
        {
            get { return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>(); }
            private set { _signInManager = value; }
        }

        //
        // POST: /Account/Login


        public ActionResult Error()
        {
            return View("Error");
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            try
            {
                if (userId == null || code == null)
                {
                    TempData["message"] = new MessageViewModel { Message = "\nInvalid request.", IsError = true };
                    return RedirectToAction("Index", "Home");
                }
                var result = await UserManager.ConfirmEmailAsync(userId, code);
                if (result.Succeeded)
                {
                    TempData["message"] = new MessageViewModel { Message = "\nEmail has been Confirmed successfully. Please login to your account to have access to different features.", IsUpdated = true };
                    return RedirectToAction("Index", "Home", new { div = "login_panel", width = "800" });
                }
                //return View(result.Succeeded ? "ConfirmEmail" : "Error");
                TempData["message"] = new MessageViewModel { Message = "\nInvalid request.", IsError = true };
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                TempData["message"] = new MessageViewModel { Message = "\nInvalid request.", IsError = true };
                return RedirectToAction("Index", "Home");
            }
        }

        //
        // GET: /Account/`Password
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> ForgotPassword(WebCustomerIdentityViewModel model)
        {
            //if (ModelState.IsValid)
            //{
            var user = await UserManager.FindByNameAsync(model.ForgotPassword.UserName);
            if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
            {
                TempData["message"] = new MessageViewModel { Message = "\nUser not found or Email not Confirmed", IsError = true };

                // Don't reveal that the user does not exist or is not confirmed
                return RedirectToAction("Index", "Home");
            }

            var code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
            var callbackUrl = Url.Action("Index", "Home", new { div = "resetpassword_panel", width = "350", code = code, userId = user.Id }, Request.Url.Scheme);
            await
                UserManager.SendEmailAsync(user.Email, "Reset Password",
                    "Please reset your password by clicking here: <a href=\"" + callbackUrl + "\">link</a>");
            ViewBag.Link = callbackUrl;
            TempData["message"] = new MessageViewModel { Message = "\nAn email with Password link has been sent.", IsUpdated = true };
            return RedirectToAction("Index", "Home");
            //}
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> ResetPassword(WebCustomerIdentityViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home", new { div = "resetpassword_panel", width = "350" });
            }

            var result = await UserManager.ResetPasswordAsync(model.ResetPassword.UserId, model.ResetPassword.Code, model.ResetPassword.Password);
            if (result.Succeeded)
            {
                TempData["message"] = new MessageViewModel { Message = "\nPassword has been updated.", IsUpdated = true };
                return RedirectToAction("Index", "Home", new { div = "login_panel", width = "800" });
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        #endregion

        private async Task SaveAccessToken(AspNetUser user, ClaimsIdentity identity)
        {
            var userclaims = await UserManager.GetClaimsAsync(user.Id);

            foreach (var at in (
                from claims in identity.Claims
                where claims.Type.EndsWith("access_token")
                select new Claim(claims.Type, claims.Value, claims.ValueType, claims.Issuer)))
            {

                if (!userclaims.Contains(at))
                {
                    await UserManager.AddClaimAsync(user.Id, at);
                }
            }
        }

        #endregion

        #region Helpers

        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }

        #endregion

        #region Validate Username

        [HttpPost]
        public JsonResult ValidateUserName(string username)
        {
            var users = aspNetUserService.GetAllUsers().ToList();
            if (users.Any())
            {
                if (!string.IsNullOrEmpty(username))
                {
                    var usernames = users.Select(x => x.UserName);
                    if (usernames.Contains(username))
                    {
                        // it means username is already taken
                        const string message = "\nUserName already taken. Please try other one.";
                        return Json(message, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Validate Email

        [HttpPost]
        public JsonResult ValidateEmail(string email)
        {
            var users = aspNetUserService.GetAllUsers().ToList();
            if (users.Any())
            {
                if (!string.IsNullOrEmpty(email))
                {
                    var emails = users.Select(x => x.Email);
                    if (emails.Contains(email))
                    {
                        // it means email is already taken
                        const string message = "\nEmail already taken. Please try other one.";
                        return Json(message, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        #endregion

        private void updateSessionValues(AspNetUser user)
        {
            AspNetUser result = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(User.Identity.GetUserId());
            string role = HttpContext.GetOwinContext().Get<ApplicationRoleManager>().FindById(result.AspNetRoles.ToList()[0].Id).Name;
            //Session["FullName"] = result.Employee.EmployeeFirstName + " " + result.Employee.EmployeeLastName;
            Session["UserID"] = result.Id;
            Session["RoleName"] = role;
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class MultiButtonAttribute : ActionNameSelectorAttribute
    {
        public string MatchFormKey { get; set; }
        public string MatchFormValue { get; set; }

        public override bool IsValidName(ControllerContext controllerContext, string actionName, MethodInfo methodInfo)
        {
            return controllerContext.HttpContext.Request[MatchFormKey] != null &&
                controllerContext.HttpContext.Request[MatchFormKey] == MatchFormValue;
        }
    }
}