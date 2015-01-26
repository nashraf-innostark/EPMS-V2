﻿using System;
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
                DateString = Convert.ToDateTime(source.Date.ToString()).ToShortDateString()
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
                DiscusionAr = source.DiscussionAr,
                Decisions = source.Decisions,
                DecisionsAr = source.DecisionsAr,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt
            };
        }

        public static DashboardModels.Meeting CreateForDashboard(this DomainModels.Meeting source)
        {
            return new DashboardModels.Meeting
            {
                MeetingId = source.MeetingId,
                TopicName = source.TopicName,
                TopicNameAr = source.TopicNameAr,
                MeetingDate = Convert.ToDateTime(source.Date.ToString()).ToShortDateString()
            };
        }
    }
}