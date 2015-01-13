using System.Collections.Generic;
using EPMS.Models.RequestModels;

namespace EPMS.Web.ViewModels.Quotation
{
    public class QuotationCreateViewModel
    {
        public QuotationCreateViewModel()
        {
            SearchRequest = new QuotationSearchRequest();
        }

        public IEnumerable<Models.Customer> Customers { get; set; }
        public string Customer { get; set; }
        public string EmployeeName { get; set; }
        public QuotationSearchRequest SearchRequest { get; set; }
    }
}