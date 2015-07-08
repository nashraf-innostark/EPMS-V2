﻿using System.Collections.Generic;

namespace EPMS.Models.DomainModels
{
    public class PhysicalCount
    {
        public long PCId { get; set; }
        public string RecCreatedBy { get; set; }
        public System.DateTime RecCreatedDate { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public System.DateTime RecLastUpdatedDate { get; set; }

        public virtual ICollection<PhysicalCountItem> PhysicalCountItems { get; set; }
    }
}