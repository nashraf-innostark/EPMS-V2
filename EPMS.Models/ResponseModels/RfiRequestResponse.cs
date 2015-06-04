using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class RfiRequestResponse
    {
        public RfiRequestResponse()
        {
            Rfis = new List<RFI>();
        }
        public IEnumerable<RFI> Rfis { get; set; }

        /// <summary>
        /// Total Count
        /// </summary>
        public int TotalCount { get; set; }
    }
}
