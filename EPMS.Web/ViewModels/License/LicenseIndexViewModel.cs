using System.Collections.Generic;
using EPMS.Web.Models;

namespace EPMS.Web.ViewModels.License
{
    public class LicenseIndexViewModel
    {
        public IEnumerable<LicenseControlPanel> LicenseControlPanels { get; set; }
    }
}