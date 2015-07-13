using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPMS.Models.DomainModels
{
    public class ItemManufacturer
    {
        public long ItemVariationId { get; set; }
        public long ManufacturerId { get; set; }
        public string Price { get; set; }
        public long? Quantity { get; set; }
        public virtual ItemVariation ItemVariation { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
    }
}
