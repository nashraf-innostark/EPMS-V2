using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;

namespace EPMS.Website.Controllers
{
    public class WebsiteFooterController : Controller
    {
        private readonly IWebsiteFooterService footerService;

        public WebsiteFooterController(IWebsiteFooterService footerService)
        {
            this.footerService = footerService;
        }

        public ActionResult Index()
        {
            var footer = footerService.LoadFooterMenu();
            return View(footer);
        }
    }
}