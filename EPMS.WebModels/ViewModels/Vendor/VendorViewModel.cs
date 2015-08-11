using System.Collections.Generic;
using EPMS.Models.ResponseModels;

namespace EPMS.WebModels.ViewModels.Vendor
{
    public class VendorViewModel
    {
        public VendorViewModel()
        {
            Vendor = new EPMS.WebModels.WebsiteModels.Vendor { VendorItems = new List<EPMS.Models.DomainModels.VendorItem>() };
        }
        public EPMS.WebModels.WebsiteModels.Vendor Vendor { get; set; }
        public IEnumerable<EPMS.WebModels.WebsiteModels.Vendor> VendorList { get; set; }
        public IEnumerable<WebsiteModels.PurchaseOrder> PurchaseOrders { get; set; }
        public IEnumerable<ItemVariationDropDownListItem> ItemVariationDropDownList { get; set; }

    }
}