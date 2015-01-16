using EPMS.Models.Common;

namespace EPMS.Models.RequestModels
{
    public class QuotationSearchRequest : GetPagedListRequest
    {
        public string ClientName { get; set; }
        public long OrderNo { get; set; }
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
