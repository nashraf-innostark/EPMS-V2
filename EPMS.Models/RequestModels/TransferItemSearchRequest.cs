using EPMS.Models.Common;

namespace EPMS.Models.RequestModels
{
    public class TransferItemSearchRequest : GetPagedListRequest
    {
        public bool CompleteAccess { get; set; }
        public string Direction { get; set; }
        public string Requester { get; set; }
        public TirRequestByColumn RequestByColumn
        {
            get
            {
                return (TirRequestByColumn)iSortCol_0;
            }
            set
            {
                iSortCol_0 = (short)value;
            }
        }
    }
}
