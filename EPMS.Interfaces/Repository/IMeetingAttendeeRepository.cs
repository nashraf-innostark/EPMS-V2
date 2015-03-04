using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    /// <summary>
    /// Meeting Attendee Repository
    /// </summary>
    public interface IMeetingAttendeeRepository : IBaseRepository<MeetingAttendee, long>
    {
        /// <summary>
        /// Get Attendees by Meeting Id
        /// </summary>
        IEnumerable<MeetingAttendee> GetAttendeesByMeetingId(long meetingId);
        MeetingAttendee GetAttendeeByEmployeeId(long employeeId);
        MeetingAttendee GetAttendeeByEmployeeAndMeetingId(long employeeId, long meetingId);
    }
}
