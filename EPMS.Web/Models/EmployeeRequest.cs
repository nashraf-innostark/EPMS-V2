using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Display(Name = "Topic")]
        public string RequestTopic { get; set; }
        [Display(Name = "Request Date")]
        public DateTime RequestDate { get; set; }
        [Display(Name = "Monetary")]
        public bool IsMonetary { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime? RecLastUpdatedDt { get; set; }

        public Employee Employee { get; set; }
        public IEnumerable<RequestDetail> RequestDetails { get; set; }
    }
}