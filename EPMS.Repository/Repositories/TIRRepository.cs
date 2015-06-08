using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPMS.Interfaces.Repository;
using EPMS.Models.Common;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class TIRRepository : BaseRepository<TIR>,ITIRRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public TIRRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<TIR> DbSet
        {
            get { return db.TIR; }
        }

        #endregion

        #region Private

        /// <summary>
        /// Order by Column Names Dictionary statements
        /// </summary>
        private readonly Dictionary<TirRequestByColumn, Func<TIR, object>> tirClause =

            new Dictionary<TirRequestByColumn, Func<TIR, object>>
                    {
                        { TirRequestByColumn.TirNo,  c => c.Id},
                        //{ TirRequestByColumn.Requester, c => c.RecCreatedBy},
                        { TirRequestByColumn.DateCreated, c => c.RecCreatedDate},
                        { TirRequestByColumn.Status, c => c.Status},
                    };
        #endregion

        public TIRListResponse GetAllTirs(ItemReleaseSearchRequest searchRequest)
        {
            return new TIRListResponse();
        }
    }
}
