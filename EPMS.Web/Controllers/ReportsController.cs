using System.Web.Mvc;
using EPMS.WebBase.Mvc;

namespace EPMS.Web.Controllers
{
    public class ReportsController : BaseController
    {
        //
        // GET: /Reports/
        [SiteAuthorize(PermissionKey = "PrisonerReport")]
        public ActionResult PrisonerReport()
        {
            return View();
        }
	}
}