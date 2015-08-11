namespace EPMS.WebModels.WebsiteModels
{
    public class QuickLaunchItems
    {
        public long QuickLaunchId { get; set; }
        public string UserId { get; set; }
        public int MenuId { get; set; }
        public int SortOrder { get; set; }
        public string ImagePath { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }

    }
}