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
            var variattions = DbSet;
            IList<ItemVariationDropDownListItem> variationDropDownList = new List<ItemVariationDropDownListItem>();
            foreach (var variation in variattions)
            {
                ItemVariationDropDownListItem item = new ItemVariationDropDownListItem
                {
                    ItemVariationId = variation.ItemVariationId,
                    ItemCodeSKUCode = variation.InventoryItem.ItemCode + " - " + variation.SKUCode,
                    ItemCodeSKUCodeDescriptoinEn = RemoveCkEditorValues(variation.SKUDescriptionEn) + " - " + variation.InventoryItem.ItemCode + " - " + variation.SKUCode,
                    //ItemCodeSKUCodeDescriptoinAr = RemoveCkEditorValues(variation.SKUDescriptionAr) + " - " + variation.InventoryItem.ItemCode + " - " + variation.SKUCode,
                    SKUCode = variation.SKUCode,
                    ItemSKUDescriptoinEn = RemoveCkEditorValues(variation.SKUDescriptionEn),
                    //ItemSKUDescriptoinAr = RemoveCkEditorValues(variation.SKUDescriptionAr),
                    ItemVariationDescriptionE = RemoveCkEditorValues(variation.DescriptionEn),
                    //ItemVariationDescriptionA = RemoveCkEditorValues(variation.DescriptionAr),
                    ItemNameE = variation.InventoryItem.ItemNameEn,
                    //ItemNameA = variation.InventoryItem.ItemNameAr,
                    DescriptionForQuotationEn = RemoveCkEditorValues(variation.DescriptionForQuotationEn),
                    //DescriptionForQuotationAr = RemoveCkEditorValues(variation.DescriptionForQuotationAr)
                };
                variationDropDownList.Add(item);
            }
            return variationDropDownList;
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
