using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPMS.Web.Controllers;
using EPMS.WebBase.Mvc;

namespace EPMS.Web.Areas.Website.Controllers
{
    public class WebsiteServicesController : BaseController
    {
        // GET: Website/Service
        [SiteAuthorize(PermissionKey = "ServiceIndex")]
        public ActionResult Index()
        {
            return View();
        }
        [SiteAuthorize(PermissionKey = "ServiceCreate,ServiceView")]
        public ActionResult Create()
        {
            return View();
        }
    }
}