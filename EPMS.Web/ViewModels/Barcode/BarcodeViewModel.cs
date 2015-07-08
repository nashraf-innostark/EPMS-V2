using System.Collections.Generic;
using EPMS.Models.ResponseModels;

namespace EPMS.Web.ViewModels.Barcode
{
    public class BarcodeViewModel
    {
        public BarcodeViewModel()
        {
            Barcodes = new List<Models.Barcode>();
            ItemVariationDropDownList = new List<ItemVariationDropDownListItem>();
        }
        public List<Models.Barcode> Barcodes { get; set; }
        public List<ItemVariationDropDownListItem> ItemVariationDropDownList { get; set; }
    }
}