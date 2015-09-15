using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EPMS.Implementation.Identity;
using EPMS.Interfaces.IServices;
using EPMS.Models.DomainModels;
using EPMS.Models.IdentityModels.ViewModels;
using EPMS.WebModels.ModelMappers.WebsiteClient;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using WebsiteCustomer = EPMS.WebModels.WebsiteModels.WebsiteCustomer;

namespace EPMS.Website.Controllers
{
    public class CustomerIdentityController : Controller
    {
        #region Private
        private readonly IAspNetUserService aspNetUserService;
        private readonly IWebsiteCustomerService websiteCustomerService;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        private ApplicationSignInManager _signInManager;
        #endregion

        #region Constructor

        public CustomerIdentityController(IAspNetUserService aspNetUserService, IWebsiteCustomerService websiteCustomerService)
        {
            this.aspNetUserService = aspNetUserService;
            this.websiteCustomerService = websiteCustomerService;
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
        public ApplicationSignInManager SignInManager
        {
            get { return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>(); }
            private set { _signInManager = value; }
        }

        // GET: CustomerIdentity
        public ActionResult Index()
        {
            return View();
        }

        // GET: SignUp
        public ActionResult SignUp()
        {
            var viewModel = new WebCustomerIdentityViewModel
            {
                SignUp = new CustomerSignUpViewModel()
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> SignUp(WebCustomerIdentityViewModel viewModel)
        {
            var users = aspNetUserService.GetAllUsers().ToList();
            if (users.Any())
            {
                var usernames = users.Select(x => x.UserName);
                if (usernames.Contains(viewModel.SignUp.UserName))
                {
                    // it means username is already taken

                    //return false;
                }
                var emails = users.Select(x => x.Email);
                if (emails.Contains(viewModel.SignUp.Email))
                {
                    // it means email is already taken

                    //return false;
                }
            }

            #region Add Customer

            WebsiteCustomer customer = new WebsiteCustomer
            {
                CustomerNameEn = viewModel.SignUp.CustomerNameEn,
                CustomerNameAr = viewModel.SignUp.CustomerNameEn,
            };
            var customerToAdd = customer.CreateFromClientToServer();
            bool status = websiteCustomerService.AddWebsiteCustomer(customerToAdd);

            #endregion

            if (status)
            {
                var user = new AspNetUser { UserName = viewModel.SignUp.UserName, Email = viewModel.SignUp.Email };
                user.CustomerId = customerToAdd.CustomerId;
                if (!String.IsNullOrEmpty(viewModel.SignUp.Password))
                {
                    var result = await UserManager.CreateAsync(user, viewModel.SignUp.Password);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, true, false);
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: SignUp
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");
            var viewModel = new WebCustomerIdentityViewModel
            {
                Login = new CustomerLoginViewModel()
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Login(WebCustomerIdentityViewModel model)
        {
            var user = await UserManager.FindByNameAsync(model.Login.UserName);
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
            var result = await SignInManager.PasswordSignInAsync(model.Login.UserName, model.Login.Password, model.Login.RememberMe, false);

            switch (result)
            {
                case SignInStatus.Success:
                    {
                        return RedirectToAction("Index", "Home");
                    }
            }
            return RedirectToAction("Index", "Home");
        }

        #endregion

    }
}