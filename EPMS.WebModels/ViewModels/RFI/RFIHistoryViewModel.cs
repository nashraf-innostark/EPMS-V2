using System.Collections.Generic;

namespace EPMS.WebModels.ViewModels.RFI
{
    public class RFIHistoryViewModel
    {
        public IList<WebsiteModels.RFI> Rfis { get; set; }
        public WebsiteModels.RFI RecentRfi { get; set; }
        public IList<WebsiteModels.RFIItem> RfiItems { get; set; }

    }
}