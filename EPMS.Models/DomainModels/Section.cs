using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPMS.Models.DomainModels
{
    public class Section
    {
        public long SectionId { get; set; }
        public string SectionName { get; set; }
        public long AisleId { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime RecLastUpdatedDt { get; set; }

        public virtual Aisle Aisle { get; set; }
        public virtual Shelf Shelf { get; set; }
    }
}
