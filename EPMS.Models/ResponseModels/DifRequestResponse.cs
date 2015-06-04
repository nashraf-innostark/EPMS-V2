using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class DifRequestResponse
    {
        public DifRequestResponse()
        {
            Difs = new List<DIF>();
        }
        public IEnumerable<DIF> Difs { get; set; }

        /// <summary>
        /// Total Count
        /// </summary>
        public int TotalCount { get; set; }
    }
}
