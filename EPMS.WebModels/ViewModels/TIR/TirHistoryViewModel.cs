using System.Collections.Generic;

namespace EPMS.WebModels.ViewModels.TIR
{
    public class TirHistoryViewModel
    {
        public TirHistoryViewModel()
        {
            Tirs = new List<WebModels.WebsiteModels.TIR>();
            RecentTir = new WebModels.WebsiteModels.TIR();
            TirItems = new List<WebModels.WebsiteModels.TIRItem>();
        }
        public IList<WebModels.WebsiteModels.TIR> Tirs { get; set; }
        public WebModels.WebsiteModels.TIR RecentTir { get; set; }
        public IList<WebModels.WebsiteModels.TIRItem> TirItems { get; set; }
    }
}