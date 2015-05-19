using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPMS.Models.DomainModels;

namespace EPMS.Models.RequestModels
{
    public class VendorRequest
    {
        public Vendor Vendor { get; set; }
        public List<VendorItem> VendorItems { get; set; }

    }
}
