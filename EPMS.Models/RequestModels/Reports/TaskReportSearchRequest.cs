using EPMS.Models.Common;

namespace EPMS.Models.RequestModels.Reports
{
    public class TaskReportSearchRequest :GetPagedListRequest
    {
        public TaskReportByColumn RequestByColumn
        {
            get
            {
                return (TaskReportByColumn)iSortCol_0;
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
