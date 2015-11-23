using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.RequestModels.Reports;
using EPMS.WebModels.ModelMappers;
using EPMS.WebModels.ModelMappers.PMS;
using EPMS.WebModels.ViewModels.Reports;
using EPMS.WebModels.WebsiteModels;
using Rotativa;

namespace EPMS.Web.Areas.Report.Controllers
{
    public class ItemController : Controller
    {
        private readonly IInventoryItemService inventoryItemService;
        private readonly IReportService reportService;

        public ItemController(IInventoryItemService inventoryItemService, IReportService reportService)
        {
            this.inventoryItemService = inventoryItemService;
            this.reportService = reportService;
        }

        // GET: Report/Item
        public ActionResult Create()
        {
            ItemReportCreateViewModel createViewModel=new ItemReportCreateViewModel();
            var items = inventoryItemService.GetAll();
            createViewModel.InventoryItems = items.Select(x => x.CreateFromServerToClientDDL()).ToList();
            return View(createViewModel);
        }
        [HttpPost]
        public ActionResult Create(ItemReportCreateViewModel createViewModel)
        {
            var request = new InventoryItemReportCreateOrDetailsRequest
            {
                ItemId = createViewModel.ItemId,
                RequesterRole = Session["RoleName"].ToString(),
                RequesterId = Session["UserID"].ToString()
            };
            var reportId = reportService.SaveInventoryItemsReport(request);
            return RedirectToAction("Details", new { ReportId = reportId });
        }

        public ActionResult Details(long? ReportId)
        {
            if (ReportId==null||ReportId <= 0) return View("Create");

            ViewBag.ReportId = ReportId;
            var details = reportService.GetInventoryItemsReport((long)ReportId);
            return View(details);
        }
        [AllowAnonymous]
        public ActionResult GeneratePdfAll(long? ReportId)
        {
            //Dictionary<string, string> cookies = (Dictionary<string, string>)Session["Cookies"];
            return new ActionAsPdf("ReportAsPdf", new { ReportId = ReportId }) { FileName = "InventoryReport_Report.pdf" };
        }
        [AllowAnonymous]
        public ActionResult ReportAsPdf(long? ReportId)
        {
            var reportData = reportService.GetInventoryItemsReport((long)ReportId);
            return View(reportData);
        }
    }
}