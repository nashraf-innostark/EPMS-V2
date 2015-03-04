using System.Collections.Generic;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;

namespace EPMS.Implementation.Services
{
    public class MeetingAttendeeService : IMeetingAttendeeService
    {
        private readonly IMeetingAttendeeRepository meetingAttendeeRepository;

        #region Constructor

        public MeetingAttendeeService(IMeetingAttendeeRepository meetingAttendeeRepository)
        {
            this.meetingAttendeeRepository = meetingAttendeeRepository;
        }
        
        #endregion

        public bool AddMeetingAttendee(MeetingAttendee attendee)
        {
            meetingAttendeeRepository.Add(attendee);
            meetingAttendeeRepository.SaveChanges();
            return true;
        }

        public bool UpdateMeetingAttendee(MeetingAttendee attendee)
        {
            meetingAttendeeRepository.Update(attendee);
            meetingAttendeeRepository.SaveChanges();
            return true;
        }

        public bool DeleteAttendeeByMeetingAndEmployeeId(long meetingId, long employeeId)
        {
            var attendee = meetingAttendeeRepository.GetAttendeeByEmployeeId(employeeId);
            if (attendee !=null)
            {
                meetingAttendeeRepository.Delete(attendee);
                meetingAttendeeRepository.SaveChanges();
                return true;
            }
            return false;
        }

        public IEnumerable<MeetingAttendee> FindAttendeeByMeetingId(long meetingId)
        {
            return meetingAttendeeRepository.GetAttendeesByMeetingId(meetingId);
        }
    }
}
