using System.Collections.Generic;

namespace EPMS.WebModels.ViewModels.IRF
{
    public class ItemReleaseDetailViewModel
    {
        public ItemReleaseDetailViewModel()
        {
            ItemRelease = new WebsiteModels.ItemRelease();
            ItemReleaseDetails = new List<WebsiteModels.ItemReleaseDetail>();
        }
        public WebsiteModels.ItemRelease ItemRelease { get; set; }
        public IEnumerable<WebsiteModels.ItemReleaseDetail> ItemReleaseDetails { get; set; }
    }
}