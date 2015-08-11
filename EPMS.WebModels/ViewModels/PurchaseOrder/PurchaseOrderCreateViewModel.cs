using System.Collections.Generic;
using EPMS.Models.ResponseModels;

namespace EPMS.WebModels.ViewModels.PurchaseOrder
{
    public class PurchaseOrderCreateViewModel
    {
        public PurchaseOrderCreateViewModel()
        {
            Order = new WebsiteModels.PurchaseOrder();
            PoItems = new List<WebsiteModels.PurchaseOrderItem>();
        }
        public WebsiteModels.PurchaseOrder Order { get; set; }
        public IList<WebsiteModels.PurchaseOrderItem> PoItems { get; set; }
        public IEnumerable<ItemVariationDropDownListItem> ItemVariationDropDownList { get; set; }
    }
}