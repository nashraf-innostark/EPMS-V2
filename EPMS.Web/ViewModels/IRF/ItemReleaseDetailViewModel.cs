using System.Collections.Generic;
using EPMS.Web.Models;

namespace EPMS.Web.ViewModels.IRF
{
    public class ItemReleaseDetailViewModel
    {
        public ItemRelease ItemRelease { get; set; }
        public IEnumerable<ItemReleaseDetail> ItemReleaseDetails { get; set; }
    }
}