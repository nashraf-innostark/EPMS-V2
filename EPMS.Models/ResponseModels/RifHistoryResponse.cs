using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class RifHistoryResponse
    {
        public string RequesterNameEn { get; set; }
        public string RequesterNameAr { get; set; }
        public string ManagerNameEn { get; set; }
        public string ManagerNameAr { get; set; }
        public RIF RecentRif { get; set; }
        public IEnumerable<RIF> Rifs { get; set; }
        public IEnumerable<RIFItem> RifItems { get; set; }
    }
}
