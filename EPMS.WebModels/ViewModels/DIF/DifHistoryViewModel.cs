using System.Collections.Generic;

namespace EPMS.WebModels.ViewModels.DIF
{
    public class DifHistoryViewModel
    {
        public IList<WebsiteModels.DIF> Difs { get; set; }
        public WebsiteModels.DIF RecentDif { get; set; }
        public IList<WebsiteModels.DIFItem> DifItems { get; set; }
    }
}