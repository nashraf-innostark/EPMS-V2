using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    class ItemWarehouseRepository : BaseRepository<ItemWarehouse>, IItemWarehouseRepository
    {
        public ItemWarehouseRepository(IUnityContainer container) : base(container)
        {
        }

        protected override IDbSet<ItemWarehouse> DbSet
        {
            get { return db.ItemWarehouses; }
        }

        public IEnumerable<ItemWarehouse> GetItemsByVariationId(long variationId)
        {
            return
                DbSet.Where(x => x.ItemVariationId == variationId);
        }

        public long GetItemQuantity(long itemVariationId, long warehousrId)
        {
            var itemAvailableQty =
                DbSet.Where(x => x.ItemVariationId == itemVariationId && x.WarehouseId == warehousrId)
                    .Sum(x => x.Quantity);
            return Convert.ToInt64(itemAvailableQty);
        }

        public ItemWarehouse FindItemWarehouseByVariationAndManufacturerId(long variationId, long warehouseId)
        {
            return
                DbSet.FirstOrDefault(x => x.ItemVariationId == variationId && x.WarehouseId == warehouseId);
        }

        public ItemWarehouse FindItemWarehouseByItemBarCodeAndWarehouseId(string itemBarcode, long warehouseId)
        {
            return DbSet.FirstOrDefault(x => x.WarehouseId == warehouseId && x.ItemVariation.ItemBarcode == itemBarcode);
        }

        public IEnumerable<ItemWarehouse> GetItemWarehousesByVariationId(long variationId)
        {
            return DbSet.Where(x => x.ItemVariationId == variationId);
        }
    }
}
