using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.RequestModels
{
    public class ItemVariationRequest
    {
        public ItemVariation ItemVariation { get; set; }
        public IList<Status> Statuses { get; set; }
        public IList<Color> Colors { get; set; }
        public IList<Size> Sizes { get; set; }
        public List<ItemImage> ItemImages { get; set; }
        public List<ItemManufacturer> ItemManufacturers { get; set; } 
        public IList<ItemWarehouse> ItemWarehouses { get; set; }
        public string SizeArrayList { get; set; }
        public string ManufacturerArrayList { get; set; }
        public string StatusArrayList { get; set; }
        public string ColorArrayList { get; set; }
    }
}
