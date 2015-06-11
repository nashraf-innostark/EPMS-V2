using System.Collections.Generic;

namespace EPMS.Web.ViewModels.RFI
{
    public class RFIHistoryViewModel
    {
        public IList<Models.RFI> Rfis { get; set; }
        public Models.RFI RecentRfi { get; set; }
        public IList<Models.RFIItem> RfiItems { get; set; }

    }
}