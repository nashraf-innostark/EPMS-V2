using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;
using Microsoft.AspNet.Identity;

namespace EPMS.Implementation.Services
{
    public class MeetingService : IMeetingService
    {
        private readonly IMeetingRepository meetingRepository;
        private readonly IMeetingAttendeeRepository meetingAttendeeRepository;

        #region Constructor

        public MeetingService(IMeetingRepository meetingRepository, IMeetingAttendeeRepository meetingAttendeeRepository)
        {
            this.meetingRepository = meetingRepository;
            this.meetingAttendeeRepository = meetingAttendeeRepository;
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

        public Meeting AddMeeting(Meeting meeting)
        {
            meetingRepository.Add(meeting);
            meetingRepository.SaveChanges();
            return meeting;
        }

        public bool UpdateMeeting(Meeting meeting)
        {
            meetingRepository.Update(meeting);
            meetingRepository.SaveChanges();
            return true;
        }

        public bool SaveMeeting(MeetingRequest meetingToBeSaved)
        {
            //System.Security.Principal.GenericPrincipal.Current.Identity.Name
            #region Add

            if (meetingToBeSaved.Meeting.MeetingId < 1)
            {
                AddNewMeeting(meetingToBeSaved.Meeting);
                SaveMeetingAttendees(meetingToBeSaved);
            }

            #endregion

            #region Update

            else
            {
                UpdateExistingMeeting(meetingToBeSaved.Meeting);
                UpdateMeetingAttendees(meetingToBeSaved);
            }

            #endregion

            return true;
        }

        private void AddNewMeeting(Meeting meeting)
        {
            meeting.RecCreatedBy = ClaimsPrincipal.Current.Identity.GetUserId();
            meeting.RecCreatedDt = DateTime.Now;
            meeting.RecLastUpdatedBy = ClaimsPrincipal.Current.Identity.GetUserId();
            meeting.RecLastUpdatedDt = DateTime.Now;
            meetingRepository.Add(meeting);
            meetingRepository.SaveChanges();
        }

        private void UpdateExistingMeeting(Meeting meeting)
        {
            meeting.RecLastUpdatedBy = ClaimsPrincipal.Current.Identity.GetUserId();
            meeting.RecLastUpdatedDt = DateTime.Now;
            meetingRepository.Update(meeting);
            meetingRepository.SaveChanges();
        }

        private void SaveMeetingAttendees(MeetingRequest meetingToBeSaved)
        {
            if (meetingToBeSaved.EmployeeIds != null)
            {
                foreach (var attendee in meetingToBeSaved.EmployeeIds)
                {
                    MeetingAttendee meetingAttendee = new MeetingAttendee();
                    meetingAttendee.MeetingId = meetingToBeSaved.Meeting.MeetingId;
                    meetingAttendee.EmployeeId = attendee;
                    meetingAttendeeRepository.Add(meetingAttendee);
                    meetingAttendeeRepository.SaveChanges();
                }
            }
        }

        private void UpdateMeetingAttendees(MeetingRequest meetingToBeSaved)
        {
            IEnumerable<MeetingAttendee> listOfAttendees =
                meetingAttendeeRepository.GetAttendeesByMeetingId(meetingToBeSaved.Meeting.MeetingId);
            var dbList = listOfAttendees.ToList();
            if (meetingToBeSaved.EmployeeIds != null)
            {
                var clientList = meetingToBeSaved.EmployeeIds.ToList();
                if (clientList != null || clientList.Count > 0)
                {
                    #region Add

                    foreach (var empId in clientList)
                    {
                        if (dbList.Any(a => a.EmployeeId == empId))
                            continue;

                        MeetingAttendee meetingAttendee = new MeetingAttendee();
                        meetingAttendee.MeetingId = meetingToBeSaved.Meeting.MeetingId;
                        meetingAttendee.EmployeeId = empId;
                        meetingAttendeeRepository.Add(meetingAttendee);
                        meetingAttendeeRepository.SaveChanges();
                    }

                    #endregion

                    #region Delete

                    foreach (var attendee in dbList)
                    {
                        if (clientList.Any(x => x == attendee.EmployeeId))
                            continue;

                        var attendeeToDelete =
                            meetingAttendeeRepository.GetAttendeeByEmployeeId(Convert.ToInt64(attendee.EmployeeId));
                        meetingAttendeeRepository.Delete(attendeeToDelete);
                        meetingAttendeeRepository.SaveChanges();
                    }

                    #endregion
                }
            }
            else
            {
                foreach (var attendee in dbList)
                {
                    var attendeeToDelete =
                    meetingAttendeeRepository.GetAttendeeByEmployeeId(Convert.ToInt64(attendee.EmployeeId));
                    meetingAttendeeRepository.Delete(attendeeToDelete);
                    meetingAttendeeRepository.SaveChanges();
                }
            }

            IEnumerable<MeetingAttendee> meetingAttendees = meetingAttendeeRepository.GetAttendeesByMeetingId(meetingToBeSaved.Meeting.MeetingId);
            var absenteeDbList = meetingAttendees.ToList();
            if (meetingToBeSaved.EmployeeIds != null && meetingToBeSaved.AbsentEmployeeIds != null)
            {
                var absenteeClientList = meetingToBeSaved.AbsentEmployeeIds.ToList();
                if (absenteeClientList != null || absenteeClientList.Count > 0)
                {
                    #region SetStatusTrue

                    foreach (var empId in absenteeClientList)
                    {
                        if (absenteeDbList.Any(a => a.EmployeeId != empId))
                        {
                            var attendeeToUpdate = meetingAttendeeRepository.GetAttendeeByEmployeeAndMeetingId(Convert.ToInt64(empId), meetingToBeSaved.Meeting.MeetingId);
                            attendeeToUpdate.Status = true;
                            meetingAttendeeRepository.Update(attendeeToUpdate);
                            meetingAttendeeRepository.SaveChanges();
                        }
                    }

                    #endregion

                    #region SetStatusFalse

                    foreach (var attendee in absenteeDbList)
                    {
                        if (absenteeClientList.Any(x => x == attendee.EmployeeId))
                            continue;

                        var attendeeToUpdate =
                        meetingAttendeeRepository.GetAttendeeByEmployeeAndMeetingId(Convert.ToInt64(attendee.EmployeeId), meetingToBeSaved.Meeting.MeetingId);
                        attendeeToUpdate.Status = false;
                        meetingAttendeeRepository.Update(attendeeToUpdate);
                        meetingAttendeeRepository.SaveChanges();
                    }

                    #endregion
                }
            }
            else
            {
                foreach (var attendee in absenteeDbList)
                {
                    var attendeeToUpdate =
                    meetingAttendeeRepository.GetAttendeeByEmployeeAndMeetingId(Convert.ToInt64(attendee.EmployeeId), meetingToBeSaved.Meeting.MeetingId);
                    attendeeToUpdate.Status = false;
                    meetingAttendeeRepository.Update(attendeeToUpdate);
                    meetingAttendeeRepository.SaveChanges();
                }
            }

        }

    }
}