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
    public class DIFRepository : BaseRepository<DIF>, IDIFRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public DIFRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<DIF> DbSet
        {
            get { return db.DIF; }
        }

        #endregion
        #region Private

        /// <summary>
        /// Order by Column Names Dictionary statements
        /// </summary>
        private readonly Dictionary<DifRequestByColumn, Func<DIF, object>> requestClause =

            new Dictionary<DifRequestByColumn, Func<DIF, object>>
                    {
                        { DifRequestByColumn.DifNo, c => c.Id},
                        //{ DifRequestByColumn.Requester,  c => c. },
                        { DifRequestByColumn.DateCreated, c => c.RecCreatedDate},
                        { DifRequestByColumn.Status,  c => c.Status}
                    };
        #endregion
        public DifRequestResponse LoadAllDifs(DifSearchRequest searchRequest)
        {
            int fromRow = searchRequest.iDisplayStart;
            int toRow = searchRequest.iDisplayStart + searchRequest.iDisplayLength;
            Expression<Func<DIF, bool>> query;
            IEnumerable<DIF> queryData;
            if (searchRequest.Requester == "Admin")
            {
                query =
                s => ((string.IsNullOrEmpty(searchRequest.SearchString)) ||
                    //(s.RFIId.Equals(Convert.ToInt64(rfiSearchRequest.SearchString))) ||
                    //(s.RecCreatedDate.ToShortDateString().Contains(rfiSearchRequest.SearchString))||
                    (s.AspNetUser.Employee.EmployeeFirstNameE.Contains(searchRequest.SearchString)) ||
                    (s.AspNetUser.Employee.EmployeeLastNameE.Contains(searchRequest.SearchString)) ||
                    (s.AspNetUser.Employee.EmployeeFirstNameA.Contains(searchRequest.SearchString)) ||
                    (s.AspNetUser.Employee.EmployeeMiddleNameA.Contains(searchRequest.SearchString))
                    
                    );
            }
            else
            {
                var employeeId = searchRequest.Requester;
                query =
                s => (((string.IsNullOrEmpty(searchRequest.SearchString))
                    //(s.RFIId.Equals(Convert.ToInt64(rfiSearchRequest.SearchString))) ||
                    //(s.RecCreatedDate.ToShortDateString().Contains(rfiSearchRequest.SearchString))||
                    ) && (s.RecCreatedBy.Equals(employeeId)));
            }

            if (searchRequest.iSortCol_0 == 0)
            {
                queryData = DbSet
                .Where(query).OrderByDescending(x => x.RecCreatedDate).Skip(fromRow).Take(toRow).ToList();
            }
            else if (searchRequest.iSortCol_0 == 3 && searchRequest.Requester == "Admin")
            {
                queryData = searchRequest.sSortDir_0 == "asc" ? 
                    DbSet.Where(query).OrderBy(x => x.AspNetUser.Employee.EmployeeFirstNameE).Skip(fromRow).Take(toRow).ToList():
                    DbSet.Where(query).OrderByDescending(x => x.AspNetUser.Employee.EmployeeFirstNameE).Skip(fromRow).Take(toRow).ToList();
            }
            else
            {
                queryData = searchRequest.sSortDir_0 == "asc" ?
                DbSet
                .Where(query).OrderBy(requestClause[searchRequest.RequestByColumn]).Skip(fromRow).Take(toRow).ToList()
                :
                DbSet
                .Where(query).OrderByDescending(requestClause[searchRequest.RequestByColumn]).Skip(fromRow).Take(toRow).ToList();
            }
            return new DifRequestResponse { Difs = queryData, TotalCount = DbSet.Count(query) };
        }
    }
}
