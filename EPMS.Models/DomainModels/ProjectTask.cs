using System;
using System.Collections.Generic;

namespace EPMS.Models.DomainModels
{
    public class ProjectTask
    {
        public long TaskId { get; set; }
        public long? CustomerId { get; set; }
        public long ProjectId { get; set; }
        public string TaskNameE { get; set; }
        public string TaskNameA { get; set; }
        public string DescriptionE { get; set; }
        public string DescriptionA { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal TotalCost { get; set; }
        public string TotalWeight { get; set; }
        public string NotesE { get; set; }
        public string NotesA { get; set; }
        public string TaskProgress { get; set; }
        public DateTime? RecCreatedDt { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecLastUpdatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Project Project { get; set; }
        public virtual ICollection<TaskEmployee> TaskEmployees { get; set; }
        public virtual ICollection<ProjectTask> PreRequisitTask { get; set; }
        public virtual ICollection<ProjectTask> ProjectTasks { get; set; }
    }
}
