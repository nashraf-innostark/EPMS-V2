using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class TIRCreateResponse
    {
        public TIR Tir { get; set; }
        public string LastFormNumber { get; set; }
        public string RequesterNameE { get; set; }
        public string RequesterNameA { get; set; }
        public string ManagerNameE { get; set; }
        public string ManagerNameA { get; set; }

        public IEnumerable<TIRItem> TirItems { get; set; }
        public IEnumerable<ItemVariationDropDownListItem> ItemVariationDropDownList { get; set; }
    }
}
