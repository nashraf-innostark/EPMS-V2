using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    class VendorItemsRepository : BaseRepository<VendorItem>, IVendorItemsRepository
    {
        public VendorItemsRepository(IUnityContainer container) : base(container)
        {
        }

        protected override IDbSet<VendorItem> DbSet
        {
            get { return db.VendorItems; }
        }

        public IEnumerable<VendorItem> GetItemsByVendorId(long vendorId)
        {
            return
                DbSet.Where(x => x.VendorId == vendorId);
        }

        public IEnumerable<VendorItem> GetItemsByItemId(long itemId)
        {
            return
                DbSet.Where(x => x.ItemVariationId == itemId);
        }
    }
}
