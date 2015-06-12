using EPMS.Models.Common;

namespace EPMS.Models.RequestModels
{
    public class TirSearchRequest : GetPagedListRequest
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
        private int _SortCol;
        public int iSortCol_0
        {
            get
            {
                return _SortCol;
            }
            set
            {
                _SortCol = value == 0 ? 0 : value;
            }
        }
    }
}
