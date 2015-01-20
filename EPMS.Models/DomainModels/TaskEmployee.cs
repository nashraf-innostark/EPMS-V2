using System;

namespace EPMS.Models.DomainModels
{
    public class TaskEmployee
    {
        public long TaskEmployeeId { get; set; }
        public long TaskId { get; set; }
        public long EmployeeId { get; set; }
        public DateTime? RecCreatedDt { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecLastUpdatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }

        public virtual  ProjectTask ProjectTask { get; set; }
    }
}
