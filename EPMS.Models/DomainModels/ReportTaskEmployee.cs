using System;

namespace EPMS.Models.DomainModels
{
    public class ReportTaskEmployee
    {
        public long TaskEmployeeId { get; set; }
        public long TaskId { get; set; }
        public Nullable<System.DateTime> RecCreatedDt { get; set; }
        public string RecCreatedBy { get; set; }
        public Nullable<System.DateTime> RecLastUpdatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public string EmployeeFirstNameE { get; set; }
        public string EmployeeMiddleNameE { get; set; }
        public string EmployeeLastNameE { get; set; }
        public string EmployeeFirstNameA { get; set; }
        public string EmployeeMiddleNameA { get; set; }
        public string EmployeeLastNameA { get; set; }

        public virtual ReportProjectTask ReportProjectTask { get; set; }
    }
}
