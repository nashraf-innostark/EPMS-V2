﻿using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface IMeetingAttendeeService
    {
        /// <summary>
        /// Add Meeting Attendees
        /// </summary>
        bool AddMeetingAttendee(MeetingAttendee attendee);
        /// <summary>
        /// Update Meeting Attendees
        /// </summary>
        bool UpdateMeetingAttendee(MeetingAttendee attendee);
        /// <summary>
        /// Find Attendees by Meeting
        /// </summary>
        IEnumerable<MeetingAttendee> FindAttendeeByMeetingId(long meetingId);

    }
}
