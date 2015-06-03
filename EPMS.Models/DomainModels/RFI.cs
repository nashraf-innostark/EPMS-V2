using System.Collections.Generic;

namespace EPMS.Models.DomainModels
{
    public class RFI
    {
        public long RFIId { get; set; }
        public long OrderId { get; set; }
        public string UsageE { get; set; }
        public string UsageA { get; set; }
        public string NotesE { get; set; }
        public string NotesA { get; set; }
        public int Status { get; set; }
        public string RecCreatedBy { get; set; }
        public System.DateTime RecCreatedDate { get; set; }
        public string RecUpdatedBy { get; set; }
        public System.DateTime RecUpdatedDate { get; set; }
        public string ManagerId { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual Order Order { get; set; }
        public virtual ICollection<RFIItem> RFIItems { get; set; }
        public virtual ICollection<ItemRelease> ItemReleases { get; set; }
    }
}
