using System.Collections.Generic;
using EPMS.Web.DashboardModels;
using EPMS.Web.Models;

namespace EPMS.Web.ViewModels.PhysicalCount
{
    public class PhysicalCountViewModel
    {
        public PhysicalCountViewModel()
        {
            PhysicalCountItems=new List<PhysicalCountItemModel>();
            PhysicalCount=new PhysicalCountModel();
        }
        public IEnumerable<WarehousDDL> Warehouses { get; set; }
        public PhysicalCountModel PhysicalCount { get; set; }
        public IList<PhysicalCountItemModel> PhysicalCountItems { get; set; }
    }
}