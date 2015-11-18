using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EPMS.WebModels.ViewModels.Reports
{
    public class CustomerCreateViewModel
    {
        public CustomerCreateViewModel()
        {
            Customers = new List<WebsiteModels.Customer>();
        }
        [Required(ErrorMessageResourceType = typeof(Resources.Reports.CustomerReport), ErrorMessageResourceName = "StartDateValidation")]
        public string StartDate { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Reports.CustomerReport), ErrorMessageResourceName = "EndDateValidation")]
        public string EndDate { get; set; }
        public long ReportId { get; set; }
        public IList<WebsiteModels.Customer> Customers { get; set; }
    }
}
