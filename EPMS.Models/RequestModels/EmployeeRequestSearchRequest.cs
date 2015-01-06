using EPMS.Models.Common;

namespace EPMS.Models.RequestModels
{
    public class EmployeeRequestSearchRequest : GetPagedListRequest
    {
        public string Requester { get; set; }
        public EmployeeRequestByColumn EmployeeRequestByColumn
        {
            get
            {
                return (EmployeeRequestByColumn)iSortCol_0;
            }
            set
            {
                iSortCol_0 = (short)value;
            }
        }
    }
}
