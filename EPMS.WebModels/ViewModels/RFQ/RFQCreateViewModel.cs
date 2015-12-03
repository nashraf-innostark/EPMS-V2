using System.Collections.Generic;
using EPMS.WebModels.WebsiteModels;

namespace EPMS.WebModels.ViewModels.RFQ
{
    public class RFQCreateViewModel
    {
        public RFQCreateViewModel()
        {
            Rfq = new WebsiteModels.RFQ{ RFQItems = new List<RFQItem>()};
        }
        public WebsiteModels.RFQ Rfq { get; set; }
        public bool FromClient { get; set; }
    }
}