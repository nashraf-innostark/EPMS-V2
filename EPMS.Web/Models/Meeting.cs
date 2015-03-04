using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EPMS.Models.DomainModels;

namespace EPMS.Web.Models
{
    public class Meeting
    {
        public long MeetingId { get; set; }
        [Required(ErrorMessage = "Topic Name is required.")]
        [StringLength(200, ErrorMessage = "Topic Name cannot exceed 300 characters. ")]
        public string TopicName { get; set; }
        [Required(ErrorMessage = "Topic Name is required.")]
        [StringLength(200, ErrorMessage = "Topic Name cannot exceed 300 characters. ")]
        public string TopicNameAr { get; set; }
        [StringLength(200, ErrorMessage = "Cannot exceed 300 characters. ")]
        public string RelatedProject { get; set; }
        public string Date { get; set; }
        public DateTime? DateAr { get; set; }
        [StringLength(200, ErrorMessage = "Cannot exceed 1000 characters. ")]
        public string Agenda { get; set; }
        [StringLength(200, ErrorMessage = "Cannot exceed 1000 characters. ")]
        public string AgendaAr { get; set; }
        public string Discussion { get; set; }
        public string DiscussionAr { get; set; }
        public string Decisions { get; set; }
        public string DecisionsAr { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime? RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime? RecLastUpdatedDt { get; set; }
        public string AttendeeName1 { get; set; }
        public string AttendeeEmail1 { get; set; }
        public string AttendeeName2 { get; set; }
        public string AttendeeEmail2 { get; set; }
        public string AttendeeName3 { get; set; }
        public string AttendeeEmail3 { get; set; }
        /// <summary>
        /// Meeting Attendees
        /// </summary>
        public IEnumerable<MeetingAttendee> MeetingAttendees { get; set; }
        /// <summary>
        /// Meeting Documents
        /// </summary>
        public IEnumerable<MeetingDocument> MeetingDocuments { get; set; }

        public IEnumerable<MeetingAttendee> AbsenteesList { get; set; }
        public List<long> EmployeeIds { get; set; }
        public List<long> AbsentEmployeeIds { get; set; }
        public string DocsNames { get; set; }
    }
}