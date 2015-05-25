using System.Collections.Generic;

namespace EPMS.Web.ViewModels.RFI
{
    public class RFIViewModel
    {
        public IEnumerable<Models.Customer> Customers { get; set; }
        public IEnumerable<Models.Order> Orders { get; set; }
    }
}