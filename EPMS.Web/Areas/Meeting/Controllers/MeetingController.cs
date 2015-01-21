using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.RequestModels;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers;
using EPMS.Web.Models;
using EPMS.Web.ViewModels.Meeting;

namespace EPMS.Web.Areas.Meeting.Controllers
{
    [Authorize]
    public class MeetingController : BaseController
    {
        #region Private

        private readonly IMeetingService meetingService;
        private readonly IMeetingAttendeeService meetingAttendeeService;
        private readonly IMeetingDocumentService meetingDocumentService;

        #endregion

        #region Constructor

        public MeetingController(IMeetingService meetingService, IMeetingAttendeeService meetingAttendeeService, IMeetingDocumentService meetingDocumentService)
        {
            this.meetingService = meetingService;
            this.meetingAttendeeService = meetingAttendeeService;
            this.meetingDocumentService = meetingDocumentService;
        }

        #endregion

        #region Public

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
    }
}