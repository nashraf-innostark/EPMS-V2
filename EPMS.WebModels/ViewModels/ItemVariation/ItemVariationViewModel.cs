using System.Collections.Generic;

namespace EPMS.WebModels.ViewModels.ItemVariation
{
    public class ItemVariationViewModel
    {
        public ItemVariationViewModel()
        {
            ItemVariation = new WebsiteModels.ItemVariation
            {
                Colors = new List<WebsiteModels.Color>(),
                Sizes = new List<WebsiteModels.Size>(),
                ItemManufacturers = new List<WebsiteModels.ItemManufacturer>(),
                Statuses = new List<WebsiteModels.Status>(),
                ItemImages = new List<WebsiteModels.ItemImage>(),
                ItemWarehouses = new List<WebsiteModels.ItemWarehouse>()
            };
            ColorsForDdl = new List<WebsiteModels.Color>();
            SizesForDdl = new List<WebsiteModels.Size>();
            ManufacturersForDdl = new List<WebsiteModels.Manufacturer>();
            StatusesForDdl = new List<WebsiteModels.Status>();
            WarehousesForDdl = new List<WebsiteModels.Warehouse>();
        }
        public WebsiteModels.ItemVariation ItemVariation { get; set; }
        public IEnumerable<WebsiteModels.ItemVariation> ItemVariations { get; set; }

        public WebsiteModels.Color Color { get; set; }
        public WebsiteModels.Size Size { get; set; }
        public WebsiteModels.Manufacturer Manufacturer { get; set; }
        public WebsiteModels.Status Status { get; set; }
        public WebsiteModels.Warehouse Warehouse { get; set; }
        public WebsiteModels.ItemImage ItemImage { get; set; }

        public List<WebsiteModels.Color> ColorsForDdl { get; set; }
        public List<WebsiteModels.Size> SizesForDdl { get; set; }
        public List<WebsiteModels.Manufacturer> ManufacturersForDdl { get; set; }
        public List<WebsiteModels.Status> StatusesForDdl { get; set; }
        public List<WebsiteModels.Warehouse> WarehousesForDdl { get; set; }

        public string SizeArrayList { get; set; }
        public string ManufacturerArrayList { get; set; }
        public string StatusArrayList { get; set; }
        public string ColorArrayList { get; set; }

    }
}