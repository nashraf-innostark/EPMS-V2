namespace EPMS.Models.ResponseModels
{
    public class ItemVariationDetail
    {
        public long ItemVariationId { get; set; }
        public string SKUCode { get; set; }
        public string ItemNameE { get; set; }
        public string ItemNameA { get; set; }
        public string ItemVariationDescriptionE { get; set; }
        public string ItemVariationDescriptionA { get; set; }
        public string DescriptionForQuotationEn { get; set; }
        public string DescriptionForQuotationAr { get; set; }
        public string ItemSKUDescriptoinEn { get; set; }
        public string ItemSKUDescriptoinAr { get; set; }
        public double? UnitPrice { get; set; }
        public string ItemCodeSKUCode { get; set; }
    }
}
