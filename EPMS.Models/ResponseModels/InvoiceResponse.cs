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
        public long FirstReceiptId { get; set; }
        public long SecondReceiptId { get; set; }
        public long ThirdReceiptId { get; set; }
        public long FourthReceiptId { get; set; }
        public string EmployeeNameE { get; set; }
        public string EmployeeNameA { get; set; }


    }
}
