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
    public class RFIRepository : BaseRepository<RFI>, IRFIRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public RFIRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<RFI> DbSet
        {
            get { return db.RFI; }
        }

        #endregion
        #region Private

        /// <summary>
        /// Order by Column Names Dictionary statements
        /// </summary>
        private readonly Dictionary<RfiRequestByColumn, Func<RFI, object>> rfiRequestClause =

            new Dictionary<RfiRequestByColumn, Func<RFI, object>>
                    {
                        { RfiRequestByColumn.RfiNo, c => c.RFIId},
                        { RfiRequestByColumn.Requester,  c => c.Order.Customer.CustomerNameE },
                        { RfiRequestByColumn.DateCreated, c => c.RecCreatedDate},
                        { RfiRequestByColumn.Status,  c => c.Status}
                    };
        #endregion
        public RfiRequestResponse LoadAllRfis(RfiSearchRequest rfiSearchRequest)
        {
            int fromRow = rfiSearchRequest.iDisplayStart;
            int toRow = rfiSearchRequest.iDisplayStart + rfiSearchRequest.iDisplayLength;
            Expression<Func<RFI, bool>> query;
            IEnumerable<RFI> rfis;
            if (rfiSearchRequest.Requester == "Admin")
            {
                query =
                s => ((string.IsNullOrEmpty(rfiSearchRequest.SearchString)) ||
                    //(s.RFIId.Equals(Convert.ToInt64(rfiSearchRequest.SearchString))) ||
                    //(s.RecCreatedDate.ToShortDateString().Contains(rfiSearchRequest.SearchString))||
                    (s.AspNetUser.Employee.EmployeeFirstNameE.Contains(rfiSearchRequest.SearchString)) ||
                    (s.AspNetUser.Employee.EmployeeLastNameE.Contains(rfiSearchRequest.SearchString)) ||
                    (s.AspNetUser.Employee.EmployeeFirstNameA.Contains(rfiSearchRequest.SearchString)) ||
                    (s.AspNetUser.Employee.EmployeeMiddleNameA.Contains(rfiSearchRequest.SearchString))
                    
                    );
            }
            else
            {
                var employeeId = rfiSearchRequest.Requester;
                query =
                s => (((string.IsNullOrEmpty(rfiSearchRequest.SearchString)) ||
                    //(s.RFIId.Equals(Convert.ToInt64(rfiSearchRequest.SearchString))) ||
                    //(s.RecCreatedDate.ToShortDateString().Contains(rfiSearchRequest.SearchString))||
                    (s.Order.Customer.CustomerNameE.Contains(rfiSearchRequest.SearchString)) ||
                    (s.Order.Customer.CustomerNameA.Contains(rfiSearchRequest.SearchString)) 
                    ) && (s.RecCreatedBy.Equals(employeeId)));
            }

            if (rfiSearchRequest.iSortCol_0 == 0)
            {
                rfis = DbSet
                .Where(query).OrderByDescending(x => x.RecCreatedDate).Skip(fromRow).Take(toRow).ToList();
            }
            else if (rfiSearchRequest.iSortCol_0 == 3 && rfiSearchRequest.Requester == "Admin")
            {
                rfis = rfiSearchRequest.sSortDir_0 == "asc" ? 
                    DbSet.Where(query).OrderBy(x => x.AspNetUser.Employee.EmployeeFirstNameE).Skip(fromRow).Take(toRow).ToList():
                    DbSet.Where(query).OrderByDescending(x => x.AspNetUser.Employee.EmployeeFirstNameE).Skip(fromRow).Take(toRow).ToList();
            }
            else
            {
                rfis = rfiSearchRequest.sSortDir_0 == "asc" ?
                DbSet
                .Where(query).OrderBy(rfiRequestClause[rfiSearchRequest.RfiRequestByColumn]).Skip(fromRow).Take(toRow).ToList()
                :
                DbSet
                .Where(query).OrderByDescending(rfiRequestClause[rfiSearchRequest.RfiRequestByColumn]).Skip(fromRow).Take(toRow).ToList();
            }
            return new RfiRequestResponse { Rfis = rfis, TotalCount = DbSet.Count(query) };
        }

        public IEnumerable<RFI> GetRfiByRequesterId(string requesterId)
        {
            return DbSet.Where(x=>x.RecCreatedBy == requesterId);
        }
    }
}
