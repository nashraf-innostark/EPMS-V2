using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EPMS.WebModels.ViewModels.Admin
{
    public class UserViewModel
    {
        /// <summary>
        /// Data
        /// </summary>
        public List<WebsiteModels.SystemUser> Data { get; set; }
        public string SelectedRoleId { get; set; }

        public List<IdentityRole> Roles { get; set; }
    }
}