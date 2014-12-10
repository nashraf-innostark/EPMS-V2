using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPMS.Models.Common;

namespace EPMS.Models.RequestModels
{
    public class JobTitleSearchRequest : GetPagedListRequest
    {
        public long JobTitleId { get; set; }
        public string JobTitleName { get; set; }
        public string JobTitleDesc { get; set; }
        public long DepartmentId { get; set; }
        public Nullable<double> BasicSalary { get; set; }
        public string RecCreatedBy { get; set; }
        public Nullable<System.DateTime> RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public Nullable<System.DateTime> RecLastUpdatedDt { get; set; }

        public JobTitleByColumn JobTitleByColumn
        {
            get
            {
                return (JobTitleByColumn)SortBy;
            }
            set
            {
                SortBy = (short)value;
            }
        }
    }
}
