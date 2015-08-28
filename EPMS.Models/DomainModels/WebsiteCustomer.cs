using System;
using System.Collections.Generic;

namespace EPMS.Models.DomainModels
{
    public class WebsiteCustomer
    {
        public long CustomerId { get; set; }
        public string CustomerNameEn { get; set; }
        public string CustomerNameAr { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDate { get; set; }
        public string RecUpdatedBy { get; set; }
        public DateTime RecUpdatedDate { get; set; }

        public virtual ICollection<AspNetUser> AspNetUsers { get; set; }
    }
}
