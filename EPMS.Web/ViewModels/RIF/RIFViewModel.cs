using System.Collections.Generic;
using EPMS.Models.ResponseModels;
using EPMS.Web.Models;

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
        public List<RIFItem> RifItem { get; set; }
        public IEnumerable<ItemVariationDropDownListItem> ItemVariationDropDownList { get; set; }
        public IList<ItemWarehouse> ItemWarehouses { get; set; }
        public IList<ItemReleaseForRif> ItemReleases { get; set; }
    }
}