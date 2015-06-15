using System.Data.Entity;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public sealed class ItemBarcodeRepository : BaseRepository<ItemBarcode>, IItemBarcodeRepository
    {
        #region Constructor
        public ItemBarcodeRepository(IUnityContainer container)
            : base(container)
        {
        }

        protected override IDbSet<ItemBarcode> DbSet
        {
            get { return db.ItemBarcode; }
        }
        #endregion
    }
}
