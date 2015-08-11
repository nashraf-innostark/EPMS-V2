using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.WebModels.ViewModels.Meeting
{
    public class MeetingViewModel
    {
        public MeetingViewModel()
        {
            Meeting = new WebsiteModels.Meeting();            
            //MeetingDocument = new MeetingDocument();
        }
        public WebsiteModels.Meeting Meeting { get; set; }

        public MeetingAttendee MeetingAttendee { get; set; }
        public IEnumerable<WebsiteModels.MeetingAttendee> MeetingAttendees { get; set; }
        //public MeetingDocument MeetingDocument { get; set; }
        public IEnumerable<MeetingDocument> MeetingDocuments { get; set; }
        public IEnumerable<WebsiteModels.Employee> Employees { get; set; }
        
        public string AttendeeName { get; set; }
        public string AttendeeEmail { get; set; }
        public bool? Status { get; set; }
        public string DocsNames { get; set; }

    }
}