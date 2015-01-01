using System;
using EPMS.Models.Common;

namespace EPMS.Models.RequestModels
{
    public class EmployeeSearchRequset : GetPagedListRequest
    {
        public Guid UserId { get; set; }
        public string SearchStr { get; set; }
        public long EmployeeId { get; set; }
        public string EmployeeNameE { get; set; }
        public string EmployeeNameA { get; set; }
        public long JobTitleId { get; set; }
        
        public EmployeeByColumn EmployeeByColumn
        {
            get
            {
                return (EmployeeByColumn)SortBy;
            }
            set
            {
                SortBy = (short)value;
            }
        }
    }
}
