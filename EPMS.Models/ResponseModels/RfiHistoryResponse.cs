using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class RfiHistoryResponse
    {
        public string RequesterNameEn { get; set; }
        public string RequesterNameAr { get; set; }
        public string ManagerNameEn { get; set; }
        public string ManagerNameAr { get; set; }
        public RFI RecentRfi { get; set; }
        public IEnumerable<RFI> Rfis { get; set; }
        public IEnumerable<RFIItem> RfiItems { get; set; }
    }
}
