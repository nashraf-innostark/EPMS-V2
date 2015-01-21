using System;
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
    }
}