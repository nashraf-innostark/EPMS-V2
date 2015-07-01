using System.Collections.Generic;
using EPMS.Web.Models;

namespace EPMS.Web.ViewModels.TIR
{
    public class TirHistoryViewModel
    {
        public TirHistoryViewModel()
        {
            Tirs = new List<Models.TIR>();
            RecentTir = new Models.TIR();
            TirItems = new List<TIRItem>();
        }
        public IList<Models.TIR> Tirs { get; set; }
        public Models.TIR RecentTir { get; set; }
        public IList<TIRItem> TirItems { get; set; }
    }
}