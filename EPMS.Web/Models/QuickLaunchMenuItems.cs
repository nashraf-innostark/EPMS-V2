using EPMS.Models.MenuModels;

namespace EPMS.Web.Models
{
    /// <summary>
    /// Quick Launch items class
    /// </summary>
    public sealed class QuickLaunchMenuItems
    {
        public int MenuID { get; set; }

        public string ImageIconPath { get; set; }

        public string UrlPath { get; set; }

        public string Name { get; set; }

        
    }
}