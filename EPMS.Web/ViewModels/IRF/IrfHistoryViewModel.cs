using System.Collections.Generic;

namespace EPMS.Web.ViewModels.IRF
{
    public class IrfHistoryViewModel
    {
        public IList<Models.ItemRelease> Irfs { get; set; }
        public Models.ItemRelease RecentIrf { get; set; }
        public IList<Models.ItemReleaseDetail> IrfItems { get; set; }
    }
}