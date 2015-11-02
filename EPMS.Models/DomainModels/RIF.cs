using System;
using System.Collections.Generic;

namespace EPMS.Models.DomainModels
{
    public class RIF
    {
        public long RIFId { get; set; }
        public long OrderId { get; set; }
        public string ReturningReasonE { get; set; }
        public string ReturningReasonA { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDate { get; set; }
        public string RecUpdatedBy { get; set; }
        public DateTime RecUpdatedDate { get; set; }
        public string NotesA { get; set; }
        public string NotesE { get; set; }
        public int Status { get; set; }
        public string ManagerId { get; set; }
        public string FormNumber { get; set; }
        public long? IRFId { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }
        public virtual Order Order { get; set; }
        public virtual ICollection<RIFItem> RIFItems { get; set; }
        public virtual ItemRelease ItemRelease { get; set; }
    }
}
