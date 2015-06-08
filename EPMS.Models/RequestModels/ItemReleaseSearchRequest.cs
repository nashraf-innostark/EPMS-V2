using EPMS.Models.Common;

namespace EPMS.Models.RequestModels
{
    public class ItemReleaseSearchRequest : GetPagedListRequest
    {
        public string Requester { get; set; }
        public ItemReleaseByColumn EmployeeByColumn
        {
            get
            {
                return (ItemReleaseByColumn)iSortCol_0;
            }
            set
            {
                iSortCol_0 = (short)value;
            }
        }
    }
}
