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
                    var itemVarQty = (ItemVariations.Sum(x => Convert.ToDouble(x.QuantityInHand)));
                    var manufacturerQty = ItemVariations.Sum(x => x.ItemManufacturers.Sum(y => y.Quantity));
                    var poItemQty = ItemVariations.Sum(
                        x =>
                            x.PurchaseOrderItems.Where(y => y.PurchaseOrder.Status == 1)
                                .Sum(y => Convert.ToDouble(y.ItemQty)));
                    var rifQty = ItemVariations.Sum(x => x.RIFItems.Where(y=>y.RIF.Status == 2).Sum(y => y.ItemQty));
                    var itemReleaseQty =
                        (ItemVariations.Sum(x => x.ItemReleaseQuantities.Where(z=>z.ItemReleaseDetail.ItemRelease.Status ==1).Sum(y => Convert.ToDouble(y.Quantity))));
                    var difQty = ItemVariations.Sum(x => x.DIFItems.Where(y=>y.DIF.Status == 2).Sum(y => y.ItemQty));

                    return
                        (double) ( itemVarQty + manufacturerQty + poItemQty + rifQty - itemReleaseQty - difQty);
                }
                return 0;
            }
        }

        public virtual InventoryDepartment InventoryDepartment { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
    }
}