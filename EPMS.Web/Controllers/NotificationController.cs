using System;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.RequestModels.NotificationRequestModels;
using EPMS.Models.ResponseModels.NotificationResponseModel;
using EPMS.Web.ViewModels.Common;
using EPMS.WebBase.Mvc;

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
        [SiteAuthorize(PermissionKey = "NS")]
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
            if (Session["UserID"] != null)
            {
                searchRequest.RoleName = Session["RoleName"].ToString();
                searchRequest.CustomerId = Convert.ToInt64(Session["CustomerID"]);
                searchRequest.EmployeeId = Convert.ToInt64(Session["EmployeeID"]);
                searchRequest.UserId = Session["UserID"].ToString();
                var resultData = notificationService.LoadAllNotifications(searchRequest);
                return Json(resultData, JsonRequestBehavior.AllowGet);
            }
            return RedirectToAction("Login", "Account");
        }

        [SiteAuthorize(PermissionKey = "NotificationCreate")]
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