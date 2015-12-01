using System.Collections.Generic;
using EPMS.Models.Common;
using EPMS.Models.DomainModels;

namespace EPMS.WebModels.ViewModels.Customer
{
    public class CustomerViewModel
    {
        public WebsiteModels.Customer Customer { get; set; }
        public AspNetUser User { get; set; }
        public IEnumerable<WebsiteModels.Customer> CustomerList { get; set; }
        public IEnumerable<EmployeeForDropDownList> Employees { get; set; }
        public string Email { get; set; }
    }
}