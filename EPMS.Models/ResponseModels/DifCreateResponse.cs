﻿using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class DifCreateResponse
    {
        public DIF Rif { get; set; }
        public string RequesterNameE { get; set; }        
        public string RequesterNameA { get; set; }
        public string ManagerNameE { get; set; }
        public string ManagerNameA { get; set; }

        public IEnumerable<DIFItem> RifItem { get; set; }
        public IEnumerable<ItemVariationDropDownListItem> ItemVariationDropDownList { get; set; }
    }
}
