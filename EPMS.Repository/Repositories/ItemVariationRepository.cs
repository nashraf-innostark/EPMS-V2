using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EPMS.Interfaces.Repository;
using EPMS.Models.ResponseModels;
using EPMS.Repository.BaseRepository;
using EPMS.Models.DomainModels;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class ItemVariationRepository : BaseRepository<ItemVariation>, IItemVariationRepository
    {
        public ItemVariationRepository(IUnityContainer container) : base(container)
        {
        }

        protected override IDbSet<ItemVariation> DbSet
        {
            get { return db.ItemVariations; }
        }

        public long GetItemVariationId(string item)
        {
            ItemVariation itemVariation = DbSet.FirstOrDefault(x => x.SKUCode.Equals(item) ||
                x.InventoryItem.ItemNameEn.Equals(item) || x.InventoryItem.ItemNameAr.Equals(item) || x.InventoryItem.ItemCode.Equals(item));
            if (itemVariation != null)
                return itemVariation.ItemVariationId;
            return 0;
        }

        public IEnumerable<ItemVariationDropDownListItem> GetItemVariationDropDownList()
        {
            return DbSet.Select(x => new ItemVariationDropDownListItem
            {
                ItemVariationId = x.ItemVariationId,
                ItemCodeSKUCodeDescriptoinEn = x.SKUDescriptionEn + " - " + x.InventoryItem.ItemCode + " - " + x.SKUCode,
                ItemCodeSKUCodeDescriptoinAr = x.SKUDescriptionAr + " - " + x.InventoryItem.ItemCode + " - " + x.SKUCode,
                ItemVariationDescriptionA = x.DescriptionAr,
                ItemVariationDescriptionE = x.DescriptionEn,
                ItemNameA = x.InventoryItem.ItemNameAr,
                ItemNameE = x.InventoryItem.ItemNameEn,
                DescriptionForQuotationEn = x.DescriptionForQuotationEn,
                DescriptionForQuotationAr = x.DescriptionForQuotationAr,
                SKUCode = x.SKUCode,
                ItemCodeSKUCode = x.InventoryItem.ItemCode + " - " + x.SKUCode,
                ItemSKUDescriptoinEn = x.SKUDescriptionEn,
                ItemSKUDescriptoinAr = x.SKUDescriptionAr,
            });
        }

        public IEnumerable<ItemVariationDropDownListItem> GetItemVariationDropDownListItems()
        {
            return DbSet.Select(x => new ItemVariationDropDownListItem
            {
                ItemVariationId = x.ItemVariationId,
                ItemCodeSKUCodeDescriptoinEn = x.SKUDescriptionEn + " - " + x.InventoryItem.ItemCode + " - " + x.SKUCode,
                ItemCodeSKUCodeDescriptoinAr = x.SKUDescriptionAr + " - " + x.InventoryItem.ItemCode + " - " + x.SKUCode,
            });
        }

        public List<ItemVariation> GetVariationsByPurchaseOrder(PurchaseOrder purchaseOrder)
        {
            var test = purchaseOrder.PurchaseOrderItems.Select(x => x.ItemVariationId);
            return
                DbSet.Where(x => test.Contains(x.ItemVariationId)).ToList();
        }

        public IEnumerable<ItemVariation> GetItemVariationByWarehouseId(long warehouseId)
        {
            var itemVariation = DbSet.Where(x => x.ItemWarehouses.Any(y => y.WarehouseId == warehouseId));
            return itemVariation;
        }

        public IEnumerable<ItemVariation> GetVariationsByInventoryItemId(long inventoryItemId)
        {
            return
                DbSet.Where(x => x.InventoryItemId == inventoryItemId);
        }

        public ItemVariation FindVariationByBarcode(string barcode)
        {
            return DbSet.FirstOrDefault(x => x.ItemBarcode == barcode);
        }

        #region Remove \r \n from CK editor values

        private static string RemoveCkEditorValues(string value)
        {
            string retval = value;
            if (!string.IsNullOrEmpty(retval))
            {
                retval = retval.Replace('\r', ' ');
                retval = retval.Replace('\n', ' ');
            }
            return retval;
        }

        #endregion
    }
}
