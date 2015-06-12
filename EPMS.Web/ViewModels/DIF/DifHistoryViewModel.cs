using System.Collections.Generic;

namespace EPMS.Web.ViewModels.DIF
{
    public class DifHistoryViewModel
    {
        public IList<Models.DIF> Difs { get; set; }
        public Models.DIF RecentDif { get; set; }
        public IList<Models.DIFItem> DifItems { get; set; }
    }
}