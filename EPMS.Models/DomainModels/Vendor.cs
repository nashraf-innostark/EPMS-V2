using System;
using System.Collections;
using System.Collections.Generic;

namespace EPMS.Models.DomainModels
{
    public class Vendor
    {
        public long VendorId { get; set; }
        public string VendorNameEn { get; set; }
        public string VendorNameAr { get; set; }
        public string ContactPerson { get; set; }
        public string VendorEmail { get; set; }
        public string VendorContact { get; set; }
        public string VendorField { get; set; }
        public string DetailsEn { get; set; }
        public string DetailsAr { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime RecLastUpdatedDt { get; set; }

        public virtual ICollection<VendorItem> VendorItems { get; set; }
        public virtual ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; }
        public virtual ICollection<PurchaseOrderItemHistory> PurchaseOrderItemHistories { get; set; }
        public virtual ICollection<ItemManufacturer> ItemManufacturers { get; set; }

    }
}
