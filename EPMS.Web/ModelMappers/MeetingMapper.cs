using System;
using System.Collections.Generic;
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
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt
            };
            var meetingrequest = new MeetingRequest();
            meetingrequest.EmployeeIds = source.EmployeeIds;
            meetingrequest.Meeting = meeting;
            return meetingrequest;
        }
    }
}