using EPMS.Models.MenuModels;

namespace EPMS.Models.DomainModels
{
    public class QuickLaunchItems
    {
        public long QuickLaunchId { get; set; }
        public long UserId { get; set; }
        public int MenuId { get; set; }
        public string SortOrder { get; set; }

        public virtual Menu Menu { get; set; }
    }
}
