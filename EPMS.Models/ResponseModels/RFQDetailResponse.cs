using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class RFQDetailResponse
    {
        public CompanyProfile Profile { get; set; }
        public RFQ Rfq { get; set; }
    }
}
