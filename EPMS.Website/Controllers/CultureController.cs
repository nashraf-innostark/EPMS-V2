using System.Globalization;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;

namespace EPMS.Website.Controllers
{
    public class CultureController : Controller
    {
        private readonly IWebsiteUserPreferenceService userPrefrencesService;

        public CultureController(IWebsiteUserPreferenceService userPrefrencesService)
        {
            this.userPrefrencesService = userPrefrencesService;
        }

        public ActionResult Set(string id)
        {
            if (id == "AR")
            {
                CultureInfo culture = new CultureInfo("ar");

                System.Threading.Thread.CurrentThread.CurrentUICulture = culture;
                System.Threading.Thread.CurrentThread.CurrentCulture = culture;

                if (User.Identity.IsAuthenticated)
                {
                    userPrefrencesService.AddUpdateCulture(Session["UserID"].ToString(), "ar");
                }
                Session["Culture"] = "ar";
            }
            else
            {
                CultureInfo culture = new CultureInfo("en");

                System.Threading.Thread.CurrentThread.CurrentUICulture = culture;
                System.Threading.Thread.CurrentThread.CurrentCulture = culture;

                if (User.Identity.IsAuthenticated)
                {
                    userPrefrencesService.AddUpdateCulture(Session["UserID"].ToString(), "en");
                }
                Session["Culture"] = "en";
            }
            string redirectUrl = Request.UrlReferrer.ToString();
            if (string.IsNullOrEmpty(redirectUrl))
            {
                return RedirectToAction("Index", "Home");
            }
            return Redirect(redirectUrl);
        }
    }
}