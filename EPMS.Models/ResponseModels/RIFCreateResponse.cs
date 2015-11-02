using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class RifCreateResponse
    {
        public RIF Rif { get; set; }
        public string CustomerNameE { get; set; }
        public string RequesterNameE { get; set; }
        public string ManagerNameE { get; set; }
        public string CustomerNameA { get; set; }
        public string RequesterNameA { get; set; }
        public string ManagerNameA { get; set; }
        public string OrderNo { get; set; }
        public string EmpJobId { get; set; }
        public string LastFormNumber { get; set; }

        public IEnumerable<RIFItem> RifItem { get; set; }
        public IEnumerable<ItemVariationDropDownListItem> ItemVariationDropDownList { get; set; }

        public IEnumerable<ItemRelease> ItemReleases { get; set; }
        public IEnumerable<Customer> Customers { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<ItemWarehouse> ItemWarehouses { get; set; }
    }
}
