using System;
using System.Collections.Generic;
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
            return new Models.MeetingModel()
            {
                MeetingId = source.MeetingId,
                TopicName = source.TopicName,
                TopicNameAr = source.TopicNameAr,
                RelatedProject = source.RelatedProject,
                Date = source.Date,
                DateString = source.Date != null ? Convert.ToDateTime(source.Date.ToString()).ToShortDateString() : ""
            };
        }

        public static Models.Meeting CreateFromServertoClient(this DomainModels.Meeting source)
        {
            return new Models.Meeting
            {
                MeetingId = source.MeetingId,
                TopicName = source.TopicName,
                TopicNameAr = source.TopicNameAr,
                RelatedProject = source.RelatedProject,
                Date = source.Date,
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
            var meeting = new DomainModels.Meeting
            {
                MeetingId = source.MeetingId,
                TopicName = source.TopicName,
                TopicNameAr = source.TopicNameAr,
                RelatedProject = source.RelatedProject,
                Date = source.Date,
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
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt
            };
            var meetingrequest = new MeetingRequest();
            meetingrequest.EmployeeIds = source.EmployeeIds;
            meetingrequest.AbsentEmployeeIds = source.AbsentEmployeeIds;
            meetingrequest.Meeting = meeting;
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
                MeetingDate = Convert.ToDateTime(source.Date.ToString()).ToShortDateString()
            };
            
        }
    }
}