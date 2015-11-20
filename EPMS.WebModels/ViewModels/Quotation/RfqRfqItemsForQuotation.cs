using System.Collections.Generic;
using EPMS.WebModels.WebsiteModels;

namespace EPMS.WebModels.ViewModels.Quotation
{
    public class RfqRfqItemsForQuotation
    {
        public RfqRfqItemsForQuotation()
        {
            Rfqs = new List<RfqDropDown>();
            RfqItems = new List<RFQItem>();
        }
        public IList<RfqDropDown> Rfqs { get; set; }
        public IList<RFQItem> RfqItems { get; set; }
    }
}
