using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.RequestModels
{
    public class RFIServerViewModel
    {
        public RFIServerViewModel()
        {
            Rfi =new RFI();
        }
        public RFI Rfi { get; set; }
        public IEnumerable<RFIItem> RfiItem { get; set; }
    }
}
