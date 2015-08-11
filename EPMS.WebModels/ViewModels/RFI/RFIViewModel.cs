using System.Collections.Generic;
using EPMS.Models.DashboardModels;
using EPMS.Models.ResponseModels;

namespace EPMS.WebModels.ViewModels.RFI
{
    public class RFIViewModel
    {
        public RFIViewModel()
        {
            Rfi = new WebsiteModels.RFI();
        }
        public IEnumerable<Models.DashboardModels.Customer> Customers { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public WebsiteModels.RFI Rfi { get; set; }
        public List<WebsiteModels.RFIItem> RfiItem { get; set; }
        public IEnumerable<ItemVariationDropDownListItem> ItemVariationDropDownList { get; set; }
    }
}