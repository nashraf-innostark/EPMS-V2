using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPMS.Models.DomainModels;

namespace EPMS.Web.ViewModels.Meeting
{
    public class MeetingViewModel
    {
        public MeetingViewModel()
        {
            Meeting = new Models.Meeting();
            MeetingAttendee = new MeetingAttendee();
            MeetingDocument = new MeetingDocument();
        }
        public Models.Meeting Meeting { get; set; }
        public IEnumerable<Models.Meeting> MeetingList { get; set; }
        public MeetingAttendee MeetingAttendee { get; set; }
        public IEnumerable<MeetingAttendee> MeetingAttendees { get; set; }
        public MeetingDocument MeetingDocument { get; set; }
        public IEnumerable<MeetingDocument> MeetingDocuments { get; set; }
        public IEnumerable<Models.Employee> Employees { get; set; }
        public List<int> EmployeeId { get; set; }
        public string AttendeeName { get; set; }
        public string AttendeeEmail { get; set; }
        public bool? Status { get; set; }

    }
}