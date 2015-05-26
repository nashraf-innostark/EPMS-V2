using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPMS.Models.DomainModels;
using Color = EPMS.Web.Models.Color;
using ItemImage = EPMS.Web.Models.ItemImage;
using Manufacturer = EPMS.Web.Models.Manufacturer;
using Size = EPMS.Web.Models.Size;
using Status = EPMS.Web.Models.Status;

namespace EPMS.Web.ViewModels.ItemVariation
{
    public class ItemVariationViewModel
    {
        public ItemVariationViewModel()
        {
            ItemVariation = new Models.ItemVariation
            {
                Colors = new List<Color>(),
                Sizes = new List<Size>(),
                Manufacturers = new List<Manufacturer>(),
                Statuses = new List<Status>(),
                ItemImages = new List<ItemImage>(),
                Warehouses = new List<Warehouse>()
            };
            ColorsForDdl = new List<Color>();
            SizesForDdl = new List<Size>();
            ManufacturersForDdl = new List<Manufacturer>();
            StatusesForDdl = new List<Status>();
            WarehousesForDdl = new List<Warehouse>();
        }
        public Models.ItemVariation ItemVariation { get; set; }
        public IEnumerable<Models.ItemVariation> ItemVariations { get; set; }

        public Color Color { get; set; }
        public Size Size { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public Status Status { get; set; }
        public Warehouse Warehouse { get; set; }
        public ItemImage ItemImage { get; set; }

        public List<Color> ColorsForDdl { get; set; }
        public List<Size> SizesForDdl { get; set; }
        public List<Manufacturer> ManufacturersForDdl { get; set; }
        public List<Status> StatusesForDdl { get; set; }
        public List<Warehouse> WarehousesForDdl { get; set; }
    }
}