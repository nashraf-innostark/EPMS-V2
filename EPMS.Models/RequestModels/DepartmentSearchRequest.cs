using System;
using EPMS.Models.Common;


namespace EPMS.Models.RequestModels
{
    public class DepartmentSearchRequest : GetPagedListRequest
    {
        public long DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentDesc { get; set; }
        public string RecCreatedBy { get; set; }
        public Nullable<System.DateTime> RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public Nullable<System.DateTime> RecLastUpdatedDt { get; set; }
        
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
