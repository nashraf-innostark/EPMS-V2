using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class InventoryItemResponse
    {
        public InventoryItemResponse()
        {
            InventoryItems = new List<InventoryItem>();
        }
        public IEnumerable<InventoryItem> InventoryItems { get; set; }
        public int TotalCount { get; set; }
        public int TotalRecords { get; set; }
        public int TotalDisplayRecords { get; set; }
    }
}
