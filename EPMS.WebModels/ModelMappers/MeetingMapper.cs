using System;
using System.Globalization;
using System.Linq;
using EPMS.Models.RequestModels;

namespace EPMS.WebModels.ModelMappers
{
    public static class MeetingMapper
    {
        /// <summary>
        /// To Convert Meeting in Meeting Model for Meetings List
        /// </summary>
        public static WebsiteModels.MeetingModel CreateFromMeeting(this Models.DomainModels.Meeting source)
        {
            CultureInfo culture = new CultureInfo("en-US");
            WebsiteModels.MeetingModel meetingModel = new WebsiteModels.MeetingModel();
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

        public static WebsiteModels.Meeting CreateFromServertoClient(this Models.DomainModels.Meeting source)
        {
            CultureInfo culture = new CultureInfo("en-US");
            WebsiteModels.Meeting meetingModel = new WebsiteModels.Meeting();
            meetingModel.MeetingId = source.MeetingId;
            meetingModel.TopicName = source.TopicName;
            meetingModel.TopicNameAr = source.TopicNameAr;
            meetingModel.RelatedProject = source.RelatedProject;
            if (source.Date != null)
                meetingModel.Date = Convert.ToDateTime(source.Date).ToString("dd/MM/yyyy", culture);
            var agenda = source.Agenda;
            if (!String.IsNullOrEmpty(source.Agenda))
            {
                agenda = agenda.Replace("\n", "");
                agenda = agenda.Replace("\r", "");
                agenda = agenda.Replace("\t", "");
            }
            meetingModel.Agenda = agenda;
            var agendaA = source.AgendaAr;
            if (!String.IsNullOrEmpty(source.AgendaAr))
            {
                agendaA = agendaA.Replace("\n", "");
                agendaA = agendaA.Replace("\r", "");
                agendaA = agendaA.Replace("\t", "");
            }
            meetingModel.AgendaAr = agendaA;
            var discussion = source.Discussion;
            if (!String.IsNullOrEmpty(source.Agenda))
            {
                discussion = discussion.Replace("\n", "");
                discussion = discussion.Replace("\r", "");
                discussion = discussion.Replace("\t", "");
            }
            meetingModel.Discussion = discussion;
            var discussionA = source.DiscussionAr;
            if (!String.IsNullOrEmpty(source.Agenda))
            {
                discussionA = discussionA.Replace("\n", "");
                discussionA = discussionA.Replace("\r", "");
                discussionA = discussionA.Replace("\t", "");
            }
            meetingModel.DiscussionAr = discussionA;
            var decision = source.Decisions;
            if (!String.IsNullOrEmpty(source.Agenda))
            {
                decision = decision.Replace("\n", "");
                decision = decision.Replace("\r", "");
                decision = decision.Replace("\t", "");
            }
            meetingModel.Decisions = decision;
            var decisionA = source.DecisionsAr;
            if (!String.IsNullOrEmpty(source.Agenda))
            {
                decisionA = decisionA.Replace("\n", "");
                decisionA = decisionA.Replace("\r", "");
                decisionA = decisionA.Replace("\t", "");
            }
            meetingModel.DecisionsAr = decisionA;
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

        public static MeetingRequest CreateFrom(this WebsiteModels.Meeting source)
        {
            Models.DomainModels.Meeting meeting = new Models.DomainModels.Meeting();
            meeting.MeetingId = source.MeetingId;
            meeting.TopicName = source.TopicName;
            meeting.TopicNameAr = source.TopicNameAr;
            meeting.RelatedProject = source.RelatedProject;
            if (source.Date != null)
                meeting.Date = DateTime.ParseExact(source.Date, "dd/MM/yyyy", new CultureInfo("en"));
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

        public static Models.DashboardModels.Meeting CreateForDashboard(this Models.DomainModels.Meeting source)
        {
            return new Models.DashboardModels.Meeting
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