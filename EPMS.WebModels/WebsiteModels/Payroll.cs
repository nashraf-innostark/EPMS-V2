using System;
using System.Collections.Generic;

namespace EPMS.WebModels.WebsiteModels
{
    public class Payroll
    {
        public long RequestId { get; set; }
        public long EmployeeId { get; set; }
        public string RequestTopic { get; set; }
        public string JobTitle { get; set; }
        public double? BasicSalary { get; set; }
        public double? Allowance1 { get; set; }
        public double? Allowance2 { get; set; }
        public double? Allowance3 { get; set; }
        public double? Allowance4 { get; set; }
        public double? Allowance5 { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime Date { get; set; }
        public bool IsMonetary { get; set; }
        public IEnumerable<RequestDetail> RequestDetails { get; set; }
    }
}