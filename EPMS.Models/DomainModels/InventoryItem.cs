using System;
using System.Collections.Generic;
using System.Linq;

namespace EPMS.Models.DomainModels
{
    public class InventoryItem
    {
        public long ItemId { get; set; }
        public string ItemCode { get; set; }
        public string ItemNameEn { get; set; }
        public string ItemNameAr { get; set; }
        public string ItemImagePath { get; set; }
        public string ItemDescriptionEn { get; set; }
        public string ItemDescriptionAr { get; set; }
        public string DescriptionForQuotationEn { get; set; }
        public string DescriptionForQuotationAr { get; set; }
        public string HazardousEn { get; set; }
        public string HazardousAr { get; set; }
        public string UsageEn { get; set; }
        public string UsageAr { get; set; }
        public int? ReorderLevel { get; set; }
        public long? DepartmentId { get; set; }
        public long? WarehouseID { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime RecLastUpdatedDt { get; set; }
        public string DepartmentPath { get; set; }
        public double? QuantityInPackage { get; set; }

        public virtual ICollection<ItemVariation> ItemVariations { get; set; }

        public double QuantityInHand
        {
            get
            {
                if (ItemVariations != null)
                {
                    return (double) (ItemVariations.Sum(x => x.ItemManufacturers.Sum(y=>y.Quantity)) +
                                     ItemVariations.Sum(x => x.PurchaseOrderItems.Sum(y => Convert.ToDouble(y.ItemQty)))
                                     + ItemVariations.Sum(x => x.RIFItems.Sum(y => y.ItemQty)) -
                                     (ItemVariations.Sum(x => x.ItemReleaseQuantities.Sum(y => Convert.ToDouble(y.Quantity)))
                                      + ItemVariations.Sum(x=>x.DIFItems.Sum(y=>y.ItemQty))));
                }
                return 0;
            }
        }
        public virtual InventoryDepartment InventoryDepartment { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
    }
}
