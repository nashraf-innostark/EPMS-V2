using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EPMS.Models.DomainModels;

namespace EPMS.WebModels.WebsiteModels
{
    public class Vendor
    {
        public Vendor()
        {
            VendorItems = new List<VendorItem>();
        }
        public long VendorId { get; set; }
        [Required(ErrorMessage = "Vendor Name is required")]
        [StringLength(400, ErrorMessageResourceType = typeof (Resources.Inventory.Vendor), ErrorMessageResourceName = "VendorStringLengthValidation")]
        public string VendorNameEn { get; set; }
        [Required(ErrorMessage = "Vendor Name is required")]
        [StringLength(400, ErrorMessageResourceType = typeof(Resources.Inventory.Vendor), ErrorMessageResourceName = "VendorStringLengthValidation")]
        public string VendorNameAr { get; set; }
        public string ContactPerson { get; set; }
        public string VendorEmail { get; set; }
        public string VendorContact { get; set; }
        public string VendorField { get; set; }
        public string DetailsEn { get; set; }
        public string DetailsAr { get; set; }
        public string RecCreatedBy { get; set; }
        public string RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime RecLastUpdatedDt { get; set; }
        public List<VendorItem> VendorItems { get; set; }
        public List<PurchaseOrderItem> PurchaseOrderItems { get; set; }
    }
}