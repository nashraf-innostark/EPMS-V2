using System.Collections.Generic;

namespace EPMS.WebModels.ViewModels.IRF
{
    public class IrfHistoryViewModel
    {
        public IrfHistoryViewModel()
        {
            Irfs = new List<WebsiteModels.ItemRelease>();
            RecentIrf = new WebsiteModels.ItemRelease();
            IrfItems = new List<WebsiteModels.ItemReleaseDetail>();
        }
        public IList<WebsiteModels.ItemRelease> Irfs { get; set; }
        public WebsiteModels.ItemRelease RecentIrf { get; set; }
        public IList<WebsiteModels.ItemReleaseDetail> IrfItems { get; set; }
    }
}