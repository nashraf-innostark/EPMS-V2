using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using EPMS.Models.RequestModels;
using DomainModels = EPMS.Models.DomainModels;
using Models = EPMS.Web.Models;

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
            meetingModel.Date = source.Date;
            if (source.Date != null)
                meetingModel.DateString = source.Date != null ? Convert.ToDateTime(source.Date).ToString("dd/MM/yyyy", culture) : "";
            return meetingModel;
        }

        public static Models.Meeting CreateFromServertoClient(this DomainModels.Meeting source)
        {
            CultureInfo culture = new CultureInfo("en-US");
            return new Models.Meeting
            {
                MeetingId = source.MeetingId,
                TopicName = source.TopicName,
                TopicNameAr = source.TopicNameAr,
                RelatedProject = source.RelatedProject,
                Date = Convert.ToDateTime(source.Date).ToString("dd/MM/yyyy", culture),
                Agenda = source.Agenda,
                AgendaAr = source.AgendaAr,
                Discussion = source.Discussion,
                DiscussionAr = source.DiscussionAr,
                Decisions = source.Decisions,
                DecisionsAr = source.DecisionsAr,
                AttendeeName1 = source.AttendeeName1,
                AttendeeEmail1 = source.AttendeeEmail1,
                AttendeeName2 = source.AttendeeName2,   
                AttendeeEmail2 = source.AttendeeEmail2,
                AttendeeName3 = source.AttendeeName3,
                AttendeeEmail3 = source.AttendeeEmail3,
                MeetingAttendees = source.MeetingAttendees.Select(x => x.CreateFromServertoClient()),
                AbsenteesList = source.MeetingAttendees.Where(x=>x.Status == true).Select(x => x.CreateFromServertoClient()),
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt
            };
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