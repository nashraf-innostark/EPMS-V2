using System;
using System.Collections.Generic;

namespace EPMS.Web.Models
{
    public class EmployeeRequest
    {
        public EmployeeRequest()
        {
            Employee = new Employee();
            RequestDetails = new List<RequestDetail>();
        }

        public long RequestId { get; set; }
        public long EmployeeId { get; set; }
        public string RequestTopic { get; set; }
        public System.DateTime RequestDate { get; set; }
        public bool IsMonetary { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime? RecLastUpdatedDt { get; set; }

        public Employee Employee { get; set; }
        public IEnumerable<RequestDetail> RequestDetails { get; set; }
    }
}