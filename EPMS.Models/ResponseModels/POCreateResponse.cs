using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class POCreateResponse
    {
        public PurchaseOrder Order { get; set; }
        public string LastFormNumber { get; set; }
        public string RequesterNameE { get; set; }
        public string RequesterNameA { get; set; }

        public IEnumerable<PurchaseOrderItem> OrderItems { get; set; }
        public IEnumerable<ItemVariationDropDownListItem> ItemVariationDropDownList { get; set; }
    }
}
