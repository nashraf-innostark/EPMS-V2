using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace EPMS.Web.Models
{
    public class Request
    {
        public Request()
        {
            RequestDetail = new RequestDetail();
        }
        public string EmployeeNameE { get; set; }
        public string EmployeeNameA { get; set; }
        public string DepartmentNameE { get; set; }
        public string DepartmentNameA { get; set; }
        public long RequestId { get; set; }
        public long EmployeeId { get; set; }
        public string EmployeeJobId { get; set; }
        [Required(ErrorMessage = "Topic is required.")]
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
        public string RequestDateString { get; set; }
        public RequestDetail RequestDetail { get; set; }
    }
}