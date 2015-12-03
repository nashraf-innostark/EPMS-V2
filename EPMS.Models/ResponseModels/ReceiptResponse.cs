using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class ReceiptResponse
    {
        public Receipt Receipt { get; set; }
        public Invoice Invoice { get; set; }
        public Quotation Quotation { get; set; }
        public CompanyProfile CompanyProfile { get; set; }
        public Customer Customer { get; set; }
        public string EmployeeNameE { get; set; }
        public string EmployeeNameA { get; set; }
    }
}
