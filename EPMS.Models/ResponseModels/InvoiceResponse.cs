using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class InvoiceResponse
    {
        public Quotation Quotation { get; set; }
        public Invoice Invoice { get; set; }
        public CompanyProfile CompanyProfile { get; set; }
        public Customer Customer { get; set; }

    }
}
