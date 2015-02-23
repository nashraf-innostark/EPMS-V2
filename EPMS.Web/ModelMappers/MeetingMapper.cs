using System;
using System.Globalization;
using System.Linq;
using EPMS.Models.RequestModels;
using DomainModels = EPMS.Models.DomainModels;

namespace EPMS.Web.ModelMappers
{
    public static class MeetingMapper
    {
        /// <summary>
        /// To Convert Meeting in Meeting Model for Meetings List
        /// </summary>
        public static Models.MeetingModel CreateFromMeeting(this DomainModels.Meeting source)
        {
            CultureInfo culture = new CultureInfo("en-US");
            Models.MeetingModel meetingModel = new Models.MeetingModel();
            meetingModel.MeetingId = source.MeetingId;
            meetingModel.TopicName = source.TopicName;
            meetingModel.TopicNameAr = source.TopicNameAr;
            meetingModel.RelatedProject = source.RelatedProject;
            if (source.Date != null)
            meetingModel.Date = source.Date != null ? Convert.ToDateTime(source.Date).ToString("dd/MM/yyyy", culture) : "";
            if (source.Date != null)
                meetingModel.DateString = source.Date != null ? Convert.ToDateTime(source.Date).ToString("dd/MM/yyyy", culture) : "";
            return meetingModel;
        }

        public static Models.Meeting CreateFromServertoClient(this DomainModels.Meeting source)
        {
            CultureInfo culture = new CultureInfo("en-US");
            Models.Meeting meetingModel = new Models.Meeting();
            meetingModel.MeetingId = source.MeetingId;
            meetingModel.TopicName = source.TopicName;
            meetingModel.TopicNameAr = source.TopicNameAr;
            meetingModel.RelatedProject = source.RelatedProject;
            if (source.Date != null)
                meetingModel.Date = Convert.ToDateTime(source.Date).ToString("dd/MM/yyyy", culture);
            meetingModel.Agenda = source.Agenda;
            meetingModel.AgendaAr = source.AgendaAr;
            meetingModel.Discussion = source.Discussion;
            meetingModel.DiscussionAr = source.DiscussionAr;
            meetingModel.Decisions = source.Decisions;
            meetingModel.DecisionsAr = source.DecisionsAr;
            meetingModel.AttendeeName1 = source.AttendeeName1;
            meetingModel.AttendeeEmail1 = source.AttendeeEmail1;
            meetingModel.AttendeeName2 = source.AttendeeName2;   
            meetingModel.AttendeeEmail2 = source.AttendeeEmail2;
            meetingModel.AttendeeName3 = source.AttendeeName3;
            meetingModel.AttendeeEmail3 = source.AttendeeEmail3;
            meetingModel.MeetingAttendees = source.MeetingAttendees.Select(x => x.CreateFromServertoClient());
            meetingModel.AbsenteesList = source.MeetingAttendees.Where(x=>x.Status == true).Select(x => x.CreateFromServertoClient());
            meetingModel.RecCreatedBy = source.RecCreatedBy;
            meetingModel.RecCreatedDt = source.RecCreatedDt;
            meetingModel.RecLastUpdatedBy = source.RecLastUpdatedBy;
            meetingModel.RecLastUpdatedDt = source.RecLastUpdatedDt;
            return meetingModel;
        }

        public static MeetingRequest CreateFrom(this Models.Meeting source)
        {
            DomainModels.Meeting meeting = new DomainModels.Meeting();
            meeting.MeetingId = source.MeetingId;
            meeting.TopicName = source.TopicName;
            meeting.TopicNameAr = source.TopicNameAr;
            meeting.RelatedProject = source.RelatedProject;
            if (source.Date != null)
                meeting.Date = Convert.ToDateTime(source.Date);
            meeting.Agenda = source.Agenda;
            meeting.AgendaAr = source.AgendaAr;
            meeting.Discussion = source.Discussion;
            meeting.DiscussionAr = source.DiscussionAr;
            meeting.Decisions = source.Decisions;
            meeting.DecisionsAr = source.DecisionsAr;
            meeting.AttendeeName1 = source.AttendeeName1;
            meeting.AttendeeEmail1 = source.AttendeeEmail1;
            meeting.AttendeeName2 = source.AttendeeName2;
            meeting.AttendeeEmail2 = source.AttendeeEmail2;
            meeting.AttendeeName3 = source.AttendeeName3;
            meeting.AttendeeEmail3 = source.AttendeeEmail3;
            meeting.RecCreatedBy = source.RecCreatedBy;
            meeting.RecCreatedDt = source.RecCreatedDt;
            meeting.RecLastUpdatedBy = source.RecLastUpdatedBy;
            meeting.RecLastUpdatedDt = source.RecLastUpdatedDt;

            var meetingrequest = new MeetingRequest();
            meetingrequest.EmployeeIds = source.EmployeeIds;
            meetingrequest.AbsentEmployeeIds = source.AbsentEmployeeIds;
            meetingrequest.Meeting = meeting;
            meetingrequest.DocsNames = source.DocsNames;
            return meetingrequest;
        }

        public static DashboardModels.Meeting CreateForDashboard(this DomainModels.Meeting source)
        {
            return new DashboardModels.Meeting
            {
                MeetingId = source.MeetingId,
                Topic = source.TopicName,
                TopicShort = source.TopicName.Length > 25 ? source.TopicName.Substring(0, 25) + "..." : source.TopicName,
                TopicA = source.TopicNameAr,
                TopicAShort = source.TopicNameAr.Length > 25 ? source.TopicNameAr.Substring(0, 25) + "..." : source.TopicNameAr,
                MeetingDate = source.Date != null ? Convert.ToDateTime(source.Date.ToString()).ToShortDateString() : ""
            };
            
        }
    }
}