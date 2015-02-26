﻿using EPMS.Implementation.Identity;
using EPMS.Interfaces.IServices;
using EPMS.Models.DomainModels;
using EPMS.Models.IdentityModels.ViewModels;
using EPMS.Web.Models;
using IdentitySample.Models;
using Microsoft.AspNet.Identity;
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
using System.Net;
using EPMS.Web.ModelMappers;
using EPMS.Models.ModelMapers;
using EPMS.WebBase.Mvc;
using System.Globalization;

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
        private IAspNetUserService AspNetUserService;
        private ICustomerService customerService;
        private IUserPrefrencesService userPrefrencesService;
        private IDashboardWidgetPreferencesService PreferencesService;
        /// <summary>
        /// Set User Permission
        /// </summary>
        private void SetUserPermissions(string userEmail)
        {
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
        private void SetCultureInfo(string userId)
        {
            var userPrefrences = userPrefrencesService.LoadPrefrencesByUserId(userId);
            CultureInfo info = userPrefrences != null
                ? new CultureInfo(userPrefrences.Culture)
                : new CultureInfo("en");
            Session["Culture"] = info.Name;
            info.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            System.Threading.Thread.CurrentThread.CurrentCulture = info;
            System.Threading.Thread.CurrentThread.CurrentUICulture = info;
        }
        #endregion

        #region Constructor

        public AccountController(IDashboardWidgetPreferencesService preferencesService, IUserPrefrencesService userPrefrencesService,IMenuRightsService menuRightService, IEmployeeService employeeService, IAspNetUserService aspNetUserService, ICustomerService customerService)
        {
            this.menuRightService = menuRightService;
            this.employeeService = employeeService;
            this.AspNetUserService = aspNetUserService;
            this.customerService = customerService;
            this.userPrefrencesService = userPrefrencesService;
            this.PreferencesService = preferencesService;

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
                ViewBag.MessageVM = new MessageViewModel {Message = "Password has been updated.", IsUpdated = true};

                return View();
            }
            else
            {
                ViewBag.MessageVM = new MessageViewModel { Message = "Incorrect old Password", IsError = true };
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
                return RedirectToAction("Index", "Dashboard");
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





                switch (result)
                {
                    case SignInStatus.Success:
                        {
                            SetUserPermissions(user.Email);
                            SetCultureInfo(user.Id);
                            if (string.IsNullOrEmpty(returnUrl))
                                return RedirectToAction("Index", "Dashboard");
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
        [SiteAuthorize(PermissionKey = "UserCreate")]
        public ActionResult Create(string userName)
        {
            RegisterViewModel Result = new RegisterViewModel();
            // Check allowed no of users
            // check license
            var licenseKeyEncrypted = ConfigurationManager.AppSettings["LicenseKey"].ToString(CultureInfo.InvariantCulture);
            var LicenseKey = EPMS.Web.EncryptDecrypt.StringCipher.Decrypt(licenseKeyEncrypted, "123");
            var splitLicenseKey = LicenseKey.Split('|');
            var NoOfUsers = Convert.ToInt32(splitLicenseKey[2]);
            // get count od users
            var countOfUsers = UserManager.Users.Count();
            if (countOfUsers < NoOfUsers)
            {
                if (!string.IsNullOrEmpty(userName))
                {
                    AspNetUser userToEdit = UserManager.FindByName(userName);
                    Result = new RegisterViewModel
                    {
                        UserId = userToEdit.Id,
                        SelectedRole = userToEdit.AspNetRoles.ToList()[0].Id,
                        SelectedEmployee = userToEdit.EmployeeId ?? 0,
                        UserName = userToEdit.UserName,
                        Email = userToEdit.Email,
                        //oldRole = userToEdit.AspNetRoles.ToList()[0].Id
                    };
                    //oResult.Roles = new RoleManager<Microsoft.AspNet.Identity.EntityFramework.IdentityRole>(new RoleStore<IdentityRole>()).Roles.ToList();
                    Result.Roles = RoleManager.Roles.Where(r => !r.Name.Equals("SuperAdmin")).OrderBy(r => r.Name).ToList();
                    Result.Employees = employeeService.GetAll().Select(x => x.ServerToServer()).ToList();
                    return View(Result);
                }
                //oResult.Roles = new RoleManager<Microsoft.AspNet.Identity.EntityFramework.IdentityRole>(new RoleStore<IdentityRole>()).Roles.ToList();
                Result.Roles = RoleManager.Roles.Where(r => !r.Name.Equals("SuperAdmin")).OrderBy(r => r.Name).ToList();
                Result.Employees = employeeService.GetAll().Select(x => x.ServerToServer()).ToList();
            }
            return View(Result);
        }


        [AllowAnonymous]
        [SiteAuthorize(PermissionKey = "User")]
        public ActionResult Users()
        {
            //if (Session["UserID"] == null)
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
                if (item.EmployeeId > 0)
                {
                    oVM.Data.Add(new SystemUser
                    {
                        EmailConfirmed = item.EmailConfirmed,
                        Email = item.Email,
                        FirstName = item.Employee.EmployeeNameE,
                        KeyId = item.Id,
                        Role = roleManager.FindById(item.AspNetRoles.ToList()[0].Id).Name,
                        Username = item.UserName
                    });
                }
            }
            return View(oVM);
        }


        //
        // POST: /Account/Create
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        //[EPMS.WebBase.Mvc.SiteAuthorize(PermissionKey = "UserAddEdit")]
        public async Task<ActionResult> Create(RegisterViewModel model)
        {
            if (!string.IsNullOrEmpty(model.UserId))
            {
                //Means Update

                // Get role
                var roleName = RoleManager.FindById(model.SelectedRole).Name;
                AspNetUser userResult = UserManager.FindById(model.UserId);
                string userrRoleID = userResult.AspNetRoles.ToList()[0].Id;
                string userRoleName = RoleManager.FindById(userrRoleID).Name;

                // Check if role has been changed
                if (userrRoleID != model.SelectedRole)
                {
                    // Update User Role
                    UserManager.RemoveFromRole(model.UserId, userRoleName);
                    UserManager.AddToRole(model.UserId, roleName);
                    TempData["message"] = new MessageViewModel { Message = EPMS.Web.Resources.HR.Account.UpdateUser, IsUpdated = true };
                }
                // Password Reset
                if (!String.IsNullOrEmpty(model.Password))
                {
                    var token = await UserManager.GeneratePasswordResetTokenAsync(model.UserId);
                    var resetPwdResults = await UserManager.ResetPasswordAsync(model.UserId, token, model.Password);

                    if (resetPwdResults.Succeeded)
                    {
                        var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                        if (user != null)
                        {
                            await SignInAsync(user, isPersistent: false);
                        }
                        TempData["message"] = new MessageViewModel
                        {
                            Message = EPMS.Web.Resources.HR.Account.UpdatePass,
                            IsUpdated = true
                        };
                    }
                }
                // Get user by UserId to Update User
                AspNetUser userToUpdate = UserManager.FindById(model.UserId);
                if (userToUpdate.Email != model.Email || userToUpdate.EmployeeId != model.SelectedEmployee)
                {
                    if (userToUpdate.EmployeeId != model.SelectedEmployee)
                    {
                        var empId = AspNetUserService.GetAllUsers().Select(x => x.EmployeeId);
                        if (empId.Contains(model.SelectedEmployee))
                        {
                            model.Employees = employeeService.GetAll().Select(x => x.ServerToServer()).ToList();
                            model.Roles = HttpContext.GetOwinContext().Get<ApplicationRoleManager>().Roles.ToList();
                            TempData["message"] = new MessageViewModel { Message = EPMS.Web.Resources.HR.Account.EmpError, IsError = true };
                            return View(model);
                        }
                    }
                    AspNetUser currUser = new AspNetUser
                    {
                        Email = model.Email,
                        EmailConfirmed = true,
                        EmployeeId = model.SelectedEmployee,
                    };
                    if (userToUpdate != null)
                    {
                        userToUpdate.UpdateUserTo(currUser);
                    }
                    var updateUserResult = await UserManager.UpdateAsync(userToUpdate);
                    if (updateUserResult.Succeeded)
                    {
                        TempData["message"] = new MessageViewModel
                        {
                            Message = EPMS.Web.Resources.HR.Account.UpdateUser,
                            IsUpdated = true
                        };
                    }
                }

                return RedirectToAction("Users");
            }

            // Add new User
            if (ModelState.IsValid)
            {
                // TODO:Check # of Users that Admin can create

                // Check if Employee has already assigned
                var empId = AspNetUserService.GetAllUsers().Select(x => x.EmployeeId);
                if (empId.Contains(model.SelectedEmployee))
                {
                    model.Employees = employeeService.GetAll().Select(x => x.ServerToServer()).ToList();
                    model.Roles = HttpContext.GetOwinContext().Get<ApplicationRoleManager>().Roles.ToList();
                    TempData["message"] = new MessageViewModel { Message = EPMS.Web.Resources.HR.Account.EmpError, IsError = true };
                    return View(model);
                }
                var user = new AspNetUser { UserName = model.UserName, Email = model.Email };
                user.EmployeeId = model.SelectedEmployee;
                user.EmailConfirmed = true;
                if (!String.IsNullOrEmpty(model.Password))
                {
                    var result = await UserManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        //Setting role
                        var roleManager = HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
                        var roleName = roleManager.FindById(model.SelectedRole).Name;
                        UserManager.AddToRole(user.Id, roleName);
                        // Add User Preferences for Dashboards Widgets
                        var userId = user.Id;
                        string[] adminWidgets = { "MeetingWidget", "OrderWidget", "ComplaintsWidget", "RecruitmentWidget", "MyTasksWidget", "EmployeeRequestsWidget", "EmployeesWidget", "ProjectWidget", "PaymentWidget", "AlertsWidget" };
                        string[] employeeWidgets = { "EmployeeRequestsWidget", "MeetingWidget", "MyProfileWidget", "PayrollWidget", "MyTasksWidget" };
                        string[] customerWidgets = { "ComplaintsWidget", "OrderWidget", "ProjectWidget" };
                        switch (roleName)
                        {
                            case "Admin":
                                for (int i = 0; i < 10; i++)
                                {
                                    EPMS.Web.Models.DashboardWidgetPreference preferences = new EPMS.Web.Models.DashboardWidgetPreference
                                    {
                                        UserId = userId,
                                        WidgetId = adminWidgets[i],
                                        SortNumber = i + 1
                                    };
                                    var preferenceToAdd = preferences.CreateFromClientToServerWidgetPreferences();
                                    PreferencesService.AddPreferences(preferenceToAdd);
                                }
                                break;
                            case "Employee":
                                for (int i = 0; i < 5; i++)
                                {
                                    EPMS.Web.Models.DashboardWidgetPreference preferences = new EPMS.Web.Models.DashboardWidgetPreference
                                    {
                                        UserId = userId,
                                        WidgetId = employeeWidgets[i],
                                        SortNumber = i + 1
                                    };
                                    var preferenceToAdd = preferences.CreateFromClientToServerWidgetPreferences();
                                    PreferencesService.AddPreferences(preferenceToAdd);
                                }
                                break;
                            case "Customer":
                                for (int i = 0; i < 3; i++)
                                {
                                    EPMS.Web.Models.DashboardWidgetPreference preferences = new EPMS.Web.Models.DashboardWidgetPreference
                                    {
                                        UserId = userId,
                                        WidgetId = customerWidgets[i],
                                        SortNumber = i + 1
                                    };
                                    var preferenceToAdd = preferences.CreateFromClientToServerWidgetPreferences();
                                    PreferencesService.AddPreferences(preferenceToAdd);
                                }
                                break;
                        }
                        TempData["message"] = new MessageViewModel { Message = EPMS.Web.Resources.HR.Account.AddUser, IsSaved = true };
                        return RedirectToAction("Users");
                    }
                }
            }
            // If we got this far, something failed, redisplay form
            model.Employees = employeeService.GetAll().Select(x => x.ServerToServer()).ToList();
            model.Roles = HttpContext.GetOwinContext().Get<ApplicationRoleManager>().Roles.ToList();
            TempData["message"] = new MessageViewModel { Message = EPMS.Web.Resources.HR.Account.ChkFields, IsError = true };
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Signup()
        {
            SignupViewModel signupViewModel = new SignupViewModel();
            return View(signupViewModel);
        }
        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        //[EPMS.WebBase.Mvc.SiteAuthorize(PermissionKey = "UserAddEdit")]
        public async Task<ActionResult> Signup(SignupViewModel signupViewModel)
        {
            // Add new User
                // Check if User already exists
                var usernames = AspNetUserService.GetAllUsers().Select(x => x.UserName);
                if (usernames.Contains(signupViewModel.UserName))
                {
                    // it means username is already taken
                    TempData["message"] = new MessageViewModel { Message = EPMS.Web.Resources.HR.Account.EmpError, IsError = true };
                    return View(signupViewModel);
                }
                //call customer add service, get cusID, 
                
                #region Add Customer

                EPMS.Models.DomainModels.Customer customer = new EPMS.Models.DomainModels.Customer();
                customer.CustomerNameE = signupViewModel.CustomerNameE;
                customer.CustomerNameA = signupViewModel.CustomerNameA;
                customer.CustomerAddress = signupViewModel.Address;
                customer.CustomerMobile = signupViewModel.MobileNumber;
                EPMS.Models.DomainModels.Customer addedCustomer = customerService.AddCustomer(customer);
                
                    //custID=ser.add(customer);
                #endregion

                var user = new AspNetUser { UserName = signupViewModel.UserName, Email = signupViewModel.Email };
                user.CustomerId = addedCustomer.CustomerId;
                //user.EmailConfirmed = true;
                if (!String.IsNullOrEmpty(signupViewModel.Password))
                {
                    var result = await UserManager.CreateAsync(user, signupViewModel.Password);
                    if (result.Succeeded)
                    {
                        //Setting role
                        var roleManager = HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
                        UserManager.AddToRole(user.Id, "Customer");

                        var code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code },
                            protocol: Request.Url.Scheme);
                        await
                            UserManager.SendEmailAsync(signupViewModel.Email, "Confirm your account",
                                "Please confirm your account by clicking this link: <a href=\"" + callbackUrl +
                                "\">link</a><br>Your Password is:" + signupViewModel.Password);
                        ViewBag.Link = callbackUrl;

                        TempData["message"] = new MessageViewModel { Message = "User Created", IsSaved = true };
                        return RedirectToAction("Index","Dashboard");
                    }
                }
            return View(signupViewModel);
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
                    UserManager.SendEmailAsync(user.Email, "Reset Password",
                        "Please reset your password by clicking here: <a href=\"" + callbackUrl + "\">link</a>");
                ViewBag.Link = callbackUrl;
                TempData["message"] = new MessageViewModel { Message = ": An email with Password link has been sent.", IsUpdated = true };
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
            AspNetUser result = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(User.Identity.GetUserId());
            var ProfileViewModel = new ProfileViewModel
            {
                Email = result.Email,
                UserName = result.UserName,
                Address = result.Address,
                ImageName = (result.ImageName != null && result.ImageName != string.Empty) ? result.ImageName : string.Empty,
                ImagePath = ConfigurationManager.AppSettings["ProfileImage"].ToString() + result.ImageName
            };
            ViewBag.FilePath = ConfigurationManager.AppSettings["ProfileImage"] + ProfileViewModel.ImageName;//Server.MapPath
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return View(ProfileViewModel);
        }

        [HttpPost]
        [Authorize]
        [ValidateInput(false)]
        public ActionResult Profile(ProfileViewModel profileViewModel)
        {
            AspNetUser result = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(User.Identity.GetUserId());

            //Updating Data
            try
            {
                result.Email = profileViewModel.Email;
                result.Address = profileViewModel.Address;
                var updationResult = UserManager.Update(result);
                updateSessionValues(result);
                TempData["message"] = new MessageViewModel { Message = "Profile has been updated", IsUpdated = true };
            }
            catch (Exception e)
            {
            }
            return RedirectToAction("Profile");
        }

        public ActionResult UploadUserPhoto()
        {
            HttpPostedFileBase userPhoto = Request.Files[0];
            try
            {
                AspNetUser result = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(User.Identity.GetUserId());
                string savedFileName = "";
                //Save image to Folder
                if ((userPhoto != null))
                {
                    var filename = userPhoto.FileName;
                    var filePathOriginal = Server.MapPath(ConfigurationManager.AppSettings["ProfileImage"]);
                    savedFileName = Path.Combine(filePathOriginal, filename);
                    userPhoto.SaveAs(savedFileName);
                    result.ImageName = filename;
                    var updationResult = UserManager.Update(result);
                }
            }
            catch (Exception exp)
            {
                return Json(new { response = "Failed to upload. Error: " + exp.Message, status = (int)HttpStatusCode.BadRequest }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { filename = userPhoto.FileName, size = userPhoto.ContentLength / 1024 + "KB", response = "Successfully uploaded!", status = (int)HttpStatusCode.OK }, JsonRequestBehavior.AllowGet);
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
            AspNetUser result = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(User.Identity.GetUserId());
            string role = HttpContext.GetOwinContext().Get<ApplicationRoleManager>().FindById(result.AspNetRoles.ToList()[0].Id).Name;
            //Session["FullName"] = result.Employee.EmployeeFirstName + " " + result.Employee.EmployeeLastName;
            Session["UserID"] = result.Id;
            Session["RoleName"] = role;
        }
    }
}