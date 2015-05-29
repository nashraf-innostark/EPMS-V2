using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class RFICreateResponse
    {
        public RFI Rfi { get; set; }
        public string RecCreatedByName { get; set; }
        public IEnumerable<RFIItem> RfiItem { get; set; }
        public IEnumerable<ItemVariationDropDownListItem> ItemVariationDropDownList { get; set; }

        public IEnumerable<Customer> Customers { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
