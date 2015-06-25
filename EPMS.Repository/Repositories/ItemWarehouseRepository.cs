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
                DbSet.Where(x => x.WarehouseId == variationId);
        }

        public long GetItemQuantity(long itemVariationId, long WarehouseId)
        {
            var itemAvailableQty =
                DbSet.Where(x => x.ItemVariationId == itemVariationId && x.WarehouseId == WarehouseId)
                    .Sum(x => x.Quantity);
            return Convert.ToInt64(itemAvailableQty);
        }

    }
}
