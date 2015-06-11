using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class DifHistoryResponse
    {
        public string RequesterNameEn { get; set; }
        public string RequesterNameAr { get; set; }
        public string ManagerNameEn { get; set; }
        public string ManagerNameAr { get; set; }
        public DIF RecentDif { get; set; }
        public IEnumerable<DIF> Difs { get; set; }
        public IEnumerable<DIFItem> DifItems { get; set; }
    }
}
