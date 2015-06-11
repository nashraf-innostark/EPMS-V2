using System.Collections.Generic;

namespace EPMS.Web.ViewModels.RIF
{
    public class RifHistoryViewModel
    {
        public IList<Models.RIF> Rifs { get; set; }
        public Models.RIF RecentRif { get; set; }
        public IList<Models.RIFItem> RifItems { get; set; }
    }
}