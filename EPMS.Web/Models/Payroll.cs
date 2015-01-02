using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPMS.Web.Models
{
    public class Payroll
    {
        public long RequestId { get; set; }
        public long EmployeeId { get; set; }
        public string RequestTopic { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime Date { get; set; }
        public bool IsMonetary { get; set; }
        public IEnumerable<RequestDetail> RequestDetails { get; set; }
    }
}