using EPMS.Models.Common;

namespace EPMS.Models.RequestModels
{
    public class QuotationSearchRequest : GetPagedListRequest
    {
        public string RoleName { get; set; }
        public long OrderNo { get; set; }
        public long CustomerId { get; set; }
        public string EmployeeId { get; set; }
        public QuotationByColumn QuotationByColumn
        {
            get
            {
                return (QuotationByColumn)iSortCol_0;
            }
            set
            {
                iSortCol_0 = (short)value;
            }
        }
    }
}
