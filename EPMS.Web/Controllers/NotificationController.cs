using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.ResponseModels.NotificationResponseModel;

namespace EPMS.Web.Controllers
{
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
            return View();
        }
        public ActionResult Create(long? id)
        {
            NotificationViewModel notificationViewModel = notificationService.LoadNotificationAndBaseData(id);
            return View(notificationViewModel);
        }

        public ActionResult Detail(long id)
        {
            return View();
        }
    }
}