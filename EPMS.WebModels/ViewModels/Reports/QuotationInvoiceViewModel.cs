using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EPMS.WebModels.ViewModels.Reports
{
    public class QuotationInvoiceViewModel
    {
        public QuotationInvoiceViewModel()
        {
            Employees = new List<WebsiteModels.Employee>();
            Quotations = new List<WebsiteModels.Quotation>();
        }

        [Required(ErrorMessageResourceType = typeof(Resources.Reports.CustomerReport), ErrorMessageResourceName = "StartDateValidation")]
        public string StartDate { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Reports.CustomerReport), ErrorMessageResourceName = "EndDateValidation")]
        public string EndDate { get; set; }

        public IEnumerable<WebsiteModels.Employee> Employees { get; set; }
        public IEnumerable<WebsiteModels.Quotation> Quotations { get; set; }
        public long EmployeeId { get; set; }
        public int QuotationsCount { get; set; }
        public int InvoicesCount { get; set; }
    }
}
