using System.Collections.Generic;
using EPMS.Models.RequestModels;

namespace EPMS.Web.ViewModels.Quotation
{
    public class QuotationListViewModel
    {
        public QuotationListViewModel()
        {
            SearchRequest = new QuotationSearchRequest();
        }
        public QuotationSearchRequest SearchRequest { get; set; }
        public IEnumerable<Models.Quotation> Quotations { get; set; }
    }
}