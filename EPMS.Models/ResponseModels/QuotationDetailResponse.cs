using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class QuotationDetailResponse
    {
        public CompanyProfile Profile { get; set; }
        public Quotation Quotation { get; set; }
    }
}
