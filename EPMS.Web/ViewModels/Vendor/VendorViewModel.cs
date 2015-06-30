using System.Collections.Generic;
using EPMS.Models.ResponseModels;
using EPMS.Web.Models;

namespace EPMS.Web.ViewModels.Vendor
{
    public class VendorViewModel
    {
        public VendorViewModel()
        {
            Vendor = new Models.Vendor {VendorItems = new List<EPMS.Models.DomainModels.VendorItem>()};
        }
        public Models.Vendor Vendor { get; set; }
        public IEnumerable<Models.Vendor> VendorList { get; set; }
        public IEnumerable<Models.PurchaseOrder> PurchaseOrders { get; set; }
        public IEnumerable<ItemVariationDropDownListItem> ItemVariationDropDownList { get; set; }

    }
}