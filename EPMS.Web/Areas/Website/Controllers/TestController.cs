using System.Web.Mvc;
using EPMS.Web.Controllers;

namespace EPMS.Web.Areas.Website.Controllers
{
    public class TestController : BaseController
    {
        // GET: Website/Test
        public ActionResult JsTree()
        {
            return View();
        }
        // GET: Website/Test
        public ActionResult Tree()
        {
            return View();
        }
    }
}