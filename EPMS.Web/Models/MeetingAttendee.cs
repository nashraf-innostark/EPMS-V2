using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPMS.Web.Models
{
    public class MeetingAttendee
    {
        public long AttendeeId { get; set; }
        public long MeetingId { get; set; }
        public long? EmployeeId { get; set; }
        public string AttendeeName { get; set; }
        public string AttendeeEmail { get; set; }
        public bool? Status { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime? RecLastUpdatedDt { get; set; }
        public string EmployeeNameE { get; set; }
        public string EmployeeNameA { get; set; }
        public string EmployeeEmail { get; set; }
    }
}