using System;

namespace EPMS.Models.DomainModels
{
    public class ShelfSection
    {
        public long ShelfSectionId { get; set; }
        public string ShelfSectionName { get; set; }
        public long ShelfId { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime RecLastUpdatedDt { get; set; }

        public virtual Shelf Shelf { get; set; }
    }
}
