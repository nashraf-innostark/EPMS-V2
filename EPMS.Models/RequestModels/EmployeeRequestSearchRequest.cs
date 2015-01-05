using EPMS.Models.Common;

namespace EPMS.Models.RequestModels
{
    public class EmployeeRequestSearchRequest : GetPagedListRequest
    {
        public string SearchStr { get; set; }
        public string Requester { get; set; }
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
