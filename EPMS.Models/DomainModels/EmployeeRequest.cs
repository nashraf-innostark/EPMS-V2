using System;
using System.Collections.Generic;

namespace EPMS.Models.DomainModels
{
    public class EmployeeRequest
    {
        public long RequestId { get; set; }
        public long EmployeeId { get; set; }
        public string RequestTopic { get; set; }
        public DateTime RequestDate { get; set; }
        public bool IsMonetary { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime? RecLastUpdatedDt { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual ICollection<RequestDetail> RequestDetails { get; set; }
    }
}
