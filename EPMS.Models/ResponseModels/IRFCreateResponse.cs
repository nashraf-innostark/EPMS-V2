using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class IRFCreateResponse
    {
        public ItemRelease ItemRelease { get; set; }
        public IEnumerable<ItemReleaseDetail> ItemReleaseDetails { get; set; }
        public IEnumerable<Customer> Customers { get; set; }
        public IEnumerable<RFI> Rfis { get; set; }
        public List<ItemWarehouse> ItemWarehouses { get; set; }
        public List<ItemVariationDropDownListItem> ItemVariationDropDownList { get; set; }
    }
}
