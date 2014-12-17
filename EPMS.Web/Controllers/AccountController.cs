﻿using EPMS.Implementation.Identity;
using EPMS.Interfaces.IServices;
using EPMS.Models.DomainModels;
using EPMS.Models.IdentityModels.ViewModels;
using EPMS.Web.Models;
using IdentitySample.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.Admin;
using EPMS.Models.ModelMapers;

namespace IdentitySample.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        #region Private

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private IMenuRightsService menuRightService;
        private IEmployeeService employeeService;
        private ApplicationRoleManager _roleManager;


        /// <summary>
        /// Set User Permission
        /// </summary>
        private void SetUserPermissions(string userEmail)
        {
            //ClaimsIdentity userIdentity = (ClaimsIdentity) User.Identity;
            //IEnumerable<Claim> claims = userIdentity.Claims;
            //string roleClaimType = userIdentity.RoleClaimType;

            //IEnumerable<Claim> roles = claims.Where(c => c.Type == roleClaimType).ToList();
            try
            {
                AspNetUser userResult = UserManager.FindByEmail(userEmail);
                IList<AspNetRole> roles = userResult.AspNetRoles.ToList();
                IList<EPMS.Models.MenuModels.MenuRight> userRights = menuRightService.FindMenuItemsByRoleId(roles[0].Id).ToList();

                string[] userPermissions = userRights.Select(user => user.Menu.PermissionKey).ToArray();
                Session["UserPermissionSet"] = userPermissions;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        #endregion

        #region Constructor

        public AccountController(IMenuRightsService menuRightService, IEmployeeService employeeService)
        {
            this.menuRightService = menuRightService;
            this.employeeService = employeeService;
        }

        #endregion

        #region Public

        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }

        public ApplicationRoleManager RoleManager
        {
            get { return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>(); }
            private set { _roleManager = value; }
        }

        #region Change Password
        public ActionResult ChangePassword()
        {
            return View();
        }
        // POST: /Account/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInAsync(user, isPersistent: false);
                }
                //return RedirectToAction("Index", new { Message = IdentitySample.Controllers.ManageController.ManageMessageId.ChangePasswordSuccess });
                //return RedirectToAction("Index", "Dashboard");
                ViewBag.MessageVM = new MessageViewModel { Message = "Password has been updated.", IsUpdated = true };

                return View();
            }
            AddErrors(result);
            return View(model);
        }
        private async Task SetExternalProperties(ClaimsIdentity identity)
        {
            // get external claims captured in Startup.ConfigureAuth
            ClaimsIdentity ext = await AuthenticationManager.GetExternalIdentityAsync(DefaultAuthenticationTypes.ExternalCookie);

            if (ext != null)
            {
                var ignoreClaim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims";
                // add external claims to identity
                foreach (var c in ext.Claims)
                {
                    if (!c.Type.StartsWith(ignoreClaim))
                        if (!identity.HasClaim(c.Type, c.Value))
                            identity.AddClaim(c);
                }
            }
        }
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
            if (!User.Identity.IsAuthenticated)
            {
                ViewBag.ReturnUrl = returnUrl;
                ViewBag.MessageVM = TempData["message"] as MessageViewModel;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            try
            {
                //if (string.IsNullOrEmpty(returnUrl))
                //    returnUrl = "/Home/Index";
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user != null)
                {
                    if (!await UserManager.IsEmailConfirmedAsync(user.Id))
                    {
                        ModelState.AddModelError("", "Email not confirmed");
                        return View(model);
                    }
                }
                // This doen't count login failures towards lockout only two factor authentication
                // To enable password failures to trigger lockout, change to shouldLockout: true
                var result =
                    await
                        SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe,
                            shouldLockout: false);


                SetUserPermissions(model.Email);


                switch (result)
                {
                    case SignInStatus.Success:
                        {
                            //return RedirectToAction("Index", "Admin");
                            return RedirectToLocal(returnUrl);
                        }
                    case SignInStatus.LockedOut:
                        return View("Lockout");
                    case SignInStatus.RequiresVerification:
                        return RedirectToAction("SendCode", new { ReturnUrl = returnUrl });
                    case SignInStatus.Failure:
                    default:
                        ModelState.AddModelError("", "Invalid login attempt.");
                        return View(model);
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");

            }
        }
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }
        #endregion

        #region Register
        //
        // GET: /Account/Register
        [AllowAnonymous]
        [EPMS.WebBase.Mvc.SiteAuthorize(PermissionKey = "UserAddEdit")]
        public ActionResult Create(string email)
        {
            RegisterViewModel oResult = new RegisterViewModel();
            if (!string.IsNullOrEmpty(email))
            {
                AspNetUser userToEdit = UserManager.FindByEmail(email);
                oResult = new RegisterViewModel
                {
                    UserId = userToEdit.Id,
                    SelectedRole = userToEdit.AspNetRoles.ToList()[0].Id,
                    Employees = employeeService.GetAll().ToList()

                };
                //oResult.Roles = new RoleManager<Microsoft.AspNet.Identity.EntityFramework.IdentityRole>(new RoleStore<IdentityRole>()).Roles.ToList();
                oResult.Roles = HttpContext.GetOwinContext().Get<ApplicationRoleManager>().Roles.ToList();

                return View(oResult);
            }
            //oResult.Roles = new RoleManager<Microsoft.AspNet.Identity.EntityFramework.IdentityRole>(new RoleStore<IdentityRole>()).Roles.ToList();
            oResult.Roles = HttpContext.GetOwinContext().Get<ApplicationRoleManager>().Roles.ToList();
            oResult.Employees = employeeService.GetAll().Select(x => x.ServerToServer()).ToList();

            return View(oResult);
        }


        [AllowAnonymous]
        [EPMS.WebBase.Mvc.SiteAuthorize(PermissionKey = "User")]
        public ActionResult Users()
        {
            //if (Session["LoginID"] == null)
            //{
            //    return RedirectToAction("Login");
            //}
            List<AspNetUser> oList = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().Users.ToList();
            //var roleManager = new RoleManager<Microsoft.AspNet.Identity.EntityFramework.IdentityRole>(new RoleStore<IdentityRole>());
            var roleManager = HttpContext.GetOwinContext().Get<ApplicationRoleManager>();

            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            UserViewModel oVM = new UserViewModel();
            oVM.Data = new List<SystemUser>();
            foreach (var item in oList)
            {
                if (item.EmployeeId>0)
                {
                    oVM.Data.Add(new SystemUser
                    {
                        EmailConfirmed = item.EmailConfirmed,
                        Email = item.Email,
                        FirstName = item.Employee.EmployeeFirstName,
                        KeyId = item.Id,
                        LastName = item.Employee.EmployeeLastName,
                        Role = roleManager.FindById(item.AspNetRoles.ToList()[0].Id).Name
                    });
                }
            }
            return View(oVM);
        }


        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [EPMS.WebBase.Mvc.SiteAuthorize(PermissionKey = "UserAddEdit")]
        public async Task<ActionResult> Create(RegisterViewModel model)
        {

            if (!string.IsNullOrEmpty(model.UserId))
            {
                //means update case
                var roleManager = new RoleManager<Microsoft.AspNet.Identity.EntityFramework.IdentityRole>(new RoleStore<IdentityRole>());
                var roleName = roleManager.FindById(model.SelectedRole).Name;
                AspNetUser userResult = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindByEmail(model.Email);
                string userrRoleID = userResult.AspNetRoles.ToList()[0].Id;
                string userRoleName = roleManager.FindById(userrRoleID).Name;
                if (userrRoleID != model.SelectedRole)
                {//meanse change the role
                    UserManager.RemoveFromRole(model.UserId, userRoleName);

                    UserManager.AddToRole(model.UserId, roleName);
                    TempData["message"] = new MessageViewModel { Message = "User has been updated.", IsUpdated = true };

                }

                //UserManager.RemoveFromRoleAsync(User.Id, roleName);

                return RedirectToAction("RegisterLV");
            }

            if (ModelState.IsValid)
            {
                var user = new AspNetUser { UserName = model.UserName, Email = model.Email };

                var result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    //Setting role
                    //var roleManager = new RoleManager<Microsoft.AspNet.Identity.EntityFramework.IdentityRole>(new RoleStore<IdentityRole>());
                    var roleManager = HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
                    var roleName = roleManager.FindById(model.SelectedRole).Name;
                    UserManager.AddToRole(user.Id, roleName);
                    //UserManager.AddToRoleAsync(user.Id, roleName);

                    var code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code },
                        protocol: Request.Url.Scheme);
                    await
                        UserManager.SendEmailAsync(model.Email, "Confirm your account",
                            "Please confirm your account by clicking this link: <a href=\"" + callbackUrl +
                            "\">link</a><br>Your Password is:" + model.Password);
                    ViewBag.Link = callbackUrl;
                    TempData["message"] = new MessageViewModel { Message = "Registeration Confirmation email send to the user.", IsSaved = true };
                    return RedirectToAction("RegisterLV");
                }
                AddErrors(result);
                //model.Roles = new RoleManager<Microsoft.AspNet.Identity.EntityFramework.IdentityRole>(new RoleStore<IdentityRole>()).Roles.ToList();
                model.Employees = employeeService.GetAll().Select(x => x.ServerToServer()).ToList();
                model.Roles = HttpContext.GetOwinContext().Get<ApplicationRoleManager>().Roles.ToList();

            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }
        #endregion

        #region External Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider,
                Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation",
                        new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model,
            string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new AspNetUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
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
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            var user = await UserManager.FindByIdAsync(await SignInManager.GetVerifiedUserIdAsync());
            if (user != null)
            {
                ViewBag.Status = "For DEMO purposes the current " + provider + " code is: " +
                                 await UserManager.GenerateTwoFactorTokenAsync(user.Id, provider);
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result =
                await
                    SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: false,
                        rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
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
                    return View("Error");
                }
                var result = await UserManager.ConfirmEmailAsync(userId, code);
                //return View(result.Succeeded ? "ConfirmEmail" : "Error");
                return RedirectToAction("Login");
            }
            catch (Exception)
            {

                return RedirectToAction("Login");
            }
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    ModelState.AddModelError("", "Email not found.");
                    // Don't reveal that the user does not exist or is not confirmed
                    return View(model);
                }

                var code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code },
                    protocol: Request.Url.Scheme);
                await
                    UserManager.SendEmailAsync(model.Email, "Reset Password",
                        "Please reset your password by clicking here: <a href=\"" + callbackUrl + "\">link</a>");
                ViewBag.Link = callbackUrl;
                TempData["message"] = new MessageViewModel { Message = "An email with Password link has been sent.", IsUpdated = true };
                return RedirectToAction("Login");
                //return View("Login");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
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
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("Login", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                TempData["message"] = new MessageViewModel { Message = "Password has been updated.", IsUpdated = true };
                return RedirectToAction("Login", "Account");
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

        //
        // POST: /Account/ExternalLogin


        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions =
                userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl });
        }

        #endregion

        #region Profile Work
        [Authorize]
        public ActionResult Profile()
        {
            AspNetUser result = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindByEmail(User.Identity.Name);
            var ProfileViewModel = new ProfileViewModel
            {
                Email = result.Email,
                UserName = result.UserName
            };
            ViewBag.FilePath = ConfigurationManager.AppSettings["ProfileImage"] + ProfileViewModel.ImageName;//Server.MapPath
            return View(ProfileViewModel);
        }

        [HttpPost]
        [Authorize]
        [ValidateInput(false)]
        public ActionResult Profile(ProfileViewModel profileViewModel)
        {
            AspNetUser result = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindByEmail(User.Identity.Name);
            string savedFileName = "";
            //Save image to Folder
            if ((profileViewModel.UploadImage != null))
            {
                var filename = profileViewModel.UploadImage.FileName;
                var filePathOriginal = Server.MapPath(ConfigurationManager.AppSettings["ProfileImage"]);
                savedFileName = Path.Combine(filePathOriginal, filename);
                profileViewModel.ImageName = filename;

                profileViewModel.UploadImage.SaveAs(savedFileName);
            }

            //Updating Data
            try
            {
                result.UserName = profileViewModel.UserName;
                //result.Employee.EmployeeLastName = profileViewModel.LastName;
                result.Email = profileViewModel.Email;
                if (profileViewModel.ImageName != null)
                {
                    //result.ImageName = profileViewModel.ImageName;
                    profileViewModel.ImagePath = ConfigurationManager.AppSettings["ProfileImage"] as string + profileViewModel.ImageName;
                }
                else
                {
                    //profileViewModel.ImagePath = ConfigurationManager.AppSettings["ProfileImage"] as string + result.ImageName;
                }
                var updationResult = UserManager.UpdateAsync(result);
                ViewBag.MessageVM = new MessageViewModel { Message = "Profile has been updated", IsUpdated = true };


                //BaseController oBaseController = new BaseController();
                //oBaseController.SetUserDetail();
                updateSessionValues(result);
            }
            catch (Exception e)
            {
            }
            return View(profileViewModel);
        }

        #endregion

        private async Task SaveAccessToken(EPMS.Models.DomainModels.AspNetUser user, ClaimsIdentity identity)
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
        private void updateSessionValues(AspNetUser user)
        {
            AspNetUser result = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindByEmail(User.Identity.Name);
            string role = HttpContext.GetOwinContext().Get<ApplicationRoleManager>().FindById(result.AspNetRoles.ToList()[0].Id).Name;
            Session["FullName"] = result.Employee.EmployeeFirstName + " " + result.Employee.EmployeeLastName;
            Session["LoginID"] = result.Id;
            Session["RoleName"] = role;
        }
    }
}