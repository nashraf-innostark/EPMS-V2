namespace EPMS.Models.DomainModels
{
    public class RFIItem
    {
        public long RFIItemId { get; set; }
        public long RFIId { get; set; }
        public long ItemVariationId { get; set; }
        public string RecCreatedBy { get; set; }
        public System.DateTime RecCreatedDate { get; set; }
        public string RecUpdatedBy { get; set; }
        public System.DateTime RecUpdatedDate { get; set; }

        public virtual ItemVariation ItemVariation { get; set; }
        public virtual RFI RFI { get; set; }
    }
}
