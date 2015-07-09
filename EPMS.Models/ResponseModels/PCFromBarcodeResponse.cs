using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class PCFromBarcodeResponse
    {
        public string ItemBarcode { get; set; }
        public string ItemNameEn { get; set; }
        public string ItemNameAr { get; set; }
        public string SKUDescriptionEn { get; set; }
        public string SKUDescriptionAr { get; set; }
        public long ItemsInPackage { get; set; }
        public ItemVariation ItemVariation { get; set; }
    }
}
