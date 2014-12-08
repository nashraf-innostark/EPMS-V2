using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPMS.Models.Common;


namespace EPMS.Models.RequestModels
{
    public class DepartmentSearchRequest : GetPagedListRequest
    {
        public long DepartmentId { get; set; }
        public string DepartmentNameE { get; set; }
        public string DepartmentNameA { get; set; }
        public string DepartmentDesc { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        public DepartmentByColumn DepapartmentByColumn
        {
            get
            {
                return (DepartmentByColumn)SortBy;
            }
            set
            {
                SortBy = (short)value;
            }
        }
    }
}
