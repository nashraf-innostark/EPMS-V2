using System;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.RequestModels.NotificationRequestModels;
using EPMS.Models.ResponseModels.NotificationResponseModel;
using EPMS.Web.ViewModels.Common;
using EPMS.WebBase.Mvc;
using System.Linq;
using Microsoft.AspNet.Identity;

namespace EPMS.Web.Controllers
{
    [Authorize]
    [SiteAuthorize(PermissionKey = "NS", IsModule = true)]
    public class NotificationController : BaseController
    {
        private readonly INotificationService notificationService;

        public NotificationController(INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }

        #region List Views
        
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
                object userPermissionSet = System.Web.HttpContext.Current.Session["UserPermissionSet"];
                if (userPermissionSet != null)
                {
                    string[] userPermissionsSet = (string[]) userPermissionSet;
                    searchRequest.NotificationRequestParams.SystemGenerated = userPermissionsSet.Contains("SystemGenerated");
                }
                searchRequest.NotificationRequestParams.RoleName = Session["RoleName"].ToString();
                searchRequest.NotificationRequestParams.CustomerId = Convert.ToInt64(Session["CustomerID"]);
                searchRequest.NotificationRequestParams.EmployeeId = Convert.ToInt64(Session["EmployeeID"]);
                searchRequest.NotificationRequestParams.UserId = Session["UserID"].ToString();

                var resultData = notificationService.LoadAllNotifications(searchRequest);
                return Json(resultData, JsonRequestBehavior.AllowGet);
            }
            return RedirectToAction("Login", "Account");
        }

        [SiteAuthorize(PermissionKey = "NotificationSent")]
        public ActionResult Sent()
        {
            NotificationListView viewModel = new NotificationListView();
            viewModel.NotificationSearchRequest = new NotificationListViewRequest();

            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Sent(NotificationListViewRequest searchRequest)
        {
            searchRequest.SearchString = Request["search"];
            if (Session["UserID"] != null)
            {
                searchRequest.NotificationRequestParams.RoleName = Session["RoleName"].ToString();
                searchRequest.NotificationRequestParams.CustomerId = Convert.ToInt64(Session["CustomerID"]);
                searchRequest.NotificationRequestParams.EmployeeId = Convert.ToInt64(Session["EmployeeID"]);
                searchRequest.NotificationRequestParams.UserId = Session["UserID"].ToString();
                var resultData = notificationService.LoadAllSentNotifications(searchRequest);
                return Json(resultData, JsonRequestBehavior.AllowGet);
            }
            return RedirectToAction("Login", "Account");
        }
        #endregion

        #region Create/Update/Details

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
                notificationViewModel.NotificationResponse.RecCreatedBy = Session["UserID"].ToString();
                if (notificationService.AddUpdateNotification(notificationViewModel.NotificationResponse))
                {
                    TempData["message"] = new MessageViewModel
                    {
                        Message = Resources.Notification.Notification.NotificationSentMsg,
                        IsSaved = true
                    };
                    return RedirectToAction("Sent");
                }
            }
            catch (Exception exception)
            {
                
            }
            return View(notificationViewModel);
        }
        [SiteAuthorize(PermissionKey = "NotificationDetails")]
        public ActionResult Details(long? id)
        {
            NotificationViewModel notificationViewModel = notificationService.LoadNotificationDetailsAndBaseData(id,User.Identity.GetUserId());
            if(notificationViewModel.NotificationResponse.NotificationId>0)
                return View(notificationViewModel);
            return RedirectToAction("Index");
        }
        #endregion

        #region Send Email/SMS Notifications
        [AllowAnonymous]
        public void SendEmails()
        {
            notificationService.SendEmailNotifications();
        }
        #endregion
    }
}