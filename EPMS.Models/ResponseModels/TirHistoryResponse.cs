using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class TirHistoryResponse
    {
        public string RequesterNameEn { get; set; }
        public string RequesterNameAr { get; set; }
        public string ManagerNameEn { get; set; }
        public string ManagerNameAr { get; set; }
        public TIR RecentTir { get; set; }
        public IEnumerable<TIR> Tirs { get; set; }
        public IEnumerable<TIRItem> TirItems { get; set; }
    }
}
