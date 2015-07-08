using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class PCCreateResponse
    {
        public PCCreateResponse()
        {
            PhysicalCountItems = new List<PhysicalCountItem>();
        }
        public string RequesterEmpId { get; set; }
        public string RequesterNameE { get; set; }
        public string RequesterNameA { get; set; }

        public PhysicalCount PhysicalCount { get; set; }
        public IEnumerable<PhysicalCountItem> PhysicalCountItems { get; set; }
        public IEnumerable<Warehouse> Warehouses { get; set; }
    }
}
