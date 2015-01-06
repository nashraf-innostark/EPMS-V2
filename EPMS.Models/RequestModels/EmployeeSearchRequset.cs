using System;
using EPMS.Models.Common;

namespace EPMS.Models.RequestModels
{
    public class EmployeeSearchRequset : GetPagedListRequest
    {
        public Guid UserId { get; set; }
        public long EmployeeId { get; set; }
        public string EmployeeNameE { get; set; }
        public string EmployeeNameA { get; set; }
        public long JobTitleId { get; set; }

        //public int iDisplayStart { get; set; }

        public EmployeeByColumn EmployeeByColumn
        {
            get
            {
                return (EmployeeByColumn)iSortCol_0;
            }
            set
            {
                iSortCol_0 = (short)value;
            }
        }
    }
}
