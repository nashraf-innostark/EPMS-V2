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
    public class RFQItemRepository : BaseRepository<RFQItem>, IRFQItemRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public RFQItemRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<RFQItem> DbSet
        {
            get { return db.RfqItems; }
        }

        #endregion
    }
}
