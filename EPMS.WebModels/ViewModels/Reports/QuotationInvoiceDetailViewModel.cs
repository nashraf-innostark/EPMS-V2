using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPMS.WebModels.WebsiteModels;

namespace EPMS.WebModels.ViewModels.Reports
{
    public class QuotationInvoiceDetailViewModel
    {
        public long ReportId { get; set; }
        public string ImageSrc { get; set; }
        public IList<WebsiteModels.Quotation> Quotations { get; set; }

        //Grpah related Data
        public long GrpahStartTimeStamp { get; set; }
        public long GrpahEndTimeStamp { get; set; }
        public List<GraphItem> GraphItems { get; set; } 
    }
}
