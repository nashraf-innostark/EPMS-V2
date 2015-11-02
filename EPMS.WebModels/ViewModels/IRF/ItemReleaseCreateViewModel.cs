﻿using System.Collections.Generic;
using EPMS.Models.Common;
using EPMS.Models.ResponseModels;
using EPMS.WebModels.WebsiteModels;

namespace EPMS.WebModels.ViewModels.IRF
{
    public class ItemReleaseCreateViewModel
    {
        public ItemReleaseCreateViewModel()
        {
            Rfis = new List<WebsiteModels.RFI>();
            ItemReleaseDetails = new List<ItemReleaseDetail>();
            ItemVariationDropDownList = new List<ItemVariationDropDownListItem>();
        }

        public WebsiteModels.ItemRelease ItemRelease { get; set; }
        public List<WebsiteModels.ItemReleaseDetail> ItemReleaseDetails { get; set; }
        public List<WebsiteModels.ItemReleaseQuantity> ReleaseQuantities { get; set; }
        public List<EmployeeForDropDownList> Employees { get; set; }
        public List<WebsiteModels.RFI> Rfis { get; set; }
        public List<WebsiteModels.ItemWarehouse> ItemWarehouses { get; set; }
        //public List<Models.RFIItem> RfiItem { get; set; }
        public List<ItemVariationDropDownListItem> ItemVariationDropDownList { get; set; }
    }
}