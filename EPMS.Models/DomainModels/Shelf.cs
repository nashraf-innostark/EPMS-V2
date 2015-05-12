using System;
using System.Collections.Generic;

namespace EPMS.Models.DomainModels
{
    public class Shelf
    {
        public long ShelfId { get; set; }
        public string ShelfName { get; set; }
        public long SectionId { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime RecLastUpdatedDt { get; set; }

        public virtual Section Section { get; set; }
        public virtual ICollection<ShelfSection> ShelfSections { get; set; }
    }
}
