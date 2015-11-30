using System.Collections.Generic;
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
}
