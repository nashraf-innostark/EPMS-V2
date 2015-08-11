using System.Collections.Generic;

namespace EPMS.WebModels.ViewModels.RIF
{
    public class RifHistoryViewModel
    {
        public IList<WebsiteModels.RIF> Rifs { get; set; }
        public WebsiteModels.RIF RecentRif { get; set; }
        public IList<WebsiteModels.RIFItem> RifItems { get; set; }
    }
}