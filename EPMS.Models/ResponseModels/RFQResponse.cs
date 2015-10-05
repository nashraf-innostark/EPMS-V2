using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class RFQResponse
    {
        public IEnumerable<RFQ> Rfqs { get; set; }
        public RFQ Rfq { get; set; }
        /// <summary>
        /// Total Count
        /// </summary>
        public int TotalCount { get; set; }
    }
}
