using EPMS.Models.DomainModels;

namespace EPMS.Models.RequestModels
{
    public class QuotationStatusRequest
    {
        public QuotationStatusRequest()
        {
            Order = new Order();
        }
        public long QuotationId { get; set; }
        public short Status { get; set; }
        public Order Order { get; set; }
    }
}
