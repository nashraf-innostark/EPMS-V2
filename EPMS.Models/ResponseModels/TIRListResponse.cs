using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class TIRListResponse
    {
        public IEnumerable<TIRItem> TirItems { get; set; }
        public int TotalCount { get; set; }
        public int TotalRecords { get; set; }
        public int TotalDisplayRecords { get; set; }
    }
}
