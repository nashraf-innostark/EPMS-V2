using System;

namespace EPMS.Web.Models
{
    public class ProjectTask
    {
        public long TaskId { get; set; }
        public long CustomerId { get; set; }
        public long ProjectId { get; set; }
        public string TaskNameE { get; set; }
        public string TaskNameA { get; set; }
        public string DescriptionE { get; set; }
        public string DescriptionA { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal TotalCost { get; set; }
        public decimal TotalWeight { get; set; }
        public string NotesE { get; set; }
        public string NotesA { get; set; }
        public int? TaskProgress { get; set; }
        public DateTime? RecCreatedDt { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecLastUpdatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
    }
}