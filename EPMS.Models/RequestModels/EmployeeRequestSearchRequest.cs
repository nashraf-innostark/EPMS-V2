using EPMS.Models.Common;

namespace EPMS.Models.RequestModels
{
    public class EmployeeRequestSearchRequest : GetPagedListRequest
    {
        public string Requester { get; set; }
        public string SearchStr { get; set; }
        public string sSearch { get; set; }
        public string sEcho { get; set; }
        public int iDisplayLength { get; set; }

        public int iDisplayStart { get; set; }
        public EmployeeRequestByColumn EmployeeRequestByColumn
        {
            get
            {
                return (EmployeeRequestByColumn)SortBy;
            }
            set
            {
                SortBy = (short)value;
            }
        }
    }
}
