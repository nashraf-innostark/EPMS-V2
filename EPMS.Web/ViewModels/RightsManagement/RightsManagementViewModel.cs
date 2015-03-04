using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.MenuModels;

namespace EPMS.Web.ViewModels.RightsManagement
{
    public class RightsManagementViewModel
    {
        public List<Rights> Rights { get; set; }
        public string SelectedRoleId { get; set; }

        public List<AspNetRole> Roles { get; set; }
    }
}