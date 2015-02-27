using System;
using System.Configuration;
using System.DirectoryServices.ActiveDirectory;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EPMS.WebBase.Mvc
{
    /// <summary>
    /// Site Authorize Attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class SiteAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// Check if user is authorized on a given permissionKey
        /// </summary>
        private bool IsAuthorized()
        {
            // check license
            var licenseKeyEncrypted = ConfigurationManager.AppSettings["LicenseKey"].ToString(CultureInfo.InvariantCulture);
            LicenseKey = EncryptDecrypt.StringCipher.Decrypt(licenseKeyEncrypted, "123");
            var splitLicenseKey = LicenseKey.Split('|');
            Domain = splitLicenseKey[0];
            MacAddress = splitLicenseKey[1];
            NoOfUsers = splitLicenseKey[2];
            ExpiryDate = DateTime.ParseExact(splitLicenseKey[3], "dd/MM/yyyy", new CultureInfo("en"));
            Modules = splitLicenseKey[4].Split(';');

            // check MAC Address
            string userMacAddress = GetMacAddress();
            if (MacAddress != userMacAddress)
            {
                return false;
            }

            // check Domain
            var dir = AppDomain.CurrentDomain.BaseDirectory;
            if (dir != Domain)
            {
                return false;
            }

            //// check license expiry date
            DateTime currDate = DateTime.Now;
            if (currDate > ExpiryDate)
            {
                return false;
            }

            object userPermissionSet = HttpContext.Current.Session["UserPermissionSet"];
            var permissionToSpecificController = false;
            //if (PermissionKey != "HRS" || PermissionKey != "Mt")
            //{
            if (userPermissionSet != null)
            {
                string[] userPermissionsSet = (string[])userPermissionSet;
                //permissionToSpecificController = (userPermissionsSet.Contains(PermissionKey));
                PermissionKeys = PermissionKey.Split(',');
                foreach (var permissionKey in PermissionKeys)
                {
                    if (userPermissionsSet.Contains(permissionKey))
                        permissionToSpecificController = true;
                }
            //}
            // check allowed modules
            bool permissionToModule = true;
            if (IsModule)
            {
                if (!Modules.Contains(PermissionKey))
                {
                    permissionToModule = false;
                }
            }

            if (permissionToModule && permissionToSpecificController)
            {
                return true;
            }

            return false;
        }
        /// <summary>
        /// Perform the authorization
        /// </summary>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            return IsAuthorized();
        }
        /// <summary>
        /// Redirects request to unauthroized request page
        /// </summary>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
            else
            {
                filterContext.Result =
                    new RedirectToRouteResult(
                        new RouteValueDictionary(
                            new { area = "", controller = "UnauthorizedRequest", action = "Index" }));
            }
        }

        #region Get MAC Address
        public static string GetMacAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty)// only return MAC Address from first card  
                {
                    //IPInterfaceProperties properties = adapter.GetIPProperties(); Line is not required
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                }
            } return sMacAddress;
        }
        #endregion

        public string PermissionKey { get; set; }
        public string[] PermissionKeys { get; set; }
        public bool IsModule { get; set; }
        public string LicenseKey { get; set; }
        public string Domain { get; set; }
        public string MacAddress { get; set; }
        public string[] Modules { get; set; }
        public string NoOfUsers { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}