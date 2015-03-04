using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EPMS.Web.Areas.Help.Controllers
{
    [Authorize]
    public class HelpController : Controller
    {
        // GET: Help/Help
        public ActionResult Index()
        {
            return View();
        }
    }
}