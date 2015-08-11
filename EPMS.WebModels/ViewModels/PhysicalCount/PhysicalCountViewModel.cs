using System.Collections.Generic;
using EPMS.Models.DashboardModels;

namespace EPMS.WebModels.ViewModels.PhysicalCount
{
    public class PhysicalCountViewModel
    {
        public PhysicalCountViewModel()
        {
            PhysicalCountItems = new List<WebsiteModels.PhysicalCountItemModel>();
            PhysicalCount = new WebsiteModels.PhysicalCountModel();
        }
        public IEnumerable<WarehousDDL> Warehouses { get; set; }
        public WebsiteModels.PhysicalCountModel PhysicalCount { get; set; }
        public IList<WebsiteModels.PhysicalCountItemModel> PhysicalCountItems { get; set; }
    }
}