using System.Web.Mvc;

namespace EPMS.Web.Areas.Meeting
{
    public class MeetingAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Meeting";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Meeting_default",
                "Meeting/{controller}/{action}/{id}",
                new { id = UrlParameter.Optional }
            );
        }
    }
}