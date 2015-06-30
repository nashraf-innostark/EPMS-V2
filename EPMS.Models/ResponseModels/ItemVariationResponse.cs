using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class ItemVariationResponse
    {
        public ItemVariation ItemVariation { get; set; }
        public IEnumerable<Color> ColorsForDdl { get; set; }
        public IEnumerable<Size> SizesForDdl { get; set; }
        public IEnumerable<Manufacturer> ManufacturersForDdl { get; set; }
        public IEnumerable<Status> StatusesForDdl { get; set; }
        public IEnumerable<Warehouse> WarehousesForDdl { get; set; }
        public InventoryItem InventoryItem { get; set; }
    }
}
