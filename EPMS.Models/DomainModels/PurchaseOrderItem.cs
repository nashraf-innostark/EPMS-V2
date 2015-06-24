﻿using System;

namespace EPMS.Models.DomainModels
{
    public class PurchaseOrderItem
    {
        public long ItemId { get; set; }
        public long PurchaseOrderId { get; set; }
        public long? ItemVariationId { get; set; }
        public string ItemDetails { get; set; }
        public long ItemQty { get; set; }
        public bool IsItemDescription { get; set; }
        public bool IsItemSKU { get; set; }
        public string PlaceInDepartment { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDate { get; set; }
        public string RecUpdatedBy { get; set; }
        public DateTime RecUpdatedDate { get; set; }

        public virtual ItemVariation ItemVariation { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }
    }
}