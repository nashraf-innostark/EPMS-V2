using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace EPMS.Web.Models
{
    public class Order
    {
        public long OrderId { get; set; }
        [DisplayName("Order Number")]
        public string OrderNo { get; set; }
        public string OrderDescription { get; set; }
        public string OrderNotes { get; set; }
        public DateTime? OrderDate { get; set; }
        public string Attachment { get; set; }
    }
}