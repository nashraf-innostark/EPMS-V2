using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPMS.Web.ViewModels.Quotation
{
    public class QuotationDetailViewModel
    {
        public Models.CompanyProfile Profile { get; set; }
        public Models.Customer Customer { get; set; }
        public Models.Quotation Quotation { get; set; }
    }
}