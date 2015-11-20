using System.Web.Mvc;
using EPMS.WebModels.ViewModels.Reports;

namespace EPMS.Web.Areas.Report.Controllers
{
    public class ItemController : Controller
    {
        // GET: Report/Item
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(ItemReportCreateViewModel createViewModel)
        {
            return View();
        }

        public ActionResult Details(long? ReportId)
        {
            return View();
        }
    }
}