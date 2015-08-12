using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers;
using EPMS.Web.Models;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.Meeting;
using EPMS.WebBase.Mvc;

namespace EPMS.Web.Areas.Meeting.Controllers
{
    [Authorize]
    [SiteAuthorize(PermissionKey = "Mt", IsModule = true)]
    public class MeetingController : BaseController
    {
        #region Private

        private readonly IMeetingService meetingService;
        private readonly IMeetingAttendeeService meetingAttendeeService;
        private readonly IMeetingDocumentService meetingDocumentService;
        private readonly IEmployeeService employeeService;

        #region SendInvitation

        private void SendInvitation(MeetingViewModel meetingViewModel, SaveMeetingResponse response)
        {
            IEnumerable<string> emEmails = response.EmployeeEmails;
            if (response.EmployeeEmails.Any() || IfAttendesOtherThanEmployee(meetingViewModel))
            {
                string empemails = emEmails.Aggregate("", (current, item) => current + "," + item);
                string emails = "";
                if (!string.IsNullOrEmpty(empemails))
                {
                    emails = empemails.Substring(1, empemails.Length - 1);
                }
                if (!string.IsNullOrEmpty(meetingViewModel.Meeting.AttendeeEmail1))
                {
                    emails = emails + "," + meetingViewModel.Meeting.AttendeeEmail1;
                }
                if (!string.IsNullOrEmpty(meetingViewModel.Meeting.AttendeeEmail2))
                {
                    emails = emails + "," + meetingViewModel.Meeting.AttendeeEmail2;
                }
                if (!string.IsNullOrEmpty(meetingViewModel.Meeting.AttendeeEmail3))
                {
                    emails = emails + "," + meetingViewModel.Meeting.AttendeeEmail3;
                }
                string emailSubject = "Meeting Invitation";
                string emailBody = "You are invited to attend the Meeting " + meetingViewModel.Meeting.TopicName +
                                   " on " + meetingViewModel.Meeting.Date;
                if (!string.IsNullOrEmpty(emails))
                {
                    Utility.SendEmailAsync(emails, emailSubject, emailBody);
                }
            }
        }

        #endregion

        #endregion

        #region Check If Attendes Other Than Employee

        private bool IfAttendesOtherThanEmployee(MeetingViewModel viewModel)
        {
            if (!string.IsNullOrEmpty(viewModel.Meeting.AttendeeEmail1) || !string.IsNullOrEmpty(viewModel.Meeting.AttendeeEmail2) || !string.IsNullOrEmpty(viewModel.Meeting.AttendeeEmail3))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Constructor

        public MeetingController(IMeetingService meetingService, IMeetingAttendeeService meetingAttendeeService, IMeetingDocumentService meetingDocumentService, IEmployeeService employeeService)
        {
            this.meetingService = meetingService;
            this.meetingAttendeeService = meetingAttendeeService;
            this.meetingDocumentService = meetingDocumentService;
            this.employeeService = employeeService;
        }

        #endregion

        #region Public

        #region Index
        public ActionResult Index()
        {
            MeetingSearchRequest meetingSearchRequest = new MeetingSearchRequest();
            MeetingListViewModel metMeetingListViewModel = new MeetingListViewModel
            {
                SearchRequest = meetingSearchRequest
            };

            return View(metMeetingListViewModel);
        }

        [HttpPost]
        public ActionResult Index(MeetingSearchRequest meetingSearchRequest)
        {
            meetingSearchRequest.SearchString = Request["search"];
            var meetings = meetingService.GetMeetings(meetingSearchRequest);

            List<MeetingModel> meetingList =
                meetings.Meetings.Select(x => x.CreateFromMeeting()).ToList();

            MeetingListViewModel meetingListViewModel = new MeetingListViewModel
            {
                aaData = meetingList,
                iTotalRecords = Convert.ToInt32(meetings.TotalCount),
                iTotalDisplayRecords = Convert.ToInt32(meetingList.Count()),
                sEcho = meetingSearchRequest.sEcho,
            };

            return Json(meetingListViewModel, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Create/Update
        [SiteAuthorize(PermissionKey = "MeetingCreate,MeetingDetails")]
        public ActionResult Create(long? id)
        {
            MeetingViewModel meetingViewModel = new MeetingViewModel();
            MeetingResponse response = id > 0 ? meetingService.GetMeetingsResponse((long)id) : meetingService.GetMeetingsResponse(0);
            if (id != null)
            {
                var meeting = response.Meeting;
                if (meeting != null)
                {
                    meetingViewModel.Meeting = meeting.CreateFromServertoClient();
                    if (meeting.MeetingAttendees.Count > 0)
                    {
                        meetingViewModel.MeetingAttendees = response.MeetingAttendees.Select(x => x.CreateFromServertoClient());
                    }
                    if (meeting.MeetingDocuments.Count > 0)
                    {
                        meetingViewModel.MeetingDocuments = response.MeetingDocuments;
                    }
                }
            }
            meetingViewModel.Employees = employeeService.GetAll().Select(x => x.CreateFromServerToClient());
            return View(meetingViewModel);
        }

        [HttpPost]
        [ValidateInput(false)] //this is due to CK Editor
        [SiteAuthorize(PermissionKey = "MeetingCreate")]
        public ActionResult Create(MeetingViewModel meetingViewModel)
        {
            MeetingRequest toBeSaveMeeting = meetingViewModel.Meeting.CreateFrom();
            SaveMeetingResponse response = meetingService.SaveMeeting(toBeSaveMeeting);
            {
                TempData["message"] = new MessageViewModel { Message = "Added", IsSaved = true };
                meetingViewModel.Meeting.MeetingId = toBeSaveMeeting.Meeting.MeetingId;
                //SaveMeetingDocuments(meetingViewModel);
                if (Request.Form["SendInvitation"] != null)
                {
                    SendInvitation(meetingViewModel, response);
                }
                return RedirectToAction("Index");
            }
        }

        #endregion

        //#region SaveMeetingDocuments
        //public void SaveMeetingDocuments(MeetingViewModel meetingViewModel)
        //{
        //    //Save file names in db
        //    if (!string.IsNullOrEmpty(meetingViewModel.DocsNames))
        //    {
        //        var meetingDocuments = meetingViewModel.DocsNames.Substring(0, meetingViewModel.DocsNames.Length - 1).Split('~').ToList();
        //        foreach (var meetingDocument in meetingDocuments)
        //        {
        //            MeetingDocument doc = new MeetingDocument();
        //            doc.FileName = meetingDocument;
        //            doc.MeetingId = meetingViewModel.Meeting.MeetingId;
        //            doc.RecCreatedDate = DateTime.Now;
        //            doc.RecLastUpdatedDate = DateTime.Now;
        //            meetingDocumentService.AddMeetingDocument(doc);
        //        }
        //    }
        //}
        //#endregion

        #region UploadDocuments
        public ActionResult UploadDocuments()
        {
            HttpPostedFileBase doc = Request.Files[0];
            var filename = "";
            try
            {
                //Save File to Folder
                if ((doc != null))
                {
                    filename = (DateTime.Now.ToString(CultureInfo.InvariantCulture) + doc.FileName);//concat date time with file name
                    Regex pattern = new Regex("[;|:|,|-|_|+|/| ]");
                    filename = pattern.Replace(filename, "");//remove some characters and spaces from file name
                    var filePathOriginal = Server.MapPath(ConfigurationManager.AppSettings["MeetingDocuments"]);
                    string savedFileName = Path.Combine(filePathOriginal, filename);
                    doc.SaveAs(savedFileName);
                }
            }
            catch (Exception exp)
            {
                return Json(new { response = "Failed to upload. Error: " + exp.Message, status = (int)HttpStatusCode.BadRequest }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { filename = filename, size = doc.ContentLength / 1024 + "KB", response = "Successfully uploaded!", status = (int)HttpStatusCode.OK }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region DeleteDocuments
        [HttpGet]
        public JsonResult DeleteDocument(long fileId)
        {

            var filePathOriginal = Server.MapPath(ConfigurationManager.AppSettings["MeetingDocuments"]);
            MeetingDocument document = meetingDocumentService.FindMeetingDocumentById(fileId);
            if (document != null)
            {
                string savedFilePhysicalPath = Path.Combine(filePathOriginal, document.FileName);
                if ((System.IO.File.Exists(savedFilePhysicalPath)))
                {
                    System.IO.File.Delete(savedFilePhysicalPath);
                    var documentDeleted = meetingDocumentService.Delete(fileId);
                    return Json(documentDeleted, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region DownloadDocuments
        public FileResult Download(string fileName)
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath(ConfigurationManager.AppSettings["MeetingDocuments"] + fileName));
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
        #endregion


        #endregion
    }
}