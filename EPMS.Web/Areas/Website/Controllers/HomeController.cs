﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPMS.Web.Controllers;

namespace EPMS.Web.Areas.Website.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Website/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}