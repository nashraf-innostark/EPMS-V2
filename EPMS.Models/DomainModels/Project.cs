using System;
using System.Collections.Generic;

namespace EPMS.Models.DomainModels
{
    public class Project
    {
        public long ProjectId { get; set; }
        public string NameE { get; set; }
        public string NameA { get; set; }
        public long? CustomerId { get; set; }
        public long? OrderId { get; set; }
        public string SerialNo { get; set; }
        public string DescriptionE { get; set; }
        public string DescriptionA { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Status { get; set; }
        public string NotesE { get; set; }
        public string NotesA { get; set; }
        public string NotesForCustomerE { get; set; }
        public string NotesForCustomerA { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDate { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime RecLastUpdatedDate { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Order Order { get; set; }
        public virtual ICollection<ProjectTask> ProjectTasks { get; set; }
    }
}
