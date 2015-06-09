using System.Collections.Generic;
using EPMS.Models.ResponseModels;

namespace EPMS.Web.ViewModels.PurchaseOrder
{
    public class PurchaseOrderCreateViewModel
    {
        public PurchaseOrderCreateViewModel()
        {
            Order = new Models.PurchaseOrder();
            Items = new List<Models.PurchaseOrderItem>();
        }
        public Models.PurchaseOrder Order { get; set; }
        public IList<Models.PurchaseOrderItem> Items { get; set; }
        public IEnumerable<ItemVariationDropDownListItem> ItemVariationDropDownList { get; set; }
    }
}