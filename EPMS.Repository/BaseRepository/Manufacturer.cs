//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EPMS.Repository.BaseRepository
{
    using System;
    using System.Collections.Generic;
    
    public partial class Manufacturer
    {
        public Manufacturer()
        {
            this.ItemManufacturers = new HashSet<ItemManufacturer>();
        }
    
        public long ManufacturerId { get; set; }
        public string ManufacturerNameEn { get; set; }
        public string ManufacturerNameAr { get; set; }
        public string RecCreatedBy { get; set; }
        public System.DateTime RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public System.DateTime RecLastUpdatedDt { get; set; }
    
        public virtual ICollection<ItemManufacturer> ItemManufacturers { get; set; }
    }
}
