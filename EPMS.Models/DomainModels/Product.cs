﻿using System;
using System.Collections.Generic;

namespace EPMS.Models.DomainModels
{
    public class Product
    {
        public long ProductId { get; set; }
        public long? ItemVariationId { get; set; }
        public string ProductNameEn { get; set; }
        public string ProductNameAr { get; set; }
        public string ProductDescEn { get; set; }
        public string ProductDescAr { get; set; }
        public string ProductPrice { get; set; }
        public string DiscountedPrice { get; set; }
        public string ProductSize { get; set; }
        public string ProductSpecificationEn { get; set; }
        public string ProductSpecificationAr { get; set; }
        public long? ProductSectionId { get; set; }
        public bool NewArrival { get; set; }
        public bool BestSeller { get; set; }
        public bool RandomProduct { get; set; }
        public bool Featured { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime RecLastUpdatedDt { get; set; }

        public virtual ItemVariation ItemVariation { get; set; }
        public virtual ProductSection ProductSection { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public string SKUCode
        {
            get
            {
                if (ItemVariation != null)
                {
                    return ItemVariation.SKUCode;
                }
                return "";
            }
        }
        public string ItemDescriptionEn
        {
            get
            {
                if (ItemVariation != null)
                {
                    return ItemVariation.InventoryItem.ItemDescriptionEn;
                }
                return "";
            }
        }
        public string ItemDescriptionAr
        {
            get
            {
                if (ItemVariation != null)
                {
                    return ItemVariation.InventoryItem.ItemDescriptionAr;
                }
                return "";
            }
        }
    }
}