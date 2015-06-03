using System.Collections.Generic;
using EPMS.Models.ResponseModels;

namespace EPMS.Web.ViewModels.IRF
{
    public class ItemReleaseFormCreateViewModel
    {
        public ItemReleaseFormCreateViewModel()
        {
            Rfis = new List<Models.RFI>();
            ItemVariationDropDownList = new List<ItemVariationDropDownListItem>();
        }
        public List<DashboardModels.Customer> Customers { get; set; }
        public List<Models.RFI> Rfis { get; set; }
        public List<Models.RFIItem> RfiItem { get; set; }
        public List<ItemVariationDropDownListItem> ItemVariationDropDownList { get; set; }
    }
}