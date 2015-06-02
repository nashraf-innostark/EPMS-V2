using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class RifRequestResponse
    {
        public RifRequestResponse()
        {
            Rifs = new List<RIF>();
        }
        public IEnumerable<RIF> Rifs { get; set; }

        /// <summary>
        /// Total Count
        /// </summary>
        public int TotalCount { get; set; }
    }
}
