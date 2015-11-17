using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.RequestModels.Reports;
using EPMS.Web.Controllers;
using EPMS.WebModels.ModelMappers;
using EPMS.WebModels.ViewModels.Reports;

namespace EPMS.Web.Areas.Report.Controllers
{
    [Authorize]
    //[SiteAuthorize(PermissionKey = "Reports", IsModule = true)]
    public class WarehouseController : BaseController
    {
        private readonly IReportService reportService;
        private readonly IWarehouseService warehouseService;

        public WarehouseController(IReportService reportService, IWarehouseService warehouseService)
        {
            this.reportService = reportService;
            this.warehouseService = warehouseService;
        }

        //[SiteAuthorize(PermissionKey = "GenerateProjectsReport")]
        public ActionResult Create()
        {
            WarehouseReportCreateViewModel reportsCreateViewModel = new WarehouseReportCreateViewModel
            {
                Warehouses = warehouseService.GetAll().ToList().Select(x => x.CreateDDL()).ToList()
            };
            return View(reportsCreateViewModel);
        }
        //[SiteAuthorize(PermissionKey = "DetailsSingleProjectReport")]
        public ActionResult Details(WarehouseReportCreateViewModel createViewModel)
        {
            var request = new WarehouseReportCreateOrDetailsRequest
            {
                WarehouseId = createViewModel.WarehouseId,
                ReportId = createViewModel.ReportId,
                RequesterRole = Session["RoleName"].ToString(),
                RequesterId = Session["UserID"].ToString()
            };

            if (createViewModel.ReportId == 0)
                    request.IsCreate = true;

            var warehouses = reportService.SaveAndGetWarehouseReportDetails(request).Warehouses;

            if (warehouses == null)
            {
                return RedirectToAction("Create");
            }
            return View(warehouses);
        }
    }
}