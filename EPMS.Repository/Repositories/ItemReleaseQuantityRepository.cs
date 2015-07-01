using System;
using System.Data.Entity;
using System.Linq;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class ItemReleaseQuantityRepository : BaseRepository<ItemReleaseQuantity>, IItemReleaseQuantityRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ItemReleaseQuantityRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<ItemReleaseQuantity> DbSet
        {
            get { return db.ItemReleaseQuantities; }
        }

        #endregion

        public long GetItemReleasedQuantity(long itemVariationId, long warehousrId)
        {
            var itemAvailableQty =
               DbSet.Where(x => x.ItemVariationId == itemVariationId && x.WarehouseId == warehousrId)
                   .Sum(x => x.Quantity);
            return Convert.ToInt64(itemAvailableQty);
        }

        public long GetItemReleasedQuantity(long itemVariationId, long warehousrId, long irfDetailId)
        {
            var itemAvailableQty = 
                DbSet.Where(x => x.ItemVariationId == itemVariationId && x.WarehouseId == warehousrId && x.IRFDetailId != irfDetailId).Sum(x => x.Quantity);
            return Convert.ToInt64(itemAvailableQty);
        }
    }
}
