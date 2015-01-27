using System.Collections.Generic;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Implementation.Services
{
    public class MeetingService : IMeetingService
    {
        private readonly IMeetingRepository meetingRepository;

        #region Constructor

        public MeetingService(IMeetingRepository meetingRepository)
        {
            this.meetingRepository = meetingRepository;
        }

        #endregion

        public IEnumerable<Meeting> GetAll()
        {
            return meetingRepository.GetAll();
        }

        public MeetingResponse GetMeetings(MeetingSearchRequest meetingResponse)
        {
            return meetingRepository.GetAllMeetings(meetingResponse);
        }

        public Meeting FindMeetingById(long id)
        {
            return meetingRepository.Find((int)id);
        }

        public bool AddMeeting(Meeting meeting)
        {
            meetingRepository.Add(meeting);
            meetingRepository.SaveChanges();
            return true;
        }

        public IEnumerable<Meeting> LoadMeetingsForDashboard(string requester)
        {
            return meetingRepository.GetMeetingsForDashboard(requester);
        }
    }
}
