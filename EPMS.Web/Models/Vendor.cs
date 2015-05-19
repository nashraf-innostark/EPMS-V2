using System;
using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Web.Models
{
    public class Vendor
    {
        public Vendor()
        {
            VendorItems = new List<VendorItem>();
        }
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
        public List<VendorItem> VendorItems { get; set; }
    }
}