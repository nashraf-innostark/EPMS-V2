using System;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.RequestModels.Reports;
using EPMS.Web.Controllers;
using EPMS.WebBase.Mvc;
using EPMS.WebModels.ModelMappers.Reports;
using EPMS.WebModels.ViewModels.Reports;

namespace EPMS.Web.Areas.Report.Controllers
{
    public class InventoryController : BaseController
    {
        private readonly IReportService reportService;

        public InventoryController(IReportService reportService)
        {
            this.reportService = reportService;
        }

        //[SiteAuthorize(PermissionKey = "InventoryReports")]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult WarehouseIndex(WarehouseReportSearchRequest searchRequest)
        {
            searchRequest.SearchString = Request["search"];
            var response = reportService.GetWarehousesReports(searchRequest);
            var data =
                response.Reports.Select(x => x.CreateProjectReportFromServerToClient()).ToList();
            WarehouseReportsListViewModel projectsListViewModel = new WarehouseReportsListViewModel
            {
                aaData = data,
                iTotalRecords = Convert.ToInt32(response.TotalCount),
                iTotalDisplayRecords = Convert.ToInt32(response.FilteredCount),
                sEcho = searchRequest.sEcho
            };
            return Json(projectsListViewModel, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult InventoryItemsIndex(WarehouseReportSearchRequest searchRequest)
        {
            searchRequest.SearchString = Request["search"];
            var response = reportService.GetInventoryItemsReports(searchRequest);
            var data =
                response.Reports.Select(x => x.CreateProjectReportFromServerToClient()).ToList();
            WarehouseReportsListViewModel projectsListViewModel = new WarehouseReportsListViewModel
            {
                aaData = data,
                iTotalRecords = Convert.ToInt32(response.TotalCount),
                iTotalDisplayRecords = Convert.ToInt32(response.FilteredCount),
                sEcho = searchRequest.sEcho
            };
            return Json(projectsListViewModel, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult VendorIndex(VendorReportSearchRequest searchRequest)
        {
            searchRequest.SearchString = Request["search"];
            var response = reportService.GetVendorsReports(searchRequest);
            var data =
                response.Reports.Select(x => x.CreateProjectReportFromServerToClient()).ToList();
            VendorReportsListViewModel projectsListViewModel = new VendorReportsListViewModel
            {
                aaData = data,
                iTotalRecords = Convert.ToInt32(response.TotalCount),
                iTotalDisplayRecords = Convert.ToInt32(response.FilteredCount),
                sEcho = searchRequest.sEcho
            };
            return Json(projectsListViewModel, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult TaskIndex(TaskReportSearchRequest searchRequest)
        {
            searchRequest.SearchString = Request["search"];
            var tasksResponse = reportService.GetTasksReports(searchRequest);
            var tasksList =
                tasksResponse.Tasks.Select(x => x.CreateTaskReportFromServerToClient()).ToList();
            TasksReportListViewModel tasksListViewModel = new TasksReportListViewModel
            {
                aaData = tasksList,
                iTotalRecords = Convert.ToInt32(tasksResponse.TotalCount),
                iTotalDisplayRecords = Convert.ToInt32(tasksResponse.FilteredCount),
                sEcho = searchRequest.sEcho
            };
            return Json(tasksListViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}