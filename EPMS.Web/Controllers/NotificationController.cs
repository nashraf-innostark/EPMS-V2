using System;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.RequestModels.NotificationRequestModels;
using EPMS.Models.ResponseModels.NotificationResponseModel;
using EPMS.Web.ViewModels.Common;

namespace EPMS.Web.Controllers
{
    [Authorize]
    public class NotificationController : BaseController
    {
        private readonly INotificationService notificationService;

        public NotificationController(INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }

        // GET: Notification
        public ActionResult Index()
        {
            NotificationListView viewModel = new NotificationListView();
            viewModel.NotificationSearchRequest = new NotificationListViewRequest();

            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Index(NotificationListViewRequest searchRequest)
        {
            searchRequest.SearchString = Request["search"];
            if (Session["RoleName"] != null && Session["RoleName"].ToString() == "Admin")
            {
                searchRequest.Requester = "Admin";
            }
            else
            {
                searchRequest.Requester = Session["EmployeeID"].ToString();
            }
            var resultData = notificationService.LoadAllNotifications(searchRequest);
            return Json(resultData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Create(long? id)
        {
            NotificationViewModel notificationViewModel = notificationService.LoadNotificationAndBaseData(id);
            return View(notificationViewModel);
        }
        [HttpPost]
        public ActionResult Create(NotificationViewModel notificationViewModel)
        {
            try
            {
                if (notificationViewModel.NotificationResponse.NotificationId > 0)
                {
                    notificationViewModel.NotificationResponse.RecLastUpdatedBy = Session["UserID"].ToString();
                    notificationViewModel.NotificationResponse.RecLastUpdatedDate = DateTime.Now;
                    if (notificationService.UpdateNotification(notificationViewModel.NotificationResponse))
                    {
                        TempData["message"] = new MessageViewModel
                        {
                            Message = Resources.Notification.Notification.NotificationSentMsg,
                            IsUpdated = true
                        };
                    }
                }
                else
                {
                    notificationViewModel.NotificationResponse.RecCreatedBy = Session["UserID"].ToString();
                    notificationViewModel.NotificationResponse.RecCreatedDate = DateTime.Now;
                    notificationViewModel.NotificationResponse.RecLastUpdatedBy = Session["UserID"].ToString();
                    notificationViewModel.NotificationResponse.RecLastUpdatedDate = DateTime.Now;
                    if (notificationService.AddNotification(notificationViewModel.NotificationResponse))
                    {
                        TempData["message"] = new MessageViewModel
                        {
                            Message = Resources.Notification.Notification.NotificationSentMsg,
                            IsSaved = true
                        };
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                
            }
            return View(notificationViewModel);
        }
        public ActionResult Details(long? id)
        {
            NotificationViewModel notificationViewModel = notificationService.LoadNotificationAndBaseData(id);
            if(notificationViewModel.NotificationResponse.NotificationId>0)
                return View(notificationViewModel);
            return RedirectToAction("Index");
        }
    }
}