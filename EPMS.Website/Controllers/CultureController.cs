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
                CultureInfo culture = new CultureInfo("ar-AE");
                CultureInfo.DefaultThreadCurrentCulture = culture;
                CultureInfo.DefaultThreadCurrentUICulture = culture;
                System.Threading.Thread.CurrentThread.CurrentUICulture = culture;
                System.Threading.Thread.CurrentThread.CurrentCulture = culture;

                if (userPrefrencesService.AddUpdateCulture(Session["UserID"].ToString(), "ar-AE"))
                    Session["Culture"] = "ar-AE";
            }
            else
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

                if (userPrefrencesService.AddUpdateCulture(Session["UserID"].ToString(), "en-US"))
                    Session["Culture"] = "en-US";
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