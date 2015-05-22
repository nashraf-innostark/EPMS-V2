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
        }
        public Models.ItemVariation ItemVariation { get; set; }
        public IEnumerable<Models.ItemVariation> ItemVariations { get; set; }

        public Color Color { get; set; }
        public Size Size { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public Status Statuse { get; set; }
        public Warehouse Warehouse { get; set; }
        public ItemImage ItemImage { get; set; }
    }
}