using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPMS.Web.Controllers;

namespace EPMS.Web.Areas.Inventory.Controllers
{
    public class InvontryWarehouseController : BaseController
    {
        // GET: Inventory/InvontryWarehouse
        public ActionResult Index()
        {
            return View();
        }
    }
}