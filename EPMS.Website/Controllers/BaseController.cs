using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using EPMS.Implementation.Identity;
using EPMS.Interfaces.IServices;
using EPMS.Models.DomainModels;
using EPMS.WebBase.UnityConfiguration;
using EPMS.WebModels.ModelMappers.Website.ShoppingCart;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Practices.Unity;

namespace EPMS.Website.Controllers
{
    public class BaseController : Controller
    {
        #region Private

        private ApplicationUserManager _userManager;
        private IWebsiteUserPreferenceService userPrefrencesService;
        private IShoppingCartService cartService;
        private IWebsiteHomePageService homePageService;

        private void SetCultureInfo()
        {
            CultureInfo info;
            if (Session["Culture"] != null)
            {
                info = new CultureInfo(Session["Culture"].ToString());
            }
            else
            {
                userPrefrencesService = UnityWebActivator.Container.Resolve<IWebsiteUserPreferenceService>();
                var userPrefrences = userPrefrencesService.LoadPrefrencesByUserId(User.Identity.GetUserId());
                info = userPrefrences != null
                ? new CultureInfo(userPrefrences.Culture)
                : new CultureInfo("en");
                Session["Culture"] = info.Name;
            }
            info.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            System.Threading.Thread.CurrentThread.CurrentCulture = info;
            System.Threading.Thread.CurrentThread.CurrentUICulture = info;
        }

        #endregion

        #region Protected
        // GET: Base
        protected override async void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            SetUserDetail();
            // Set Website Logo
            SetWebsiteLogo();
            // Set Cart Items
            SetCartItems();
            // Set culture info
            SetCultureInfo();
        }
        #endregion

        #region Public

        //when isForce =  true it sets the value, no matter session has or not
        public void SetUserDetail()
        {
            Session["FullName"] = Session["UserID"] = string.Empty;

            if (!User.Identity.IsAuthenticated) return;

            AspNetUser result =
                HttpContext.GetOwinContext()
                    .GetUserManager<ApplicationUserManager>()
                    .FindById(User.Identity.GetUserId());
            //string role =
            //    HttpContext.GetOwinContext()
            //        .Get<ApplicationRoleManager>()
            //        .FindById(result.AspNetRoles.ToList()[0].Id)
            //        .Name;
            Session["UserName"] = result.UserName;
            Session["UserID"] = result.Id;
            //Session["RoleName"] = role;
            //Session["RoleId"] = result.AspNetRoles.ToList()[0].Id;
        }

        public void SetCartItems()
        {
            if (User.Identity.IsAuthenticated)
            {
                Session["ShoppingCartId"] = Session["UserID"].ToString();
                cartService = UnityWebActivator.Container.Resolve<IShoppingCartService>();
                var response = cartService.FindByUserCartId(Session["UserID"].ToString());
                if (response != null)
                {
                    WebModels.WebsiteModels.ShoppingCart userCart = response.CreateFromServerToClient();
                    Session["ShoppingCartItems"] = userCart;
                }
                else
                {
                    Session["ShoppingCartItems"] = null;
                }
            }
        }

        public void SetWebsiteLogo()
        {
            homePageService = UnityWebActivator.Container.Resolve<IWebsiteHomePageService>();
            var logo = homePageService.GetHomePageLogo().WebsiteLogoPath;
            Session["WebsiteLogo"] = ConfigurationManager.AppSettings["WebsiteLogo"] + logo;
        }
        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }

        #endregion

    }
}