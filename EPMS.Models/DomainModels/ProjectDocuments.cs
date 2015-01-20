using System;

namespace EPMS.Models.DomainModels
{
    public class ProjectDocuments
    {
        public long Id { get; set; }
        public long ProjectId { get; set; }
        public string FileName { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDate { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime RecLastUpdatedDate { get; set; }

        public virtual Project Project { get; set; }
    }
}
