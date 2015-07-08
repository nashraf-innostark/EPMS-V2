using System.Collections.Generic;
using EPMS.Web.Models;

namespace EPMS.Web.ViewModels.PhysicalCount
{
    public class PhysicalCountViewModel
    {
        public IList<WarehouseDdl> Warehouses { get; set; }
        public IList<PhysicalCountModel> PhysicalCountModels { get; set; }
    }
}