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
    public class RIFRepository : BaseRepository<RIF>, IRIFRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public RIFRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<RIF> DbSet
        {
            get { return db.RIF; }
        }

        #endregion
        #region Private

        /// <summary>
        /// Order by Column Names Dictionary statements
        /// </summary>
        private readonly Dictionary<RifRequestByColumn, Func<RIF, object>> requestClause =

            new Dictionary<RifRequestByColumn, Func<RIF, object>>
                    {
                        { RifRequestByColumn.RifNo, c => c.RIFId},
                        { RifRequestByColumn.Requester,  c => c.Order.Customer.CustomerNameE },
                        { RifRequestByColumn.DateCreated, c => c.RecCreatedDate},
                        { RifRequestByColumn.Status,  c => c.Status}
                    };
        #endregion
        public RifRequestResponse LoadAllRifs(RifSearchRequest searchRequest)
        {
            int fromRow = searchRequest.iDisplayStart;
            int toRow = searchRequest.iDisplayLength;
            Expression<Func<RIF, bool>> query;
            IEnumerable<RIF> queryData;
            if (searchRequest.Requester == "Admin")
            {
                query =
                s => ((string.IsNullOrEmpty(searchRequest.SearchString)) ||
                    //(s.RFIId.Equals(Convert.ToInt64(rfiSearchRequest.SearchString))) ||
                    //(s.RecCreatedDate.ToShortDateString().Contains(rfiSearchRequest.SearchString))||
                    (s.AspNetUser.Employee.EmployeeFirstNameE.Contains(searchRequest.SearchString)) ||
                    (s.AspNetUser.Employee.EmployeeMiddleNameE.Contains(searchRequest.SearchString)) ||
                    (s.AspNetUser.Employee.EmployeeLastNameE.Contains(searchRequest.SearchString)) ||
                    (s.AspNetUser.Employee.EmployeeFirstNameA.Contains(searchRequest.SearchString)) ||
                    (s.AspNetUser.Employee.EmployeeMiddleNameA.Contains(searchRequest.SearchString)) ||
                    (s.AspNetUser.Employee.EmployeeLastNameA.Contains(searchRequest.SearchString))
                    
                    );
            }
            else
            {
                var employeeId = searchRequest.Requester;
                query =
                s => (((string.IsNullOrEmpty(searchRequest.SearchString)) ||
                    //(s.RFIId.Equals(Convert.ToInt64(rfiSearchRequest.SearchString))) ||
                    //(s.RecCreatedDate.ToShortDateString().Contains(rfiSearchRequest.SearchString))||
                    (s.Order.Customer.CustomerNameE.Contains(searchRequest.SearchString)) ||
                    (s.Order.Customer.CustomerNameA.Contains(searchRequest.SearchString)) 
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
            return new RifRequestResponse { Rifs = queryData, TotalCount = DbSet.Count(query) };
        }

        public IEnumerable<RIF> GetRecentRIFs(int status, string requester, DateTime date)
        {
            DateTime newDataTime = new DateTime();
            if (requester == "Admin")
            {
                requester = "";
                return status > 0 ? DbSet.Where(x => x.Status == status && (string.IsNullOrEmpty(requester) || x.RecCreatedBy == requester) && (date == newDataTime || DbFunctions.TruncateTime(x.RecCreatedDate) == date.Date)).OrderByDescending(x => x.RecCreatedDate).Take(5) :
                    DbSet.Where(x => (string.IsNullOrEmpty(requester) || x.RecCreatedBy == requester) && (date == newDataTime || DbFunctions.TruncateTime(x.RecCreatedDate) == date.Date)).OrderByDescending(x => x.RecCreatedDate).Take(5);
            }
            return status > 0 ? DbSet.Where(x => x.RecCreatedBy == requester && x.Status == status && (date == newDataTime || DbFunctions.TruncateTime(x.RecCreatedDate) == date.Date)).OrderByDescending(x => x.RecCreatedDate).Take(5) :
                DbSet.Where(x => x.RecCreatedBy == requester && (date == newDataTime || DbFunctions.TruncateTime(x.RecCreatedDate) == date.Date)).OrderByDescending(x => x.RecCreatedDate).Take(5);
        }

        public string GetLastFormNumber()
        {
            RIF rif = DbSet.OrderByDescending(x => x.RecCreatedDate).FirstOrDefault();
            if (rif != null)
                return rif.FormNumber;
            return "RI00000000";
        }
    }
}
