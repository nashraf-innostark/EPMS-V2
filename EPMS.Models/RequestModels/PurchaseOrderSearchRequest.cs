using EPMS.Models.Common;

namespace EPMS.Models.RequestModels
{
    public class PurchaseOrderSearchRequest : GetPagedListRequest
    {
        public bool IsManager { get; set; }
        public string Direction { get; set; }
        public PurchaseOrderByColumn PurchaseOrderByColumn
        {
            get
            {
                return (PurchaseOrderByColumn)iSortCol_0;
            }
            set
            {
                iSortCol_0 = (short)value;
            }
        }
    }
}
