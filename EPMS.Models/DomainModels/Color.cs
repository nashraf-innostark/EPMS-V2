using System;
using System.Collections.Generic;

namespace EPMS.Models.DomainModels
{
    public class Color
    {
        public long ColorId { get; set; }
        public string ColorNameEn { get; set; }
        public string ColorNameAr { get; set; }
        public string ColorCode { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime RecLastUpdatedDt { get; set; }

        public virtual ICollection<ItemVariation> ItemVariations { get; set; }
    }
}
