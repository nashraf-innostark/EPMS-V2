using EPMS.Models.Common;

namespace EPMS.Models.RequestModels
{
    public class PhysicalCountSearchRequest : GetPagedListRequest
    {
        public PhysicalCountByColumn PhysicalCountByColumn
        {
            get
            {
                return (PhysicalCountByColumn)iSortCol_0;
            }
            set
            {
                iSortCol_0 = (short)value;
            }
        }
    }
}
