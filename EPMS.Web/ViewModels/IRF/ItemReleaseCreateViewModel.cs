using System.Collections.Generic;
using EPMS.Models.ResponseModels;
using EPMS.Web.Models;
using ItemReleaseDetail = EPMS.Models.DomainModels.ItemReleaseDetail;

namespace EPMS.Web.ViewModels.IRF
{
    public class ItemReleaseCreateViewModel
    {
        public ItemReleaseCreateViewModel()
        {
            Rfis = new List<Models.RFI>();
            ItemVariationDropDownList = new List<ItemVariationDropDownListItem>();
        }

        public ItemRelease ItemRelease { get; set; }
        public List<Models.ItemReleaseDetail> ItemReleaseDetails { get; set; }
        public List<Models.ItemReleaseQuantity> ReleaseQuantities { get; set; }
        public List<Models.Customer> Customers { get; set; }
        public List<Models.RFI> Rfis { get; set; }
        public List<ItemWarehouse> ItemWarehouses { get; set; }
        //public List<Models.RFIItem> RfiItem { get; set; }
        public List<ItemVariationDropDownListItem> ItemVariationDropDownList { get; set; }
    }
}