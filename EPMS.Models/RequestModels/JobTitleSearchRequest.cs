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
        public long JobId { get; set; }
        public string JobTitleNameE { get; set; }
        public string JobTitleNameA { get; set; }
        public string JobDescriptionE { get; set; }
        public string JobDescriptionA { get; set; }
        public decimal? BasicSalary { get; set; }
        public long DepartmentId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

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
