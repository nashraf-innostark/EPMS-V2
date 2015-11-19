using System;
using System.Collections.Generic;

namespace EPMS.Models.DomainModels
{
    public class ReportProjectTask
    {
        public long TaskId { get; set; }
        public long ProjectId { get; set; }
        public string TaskNameE { get; set; }
        public string TaskNameA { get; set; }
        public string DescriptionE { get; set; }
        public string DescriptionA { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public decimal TotalCost { get; set; }
        public Nullable<decimal> TotalWeight { get; set; }
        public string NotesE { get; set; }
        public string NotesA { get; set; }
        public Nullable<decimal> TaskProgress { get; set; }
        public Nullable<System.DateTime> RecCreatedDt { get; set; }
        public string RecCreatedBy { get; set; }
        public Nullable<System.DateTime> RecLastUpdatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public bool IsParent { get; set; }
        public Nullable<long> ParentTask { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public bool IsDeleted { get; set; }
        public int NoOfSubTasks { get; set; }
        public long ReportId { get; set; }
        public virtual ReportProject ReportProject { get; set; }
        public virtual ICollection<ReportProjectTask> ReportProjectSubTasks { get; set; }
        public virtual ReportProjectTask ReportProjectParentTask { get; set; }
        public virtual Report Report { get; set; }
        public virtual ICollection<ReportTaskEmployee> ReportTaskEmployees { get; set; }
    }
}
