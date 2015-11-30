using System.Collections.Generic;

namespace EPMS.WebModels.ViewModels.Reports
{
    public class RfqOrderCreateViewModel
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public long CustomerId { get; set; }
        public IList<Models.DashboardModels.Customer> Customers { get; set; }
    }
}