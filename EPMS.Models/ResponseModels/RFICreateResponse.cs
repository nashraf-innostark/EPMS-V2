﻿using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class RFICreateResponse
    {
        public RFI Rfi { get; set; }
        public string CustomerNameE { get; set; }
        public string RequesterNameE { get; set; }
        public string ManagerNameE { get; set; }
        public string CustomerNameA { get; set; }
        public string RequesterNameA { get; set; }
        public string ManagerNameA { get; set; }
        public string OrderNo { get; set; }

        public IEnumerable<RFIItem> RfiItem { get; set; }
        public IEnumerable<ItemVariationDropDownListItem> ItemVariationDropDownList { get; set; }

        public IEnumerable<Customer> Customers { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
