using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EPMS.Models.DashboardModels;

namespace EPMS.WebModels.ViewModels.Reports
{
    public class CreateViewModel
    {
        public CreateViewModel()
        {
            Projects = new List<Models.DashboardModels.Project>();
        }

        public long ProjectId { get; set; }
        public long ReportId { get; set; }
        public IList<Models.DashboardModels.Project> Projects { get; set; }
    }

    public class WarehouseReportCreateViewModel
    {
        public WarehouseReportCreateViewModel()
        {
            Warehouses = new List<WarehousDDL>();
        }

        public long WarehouseId { get; set; }
        public long ReportId { get; set; }
        public List<WarehousDDL> Warehouses { get; set; }
    }
    public class ItemReportCreateViewModel
    {
        public ItemReportCreateViewModel()
        {
            InventoryItems = new List<InventoryItemDDL>();
        }

        public long ItemId { get; set; }
        public long ReportId { get; set; }
        public List<InventoryItemDDL> InventoryItems { get; set; }
    }
    public class QuotationOrderReportCreateViewModel
    {
        public QuotationOrderReportCreateViewModel()
        {
            Customers = new List<Models.DashboardModels.Customer>();
        }
        [Required(ErrorMessageResourceType = typeof(Resources.Reports.CustomerReport), ErrorMessageResourceName = "StartDateValidation")]
        public string StartDate { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Reports.CustomerReport), ErrorMessageResourceName = "EndDateValidation")]
        public string EndDate { get; set; }
        public long CustomerId { get; set; }
        public long ReportId { get; set; }
        public List<Models.DashboardModels.Customer> Customers { get; set; }
    }
}
