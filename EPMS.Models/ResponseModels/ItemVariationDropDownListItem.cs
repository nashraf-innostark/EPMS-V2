﻿namespace EPMS.Models.ResponseModels
{
    public class ItemVariationDropDownListItem
    {
        public long ItemVariationId { get; set; }
        public string ItemCodeSKUCode { get; set; }
        public string ItemCodeSKUCodeDescriptoinEn { get; set; }
        public string ItemCodeSKUCodeDescriptoinAr { get; set; }
        public string SKUCode { get; set; }
        public string ItemSKUDescriptoinEn { get; set; }
        public string ItemSKUDescriptoinAr { get; set; }
        public string ItemVariationDescriptionE { get; set; }
        public string ItemVariationDescriptionA { get; set; }
        public string ItemNameE { get; set; }
        public string ItemNameA { get; set; }
        public string DescriptionForQuotationEn { get; set; }
        public string DescriptionForQuotationAr { get; set; }
    }
}
