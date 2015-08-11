using System.Collections.Generic;
using EPMS.Models.DashboardModels;
using EPMS.Models.ResponseModels;

namespace EPMS.WebModels.ViewModels.RIF
{
    public class RIFViewModel
    {
        public RIFViewModel()
        {
            Rif = new WebsiteModels.RIF();
        }
        public IEnumerable<Models.DashboardModels.Customer> Customers { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public WebsiteModels.RIF Rif { get; set; }
        public List<WebsiteModels.RIFItem> RifItem { get; set; }
        public IEnumerable<ItemVariationDropDownListItem> ItemVariationDropDownList { get; set; }
    }
}