    using System.Globalization;
    using System.Web.Mvc;
    using EPMS.Interfaces.IServices;

namespace EPMS.Web.Controllers
{
    public class CultureController : Controller
    {
        private readonly IUserPrefrencesService userPrefrencesService;

        public CultureController(IUserPrefrencesService userPrefrencesService)
        {
            this.userPrefrencesService = userPrefrencesService;
        }

        // GET: Culture
        public ActionResult Set(string id, bool login=false)
        {
            if (Session["UserID"] == null && !login)
            {
                return RedirectToAction("LogOff", "Account");
            }

            if (id == "AR")
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("ar");
                System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("ar");

                if (Session["UserID"] != null)
                    userPrefrencesService.AddUpdateCulture(Session["UserID"].ToString(), "ar");

                Session["Culture"] = "ar";
            }
            else
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
                System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("en");

                if (Session["UserID"] != null)
                    userPrefrencesService.AddUpdateCulture(Session["UserID"].ToString(), "en");
                Session["Culture"] = "en";
            }
            string redirectUrl = Request.UrlReferrer.ToString();
            if (string.IsNullOrEmpty(redirectUrl))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            return Redirect(redirectUrl);
        }
    }
}