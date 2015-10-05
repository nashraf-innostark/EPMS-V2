namespace EPMS.Models.DomainModels
{
    public class ItemBarcode
    {
        public long Id { get; set; }
        public long ItemVariationId { get; set; }
        public string BarcodeValue { get; set; }
        public string BarcodePattern { get; set; }
        public long Quantity { get; set; }
        public string RecCreatedBy { get; set; }
        public System.DateTime RecCreatedDate { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public System.DateTime RecLastUpdatedDate { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }
        public virtual ItemVariation ItemVariation { get; set; }
    }
}
