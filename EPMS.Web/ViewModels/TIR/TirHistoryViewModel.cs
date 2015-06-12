using System.Collections.Generic;

namespace EPMS.Web.ViewModels.TIR
{
    public class TirHistoryViewModel
    {
        public IList<Models.TIR> Tirs { get; set; }
        public Models.TIR RecentTir { get; set; }
        public IList<Models.TIRItem> TirItems { get; set; }
    }
}