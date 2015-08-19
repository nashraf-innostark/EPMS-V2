using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;

namespace EPMS.Website.Controllers
{
    public class ServicesController : Controller
    {
        private readonly IWebsiteServicesService websiteServicesService;

        #region Private
        #endregion

        #region Constructor

        public ServicesController(IWebsiteServicesService websiteServicesService)
        {
            this.websiteServicesService = websiteServicesService;
        }

        #endregion

        #region Private
        #endregion
    }
}