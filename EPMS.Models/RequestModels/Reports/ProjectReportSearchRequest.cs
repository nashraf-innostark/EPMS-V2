using EPMS.Models.Common;

namespace EPMS.Models.RequestModels.Reports
{
    public class ProjectReportSearchRequest : GetPagedListRequest
    {
        public ProjectReportByColumn RequestByColumn
        {
            get
            {
                return (ProjectReportByColumn)iSortCol_0;
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
    public class WarehouseReportSearchRequest : GetPagedListRequest
    {
        public ProjectReportByColumn RequestByColumn
        {
            get
            {
                return (ProjectReportByColumn)iSortCol_0;
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
