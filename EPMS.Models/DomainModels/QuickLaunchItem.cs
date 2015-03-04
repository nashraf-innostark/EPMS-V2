using EPMS.Models.MenuModels;

namespace EPMS.Models.DomainModels
{
    public class QuickLaunchItem
    {
        public long QuickLaunchId { get; set; }
        public string UserId { get; set; }
        public int MenuId { get; set; }
        public int SortOrder { get; set; }

        public virtual Menu Menu { get; set; }
    }
}
