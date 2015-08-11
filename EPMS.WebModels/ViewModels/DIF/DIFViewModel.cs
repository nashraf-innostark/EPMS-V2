using System.Collections.Generic;
using EPMS.Models.ResponseModels;

namespace EPMS.WebModels.ViewModels.DIF
{
    public class DIFViewModel
    {
        public DIFViewModel()
        {
            Dif = new WebsiteModels.DIF();
        }
        public WebsiteModels.DIF Dif { get; set; }
        public List<WebsiteModels.DIFItem> DifItem { get; set; }
        public IEnumerable<ItemVariationDropDownListItem> ItemVariationDropDownList { get; set; }
    }
}