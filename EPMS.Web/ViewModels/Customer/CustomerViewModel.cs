using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Web.ViewModels.Customer
{
    public class CustomerViewModel
    {
        public Models.Customer Customer { get; set; }
        public AspNetUser User { get; set; }
        public IEnumerable<Models.Customer> CustomerList { get; set; }
        public string Email { get; set; }
    }
}