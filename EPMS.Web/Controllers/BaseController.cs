﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using EPMS.Implementation.Identity;
using EPMS.Interfaces.IServices;
using EPMS.Models.DomainModels;
using EPMS.Models.MenuModels;
using EPMS.Models.RequestModels.NotificationRequestModels;
using EPMS.WebBase.UnityConfiguration;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Practices.Unity;

namespace EPMS.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        #region Private

        private ApplicationUserManager _userManager;
        private IMenuRightsService menuRightService;
        private IUserPrefrencesService userPrefrencesService;
        private INotificationService notificationService;
        private void SetCultureInfo()
        {
            CultureInfo info;
            if (Session["Culture"] != null)
            {
                info = new CultureInfo(Session["Culture"].ToString());
            }
            else
            {
                userPrefrencesService = UnityWebActivator.Container.Resolve<IUserPrefrencesService>();
                var userPrefrences = userPrefrencesService.LoadPrefrencesByUserId(User.Identity.GetUserId());
                info = userPrefrences != null
                ? new CultureInfo(userPrefrences.Culture)
                : new CultureInfo("en");
                Session["Culture"] = info.Name;
            }
            info.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            System.Threading.Thread.CurrentThread.CurrentCulture = info;
            System.Threading.Thread.CurrentThread.CurrentUICulture = info;
        }

        private void GetNotifications()
        {
            notificationService = UnityWebActivator.Container.Resolve<INotificationService>();
            NotificationRequestParams requestParams=new NotificationRequestParams();
            object userPermissionSet = System.Web.HttpContext.Current.Session["UserPermissionSet"];
            if (userPermissionSet != null)
            {
                string[] userPermissionsSet = (string[]) userPermissionSet;
                requestParams.SystemGenerated = userPermissionsSet.Contains("SystemGenerated");

                requestParams.RoleName = Session["RoleName"].ToString();
                requestParams.CustomerId = Convert.ToInt64(Session["CustomerID"]);
                requestParams.EmployeeId = Convert.ToInt64(Session["EmployeeID"]);
                requestParams.UserId = Session["UserID"].ToString();

                //Session["NotificationCount"] = notificationService.LoadUnreadNotificationsCount(requestParams);
                ViewBag.Notifications = notificationService.LoadUnreadNotificationsCount(requestParams);
            }
        }
        #endregion

        #region Protected
        // GET: Base
        protected override async void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            if (Session["FullName"] == null || Session["FullName"].ToString() == string.Empty)
                SetUserDetail();
            //Set culture info
            SetCultureInfo();
            //Get Notifications Count
            GetNotifications();
        }
        #endregion

        #region Public

//when isForce =  true it sets the value, no matter session has or not
        public void SetUserDetail()
        {
            Session["FullName"] = Session["UserID"] = string.Empty;

            if (!User.Identity.IsAuthenticated) return;
            AspNetUser result =
                HttpContext.GetOwinContext()
                    .GetUserManager<ApplicationUserManager>()
                    .FindById(User.Identity.GetUserId());
            string role =
                HttpContext.GetOwinContext()
                    .Get<ApplicationRoleManager>()
                    .FindById(result.AspNetRoles.ToList()[0].Id)
                    .Name;
            Session["FullName"] = result.UserName;
            Session["UserID"] = result.Id;
            Session["RoleName"] = role;
            Session["EmployeeID"] = result.EmployeeId;
            Session["CustomerID"] = result.CustomerId;

            menuRightService = UnityWebActivator.Container.Resolve<IMenuRightsService>();

            AspNetUser userResult = UserManager.FindById(User.Identity.GetUserId());
            List<AspNetRole> roles = userResult.AspNetRoles.ToList();
            IList<MenuRight> userRights =
                menuRightService.FindMenuItemsByRoleId(roles[0].Id).ToList();

            string[] userPermissions = userRights.Select(user => user.Menu.PermissionKey).ToArray();
            Session["UserPermissionSet"] = userPermissions;
        }
        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }

        #endregion

    }
}