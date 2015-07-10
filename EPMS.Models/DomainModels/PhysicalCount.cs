using System;
using System.Collections.Generic;

namespace EPMS.Models.DomainModels
{
    public class PhysicalCount
    {
        public long PCId { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDate { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime RecLastUpdatedDate { get; set; }
        public string NotesEn { get; set; }
        public string NotesAr { get; set; }

        public virtual ICollection<PhysicalCountItem> PhysicalCountItems { get; set; }
    }
}
