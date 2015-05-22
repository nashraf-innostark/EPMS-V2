using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.RequestModels
{
    class ItemVariationRequest
    {
        public ItemVariation ItemVariation { get; set; }
        public IList<Status> Statuses { get; set; }
        public IList<Color> Colors { get; set; }
        public IList<Size> Sizes { get; set; }
        public IList<ItemImage> ItemImages { get; set; }
        public IList<Warehouse> Warehouses { get; set; }
    }
}
