using System.Collections.Generic;

namespace EPMS.Web.ViewModels.RFI
{
    public class RFIViewModel
    {
        public RFIViewModel()
        {
            Rfi = new Models.RFI();
        }
        public IEnumerable<DashboardModels.Customer> Customers { get; set; }
        public IEnumerable<DashboardModels.Order> Orders { get; set; }
        public Models.RFI Rfi { get; set; }
        public IEnumerable<Models.RFIItem> RfiItem { get; set; }
    }
}