using System.Collections.Generic;

namespace EPMS.WebModels.ViewModels.Invoice
{
    public class InvoiceListViewModel
    {
        public InvoiceListViewModel()
        {
            Invoices = new List<WebsiteModels.Invoice>();
        }

        public IEnumerable<WebsiteModels.Invoice> Invoices { get; set; }
    }
}
