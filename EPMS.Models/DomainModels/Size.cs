using System;
using System.Collections.Generic;

namespace EPMS.Models.DomainModels
{
    public class Size
    {
        public long SizeId { get; set; }
        public string SizeNameEn { get; set; }
        public string SizeNameAr { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime RecLastUpdatedDt { get; set; }

        public virtual ICollection<ItemVariation> ItemVariations { get; set; }
        public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
