using System.Collections.Generic;
using EPMS.Web.Models;

namespace EPMS.Web.ViewModels.IRF
{
    public class IrfHistoryViewModel
    {
        public IrfHistoryViewModel()
        {
            Irfs = new List<ItemRelease>();
            RecentIrf = new ItemRelease();
            IrfItems = new List<ItemReleaseDetail>();
        }
        public IList<ItemRelease> Irfs { get; set; }
        public ItemRelease RecentIrf { get; set; }
        public IList<ItemReleaseDetail> IrfItems { get; set; }
    }
}