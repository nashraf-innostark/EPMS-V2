using System.Web.Mvc;

namespace EPMS.Web.Areas.Website
{
    public class WebsiteAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Website";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Website_default",
                "Website/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "Website_home",
                "Website/Home/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}