using System;
using System.Collections.Generic;

namespace EPMS.Models.DomainModels
{
    public class RFIHistory
    {
        public long RFIId { get; set; }
        public long OrderId { get; set; }
        public string UsageE { get; set; }
        public string UsageA { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDate { get; set; }
        public string RecUpdatedBy { get; set; }
        public DateTime RecUpdatedDate { get; set; }
        public string NotesA { get; set; }
        public string NotesE { get; set; }
        public int? Status { get; set; }
        public string ManagerId { get; set; }
        public long ParentId { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }
        public virtual Order Order { get; set; }
        public virtual ICollection<RFIItemHistory> RFIItemHistories { get; set; }
    }
}
