using System.Collections.Generic;
using EPMS.Models.ResponseModels;

namespace EPMS.Web.ViewModels.DIF
{
    public class DIFViewModel
    {
        public DIFViewModel()
        {
            Dif = new Models.DIF();
        }
        public Models.DIF Dif { get; set; }
        public List<Models.DIFItem> DifItem { get; set; }
        public IEnumerable<ItemVariationDropDownListItem> ItemVariationDropDownList { get; set; }
        public IEnumerable<DashboardModels.WarehousDDL> Warehouses { get; set; }
    }
}