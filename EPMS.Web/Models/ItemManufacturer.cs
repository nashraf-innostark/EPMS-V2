using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPMS.Web.Models
{
    public class ItemManufacturer
    {
        public long ItemVariationId { get; set; }
        public long ManufacturerId { get; set; }
        public string Price { get; set; }
        public string ManufacturerNameEn { get; set; }
        public string ManufacturerNameAr { get; set; }
        public long? Quantity { get; set; }
    }
}