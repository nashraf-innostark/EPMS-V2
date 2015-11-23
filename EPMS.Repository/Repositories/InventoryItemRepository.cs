using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EPMS.Models.RequestModels.Reports;
using EPMS.Repository.BaseRepository;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class InventoryItemRepository : BaseRepository<InventoryItem>, IInventoryItemRepository
    {
        public InventoryItemRepository(IUnityContainer container)
            : base(container)
        {
        }

        protected override IDbSet<InventoryItem> DbSet
        {
            get { return db.InventoryItems; }
        }

        public bool ItemExists(InventoryItem item)
        {
            if (item.ItemId > 0) //Already in the System
            {
                return DbSet.Any(
                    it => item.ItemId != it.ItemId &&
                        (it.ItemNameEn == item.ItemNameEn || it.ItemNameAr == item.ItemNameAr));
            }
            return DbSet.Any(
                it =>
                it.ItemNameEn == item.ItemNameEn || it.ItemNameAr == item.ItemNameAr);
        }

        public IEnumerable<InventoryItem> GetInventoryItemReportDetails(InventoryItemReportCreateOrDetailsRequest request)
        {
            return DbSet.Include(x=>x.ItemVariations).Where(x => (request.ItemId == 0 || x.ItemId == request.ItemId));
        }
    }
}
