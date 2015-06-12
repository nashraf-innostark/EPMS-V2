using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class IrfHistoryResponse
    {
        public string RequesterNameEn { get; set; }
        public string RequesterNameAr { get; set; }
        public string ManagerNameEn { get; set; }
        public string ManagerNameAr { get; set; }
        public ItemRelease RecentIrf { get; set; }
        public IEnumerable<ItemRelease> Irfs { get; set; }
        public IEnumerable<ItemReleaseDetail> IrfItems { get; set; }
    }
}
