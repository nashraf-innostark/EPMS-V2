using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.IServices
{
    public interface IMeetingService
    {
        /// <summary>
        /// Get All Meetings
        /// </summary>
        IEnumerable<Meeting> GetAll();
        /// <summary>
        /// Get Meetings by Search Request
        /// </summary>
        MeetingResponse GetMeetings(MeetingSearchRequest meetingResponse);
        Meeting FindMeetingById(long id);
        Meeting AddMeeting(Meeting meeting);
        bool UpdateMeeting(Meeting meeting);
        IEnumerable<Meeting> LoadMeetingsForDashboard(string requester);

        bool SaveMeeting(MeetingRequest meetinToBeSaved);
    }
}
