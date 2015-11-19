using System.Collections.Generic;

namespace EPMS.Models.DomainModels
{
    public class RfqOrder
    {
        public long RfqOrderId { get; set; }
        public long CustomerId { get; set; }
        public long? NoOfRfqs { get; set; }
        public long? NoOfOrders { get; set; }
        public string CreatedByEn { get; set; }
        public string CreatedByAr { get; set; }

        public virtual ICollection<Report> Reports { get; set; }
    }
}
