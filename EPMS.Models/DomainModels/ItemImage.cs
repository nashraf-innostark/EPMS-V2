﻿namespace EPMS.Models.DomainModels
{
    public class ItemImage
    {
        public long ImageId { get; set; }
        public string ItemImagePath { get; set; }
        public int? ImageOrder { get; set; }
        public bool ShowImage { get; set; }
        public long ItemVariationId { get; set; }

        public virtual ItemVariation ItemVariation { get; set; }
    }
}
