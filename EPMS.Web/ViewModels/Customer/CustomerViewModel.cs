using System.Collections.Generic;

namespace EPMS.Web.ViewModels.Customer
{
    public class CustomerViewModel
    {
        public Models.Customer Customer { get; set; }

        public IEnumerable<Models.Customer> CustomerList { get; set; }
    }
}