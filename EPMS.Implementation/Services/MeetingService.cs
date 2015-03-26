using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;
using EPMS.Models.ResponseModels.NotificationResponseModel;
using Microsoft.AspNet.Identity;

namespace EPMS.Implementation.Services
{
    public class MeetingService : IMeetingService
    {
        private readonly IAspNetUserRepository aspNetUserRepository;
        private readonly INotificationRepository notificationRepository;
        private readonly IMeetingRepository meetingRepository;
        private readonly INotificationService notificationService;
        private readonly IMeetingAttendeeRepository meetingAttendeeRepository;
        private readonly IEmployeeRepository employeeRepository;
        private readonly IMeetingDocumentRepository meetingDocumentRepository;
        private readonly IMeetingAttendeeService meetingAttendeeService;
        private readonly IMeetingDocumentService meetingDocumentService;
        private readonly IEmployeeService employeeService;

        #region Constructor

        public MeetingService(IAspNetUserRepository aspNetUserRepository, INotificationRepository notificationRepository, IMeetingRepository meetingRepository, INotificationService notificationService, IMeetingAttendeeRepository meetingAttendeeRepository, IEmployeeRepository employeeRepository, IMeetingDocumentRepository meetingDocumentRepository, IMeetingAttendeeService meetingAttendeeService, IMeetingDocumentService meetingDocumentService, IEmployeeService employeeService)
        {
            this.aspNetUserRepository = aspNetUserRepository;
            this.notificationRepository = notificationRepository;
            this.meetingRepository = meetingRepository;
            this.notificationService = notificationService;
            this.meetingAttendeeRepository = meetingAttendeeRepository;
            this.employeeRepository = employeeRepository;
            this.meetingDocumentRepository = meetingDocumentRepository;
            this.meetingAttendeeService = meetingAttendeeService;
            this.meetingDocumentService = meetingDocumentService;
            this.employeeService = employeeService;
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
        public MeetingResponse GetMeetingsResponse(long id)
        {
            MeetingResponse response = new MeetingResponse
            {
                Meeting = new Meeting(),
                MeetingAttendees = new List<MeetingAttendee>(),
                MeetingDocuments = new List<MeetingDocument>()
            };
            if (id > 0)
            {
                response.Meeting = FindMeetingById(id);
                response.MeetingAttendees = meetingAttendeeService.FindAttendeeByMeetingId(id);
                response.MeetingDocuments = meetingDocumentService.FindDocumentByMeetingId(id);
            }
            response.Employees = employeeService.GetAll();
            return response;
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
        /// <summary>
        /// Save Meeting from Model
        /// </summary>
        public SaveMeetingResponse SaveMeeting(MeetingRequest meetingToBeSaved)
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
            if (meetingToBeSaved.EmployeeIds != null)
                SendNotification(meetingToBeSaved.Meeting, meetingToBeSaved.EmployeeIds);
            SaveMeetingDocuments(meetingToBeSaved);
            return new SaveMeetingResponse
            {
                EmployeeEmails = GetEmployeeEmails(meetingToBeSaved)
            };

            #endregion

        }
        /// <summary>
        /// Add New Meeting
        /// </summary>
        private void AddNewMeeting(Meeting meeting)
        {
            meeting.RecCreatedBy = ClaimsPrincipal.Current.Identity.GetUserId();
            meeting.RecCreatedDt = DateTime.Now;
            meeting.RecLastUpdatedBy = ClaimsPrincipal.Current.Identity.GetUserId();
            meeting.RecLastUpdatedDt = DateTime.Now;
            meetingRepository.Add(meeting);
            meetingRepository.SaveChanges();
        }
        /// <summary>
        /// Update Existing Meeting
        /// </summary>
        /// <param name="meeting"></param>
        private void UpdateExistingMeeting(Meeting meeting)
        {
            meeting.RecLastUpdatedBy = ClaimsPrincipal.Current.Identity.GetUserId();
            meeting.RecLastUpdatedDt = DateTime.Now;
            meetingRepository.Update(meeting);
            meetingRepository.SaveChanges();
        }
        /// <summary>
        /// Add New Meeting Attendees if Any
        /// </summary>
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
        /// <summary>
        /// Update Meeting Attendees in case of Update Meeting
        /// </summary>
        /// <param name="meetingToBeSaved"></param>
        private void UpdateMeetingAttendees(MeetingRequest meetingToBeSaved)
        {
            //Add or Delete Meeting Attendees in case of Update Meeting
            IEnumerable<MeetingAttendee> listOfAttendees =
                meetingAttendeeRepository.GetAttendeesByMeetingId(meetingToBeSaved.Meeting.MeetingId);
            var dbList = listOfAttendees.ToList();
            if (meetingToBeSaved.EmployeeIds != null)
            {
                var clientList = meetingToBeSaved.EmployeeIds.ToList();
                if (clientList != null || clientList.Count > 0)
                {
                    //Add New Meeting Attendes 
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
                    //Delete Meeting Attendees
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
                //Delete Attendees if List from Client is Empty
                foreach (var attendee in dbList)
                {
                    var attendeeToDelete =
                    meetingAttendeeRepository.GetAttendeeByEmployeeId(Convert.ToInt64(attendee.EmployeeId));
                    meetingAttendeeRepository.Delete(attendeeToDelete);
                    meetingAttendeeRepository.SaveChanges();
                }
            }

            //Update Attendees Status to Absent
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
                //Set Status False if List from Client is Empty
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
        public IEnumerable<Meeting> LoadMeetingsForDashboard(string requester)
        {
            return meetingRepository.GetMeetingsForDashboard(requester);
        }
        /// <summary>
        /// Get Employee Emails for Email Invitation
        /// </summary>m
        private IEnumerable<string> GetEmployeeEmails(MeetingRequest meetingToBeSaved)
        {
            return employeeRepository.FindEmployeeEmailById(meetingToBeSaved.EmployeeIds);
        }
        /// <summary>
        /// Save Meeting Documents
        /// </summary>
        private void SaveMeetingDocuments(MeetingRequest meetingToBeSaved)
        {
            //Save file names in db
            if (!string.IsNullOrEmpty(meetingToBeSaved.DocsNames))
            {
                //WHAT IF DOCUMENT UPDATE CASE?
                var meetingDocuments = meetingToBeSaved.DocsNames.Substring(0, meetingToBeSaved.DocsNames.Length - 1).Split('~').ToList();
                foreach (var meetingDocument in meetingDocuments)
                {
                    MeetingDocument doc = new MeetingDocument
                    {
                        FileName = meetingDocument,
                        MeetingId = meetingToBeSaved.Meeting.MeetingId,
                        RecCreatedDate = DateTime.Now,
                        RecLastUpdatedDate = DateTime.Now
                    };
                    meetingDocumentRepository.Add(doc);
                    meetingDocumentRepository.SaveChanges();
                }
            }
        }
        public void SendNotification(Meeting meeting, List<long> employeeIds)
        {
            NotificationViewModel notificationViewModel = new NotificationViewModel();

            #region Send notification to admin
            notificationViewModel.NotificationResponse.NotificationId =
                        notificationRepository.GetNotificationsIdByCategories(4, 0, meeting.MeetingId);

            notificationViewModel.NotificationResponse.TitleE = ConfigurationManager.AppSettings["MeetingE"];
            notificationViewModel.NotificationResponse.TitleA = ConfigurationManager.AppSettings["MeetingA"];
            notificationViewModel.NotificationResponse.AlertBefore = Convert.ToInt32(ConfigurationManager.AppSettings["MeetingAlertBefore"]); //Days

            notificationViewModel.NotificationResponse.CategoryId = 4; //Meetings
            notificationViewModel.NotificationResponse.SubCategoryId = 0;
            notificationViewModel.NotificationResponse.ItemId = meeting.MeetingId;

            notificationViewModel.NotificationResponse.AlertDate = DateTime.Now.ToShortDateString();
            notificationViewModel.NotificationResponse.AlertDateType = 1; //0=Hijri, 1=Gregorian
            notificationViewModel.NotificationResponse.ForAdmin = false;
            notificationViewModel.NotificationResponse.SystemGenerated = true;

            notificationService.AddUpdateMeetingNotification(notificationViewModel, employeeIds);

            #endregion
        }
    }
}