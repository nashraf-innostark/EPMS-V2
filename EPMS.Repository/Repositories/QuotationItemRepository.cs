using System.Data.Entity;
using EPMS.Repository.BaseRepository;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class QuotationItemRepository : BaseRepository<QuotationItemDetail>, IQuotationItemRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public QuotationItemRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<QuotationItemDetail> DbSet
        {
            get { return db.QuotationItemDetails; }
        }
        #endregion
    }
}
