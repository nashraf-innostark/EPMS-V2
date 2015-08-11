using System.Collections.Generic;
using EPMS.Models.ResponseModels;

namespace EPMS.WebModels.ViewModels.Barcode
{
    public class BarcodeViewModel
    {
        public BarcodeViewModel()
        {
            Barcodes = new List<WebsiteModels.Barcode>();
            ItemVariationDropDownList = new List<ItemVariationDropDownListItem>();
        }
        public List<WebsiteModels.Barcode> Barcodes { get; set; }
        public List<ItemVariationDropDownListItem> ItemVariationDropDownList { get; set; }
    }
}