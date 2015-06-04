namespace EPMS.Models.DomainModels
{
    public class DIFItem
    {
        public long ItemId { get; set; }
        public long DIFId { get; set; }
        public long? ItemVariationId { get; set; }
        public string ItemDetails { get; set; }
        public long ItemQty { get; set; }
        public bool IsItemDescription { get; set; }
        public bool IsItemSKU { get; set; }
        public string RecCreatedBy { get; set; }
        public System.DateTime RecCreatedDate { get; set; }
        public string RecUpdatedBy { get; set; }
        public System.DateTime RecUpdatedDate { get; set; }

        public virtual DIF DIF { get; set; }
        public virtual ItemVariation ItemVariation { get; set; }
    }
}
