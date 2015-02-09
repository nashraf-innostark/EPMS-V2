using EPMS.Models.MenuModels;

namespace EPMS.Web.Models
{
    public class QuickLaunchItems
    {
        public long QuickLaunchId { get; set; }
        public string UserId { get; set; }
        public int MenuId { get; set; }
        public int SortOrder { get; set; }

    }
}