using System.Collections.Generic;

namespace EPMS.Models.DomainModels
{
    public class POF
    {
        public long Id { get; set; }
        public string RecCreatedBy { get; set; }
        public System.DateTime RecCreatedDate { get; set; }
        public string RecUpdatedBy { get; set; }
        public System.DateTime RecUpdatedDate { get; set; }
        public string NotesA { get; set; }
        public string NotesE { get; set; }
        public int? Status { get; set; }
        public string ManagerId { get; set; }

        public virtual ICollection<POFItem> POFItems { get; set; }
    }
}
