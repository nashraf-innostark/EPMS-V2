using System.Collections.Generic;
using EPMS.Models.ResponseModels;

namespace EPMS.Web.ViewModels.RIF
{
    public class RIFViewModel
    {
        public RIFViewModel()
        {
            Rif = new Models.RIF();
        }
        public IEnumerable<DashboardModels.Customer> Customers { get; set; }
        public IEnumerable<DashboardModels.Order> Orders { get; set; }
        public Models.RIF Rif { get; set; }
        public List<Models.RIFItem> RifItem { get; set; }
        public IEnumerable<ItemVariationDropDownListItem> ItemVariationDropDownList { get; set; }
        public IEnumerable<DashboardModels.WarehousDDL> Warehouses { get; set; }
    }
}