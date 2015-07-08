using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using EPMS.Interfaces.Repository;
using EPMS.Models.Common;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class PhysicalCountRepository : BaseRepository<PhysicalCount>, IPhysicalCountRepository
    {
        #region Private

        /// <summary>
        /// Order by Column Names Dictionary statements
        /// </summary>
        private readonly Dictionary<PhysicalCountByColumn, Func<PhysicalCount, object>> physicalCountClause =

            new Dictionary<PhysicalCountByColumn, Func<PhysicalCount, object>>
                    {
                        { PhysicalCountByColumn.PhysicalCountId,  c => c.PCId}
                    };
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public PhysicalCountRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<PhysicalCount> DbSet
        {
            get { return db.PhysicalCount; }
        }

        #endregion

        #region Public

        public PhysicalCountResponse GetAllPhysicalCountResponse(PhysicalCountSearchRequest searchRequest)
        {
            int fromRow = searchRequest.iDisplayStart;
            int toRow = searchRequest.iDisplayStart + searchRequest.iDisplayLength;
            if (searchRequest.iSortCol_0 == 1)
            {
                searchRequest.iSortCol_0 = 2;
            }
            Expression<Func<PhysicalCount, bool>> query;
            int pcId;
            if (int.TryParse(searchRequest.SearchString, out pcId))
            {
                query = s => (string.IsNullOrEmpty(searchRequest.SearchString) || s.PCId == pcId);
            }
            else
            {
                query = s => (string.IsNullOrEmpty(searchRequest.SearchString));
            }
            IEnumerable<PhysicalCount> physicalCounts = searchRequest.sSortDir_0 == "asc" ?
            DbSet
            .Where(query).OrderBy(physicalCountClause[searchRequest.PhysicalCountByColumn]).Skip(fromRow).Take(toRow).ToList()
                                       :
                                       DbSet
                                       .Where(query).OrderByDescending(physicalCountClause[searchRequest.PhysicalCountByColumn]).Skip(fromRow).Take(toRow).ToList();
            return new PhysicalCountResponse { PhysicalCounts = physicalCounts, TotalDisplayRecords = DbSet.Count(query), TotalRecords = DbSet.Count(query) };
        }

        #endregion
    }
}
