using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
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
    public class TIRRepository : BaseRepository<TIR>, ITIRRepository
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
        private readonly Dictionary<TirRequestByColumn, Func<TIR, object>> tirClauseE =

            new Dictionary<TirRequestByColumn, Func<TIR, object>>
                    {
                        { TirRequestByColumn.TirNo,  c => c.Id},
                        { TirRequestByColumn.Requester, c => c.AspNetUser.Employee.EmployeeFirstNameE + " " + c.AspNetUser.Employee.EmployeeMiddleNameE + " " + c.AspNetUser.Employee.EmployeeLastNameE},
                        { TirRequestByColumn.DateCreated, c => c.RecCreatedDate},
                        { TirRequestByColumn.Status, c => c.Status},
                    };
        /// <summary>
        /// Order by Column Names Dictionary statements
        /// </summary>
        private readonly Dictionary<TirRequestByColumn, Func<TIR, object>> tirClauseA =

            new Dictionary<TirRequestByColumn, Func<TIR, object>>
                    {
                        { TirRequestByColumn.TirNo,  c => c.Id},
                        { TirRequestByColumn.Requester, c => c.AspNetUser.Employee.EmployeeFirstNameA + " " + c.AspNetUser.Employee.EmployeeMiddleNameA + " " + c.AspNetUser.Employee.EmployeeLastNameA},
                        { TirRequestByColumn.DateCreated, c => c.RecCreatedDate},
                        { TirRequestByColumn.Status, c => c.Status},
                    };
        #endregion

        public TIRListResponse GetAllTirs(TransferItemSearchRequest searchRequest)
        {
            int fromRow = searchRequest.iDisplayStart;
            int toRow = searchRequest.iDisplayStart + searchRequest.iDisplayLength;
            if (searchRequest.iSortCol_0 == 0)
            {
                searchRequest.iSortCol_0 = 1;
            }
            Expression<Func<TIR, bool>> query;
            int status;
            if (int.TryParse(searchRequest.SearchString, out status))
            {
                if (searchRequest.CompleteAccess)
                {
                    if (searchRequest.iSortCol_0 == 3)
                    {
                        var date = Convert.ToDateTime(searchRequest.SearchString);
                        query = s =>
                            (string.IsNullOrEmpty(searchRequest.SearchString) || (s.FormNumber.Equals(searchRequest.SearchString) ||
                    s.AspNetUser.Employee.EmployeeFirstNameE.Contains(searchRequest.SearchString) || s.AspNetUser.Employee.EmployeeMiddleNameE.Contains(searchRequest.SearchString) ||
                    s.AspNetUser.Employee.EmployeeLastNameE.Contains(searchRequest.SearchString) || s.AspNetUser.Employee.EmployeeFirstNameA.Contains(searchRequest.SearchString) ||
                    s.AspNetUser.Employee.EmployeeMiddleNameA.Contains(searchRequest.SearchString) || s.AspNetUser.Employee.EmployeeLastNameA.Contains(searchRequest.SearchString) ||
                    s.RecCreatedDate.Equals(date) || s.Status == status));
                    }
                    else
                    {
                        query = s =>
                            (string.IsNullOrEmpty(searchRequest.SearchString) || (s.FormNumber.Equals(searchRequest.SearchString) ||
                    s.AspNetUser.Employee.EmployeeFirstNameE.Contains(searchRequest.SearchString) || s.AspNetUser.Employee.EmployeeMiddleNameE.Contains(searchRequest.SearchString) ||
                    s.AspNetUser.Employee.EmployeeLastNameE.Contains(searchRequest.SearchString) || s.AspNetUser.Employee.EmployeeFirstNameA.Contains(searchRequest.SearchString) ||
                    s.AspNetUser.Employee.EmployeeMiddleNameA.Contains(searchRequest.SearchString) || s.AspNetUser.Employee.EmployeeLastNameA.Contains(searchRequest.SearchString) ||
                    s.Status == status));
                    }
                }
                else
                {
                    if (searchRequest.iSortCol_0 == 3)
                    {
                        var date = Convert.ToDateTime(searchRequest.SearchString);
                        query = s =>
                            (string.IsNullOrEmpty(searchRequest.SearchString) || (s.FormNumber.Equals(searchRequest.SearchString) ||
                    s.AspNetUser.Employee.EmployeeFirstNameE.Contains(searchRequest.SearchString) || s.AspNetUser.Employee.EmployeeMiddleNameE.Contains(searchRequest.SearchString) ||
                    s.AspNetUser.Employee.EmployeeLastNameE.Contains(searchRequest.SearchString) || s.AspNetUser.Employee.EmployeeFirstNameA.Contains(searchRequest.SearchString) ||
                    s.AspNetUser.Employee.EmployeeMiddleNameA.Contains(searchRequest.SearchString) || s.AspNetUser.Employee.EmployeeLastNameA.Contains(searchRequest.SearchString) ||
                    s.RecCreatedDate.Equals(date) || s.Status == status));
                    }
                    else
                    {
                        query = s =>
                            (string.IsNullOrEmpty(searchRequest.SearchString) || (s.FormNumber.Equals(searchRequest.SearchString) ||
                    s.AspNetUser.Employee.EmployeeFirstNameE.Contains(searchRequest.SearchString) || s.AspNetUser.Employee.EmployeeMiddleNameE.Contains(searchRequest.SearchString) ||
                    s.AspNetUser.Employee.EmployeeLastNameE.Contains(searchRequest.SearchString) || s.AspNetUser.Employee.EmployeeFirstNameA.Contains(searchRequest.SearchString) ||
                    s.AspNetUser.Employee.EmployeeMiddleNameA.Contains(searchRequest.SearchString) || s.AspNetUser.Employee.EmployeeLastNameA.Contains(searchRequest.SearchString) ||
                    s.Status == status));
                    }
                }
            }
            else
            {
                if (searchRequest.CompleteAccess)
                {
                    if (searchRequest.iSortCol_0 == 3 && searchRequest.SearchString != "")
                    {
                        var date = Convert.ToDateTime(searchRequest.SearchString);
                        query = s =>
                            (string.IsNullOrEmpty(searchRequest.SearchString) || (s.FormNumber.Equals(searchRequest.SearchString) ||
                    s.AspNetUser.Employee.EmployeeFirstNameE.Contains(searchRequest.SearchString) || s.AspNetUser.Employee.EmployeeMiddleNameE.Contains(searchRequest.SearchString) ||
                    s.AspNetUser.Employee.EmployeeLastNameE.Contains(searchRequest.SearchString) || s.AspNetUser.Employee.EmployeeFirstNameA.Contains(searchRequest.SearchString) ||
                    s.AspNetUser.Employee.EmployeeMiddleNameA.Contains(searchRequest.SearchString) || s.AspNetUser.Employee.EmployeeLastNameA.Contains(searchRequest.SearchString) ||
                    s.RecCreatedDate.Equals(date)));
                    }
                    else
                    {
                        query = s =>
                            (string.IsNullOrEmpty(searchRequest.SearchString) || (s.FormNumber.Equals(searchRequest.SearchString) ||
                    s.AspNetUser.Employee.EmployeeFirstNameE.Contains(searchRequest.SearchString) || s.AspNetUser.Employee.EmployeeMiddleNameE.Contains(searchRequest.SearchString) ||
                    s.AspNetUser.Employee.EmployeeLastNameE.Contains(searchRequest.SearchString) || s.AspNetUser.Employee.EmployeeFirstNameA.Contains(searchRequest.SearchString) ||
                    s.AspNetUser.Employee.EmployeeMiddleNameA.Contains(searchRequest.SearchString) || s.AspNetUser.Employee.EmployeeLastNameA.Contains(searchRequest.SearchString)));
                    }
                }
                else
                {
                    if (searchRequest.iSortCol_0 == 3 && searchRequest.SearchString != "")
                    {
                        var date = Convert.ToDateTime(searchRequest.SearchString);
                        query = s =>
                            (string.IsNullOrEmpty(searchRequest.SearchString) || (s.FormNumber.Equals(searchRequest.SearchString) ||
                    s.AspNetUser.Employee.EmployeeFirstNameE.Contains(searchRequest.SearchString) || s.AspNetUser.Employee.EmployeeMiddleNameE.Contains(searchRequest.SearchString) ||
                    s.AspNetUser.Employee.EmployeeLastNameE.Contains(searchRequest.SearchString) || s.AspNetUser.Employee.EmployeeFirstNameA.Contains(searchRequest.SearchString) ||
                    s.AspNetUser.Employee.EmployeeMiddleNameA.Contains(searchRequest.SearchString) || s.AspNetUser.Employee.EmployeeLastNameA.Contains(searchRequest.SearchString) ||
                    s.RecCreatedDate.Equals(date)));
                    }
                    else
                    {
                        query = s =>
                            (string.IsNullOrEmpty(searchRequest.SearchString) || (s.FormNumber.Equals(searchRequest.SearchString) ||
                    s.AspNetUser.Employee.EmployeeFirstNameE.Contains(searchRequest.SearchString) || s.AspNetUser.Employee.EmployeeMiddleNameE.Contains(searchRequest.SearchString) ||
                    s.AspNetUser.Employee.EmployeeLastNameE.Contains(searchRequest.SearchString) || s.AspNetUser.Employee.EmployeeFirstNameA.Contains(searchRequest.SearchString) ||
                    s.AspNetUser.Employee.EmployeeMiddleNameA.Contains(searchRequest.SearchString) || s.AspNetUser.Employee.EmployeeLastNameA.Contains(searchRequest.SearchString)));
                    }
                }
            }

            IEnumerable<TIR> tirs;
            if (searchRequest.Direction == "ltr")
            {
                tirs = searchRequest.sSortDir_0 == "asc" ?
            DbSet
            .Where(query).OrderBy(tirClauseE[searchRequest.RequestByColumn]).Skip(fromRow).Take(toRow).ToList()
                                       :
                                       DbSet
                                       .Where(query).OrderByDescending(tirClauseE[searchRequest.RequestByColumn]).Skip(fromRow).Take(toRow).ToList();
            }
            else
            {
                tirs = searchRequest.sSortDir_0 == "asc" ?
            DbSet
            .Where(query).OrderBy(tirClauseA[searchRequest.RequestByColumn]).Skip(fromRow).Take(toRow).ToList()
                                       :
                                       DbSet
                                       .Where(query).OrderByDescending(tirClauseA[searchRequest.RequestByColumn]).Skip(fromRow).Take(toRow).ToList();
            }
            return new TIRListResponse { TirItems = tirs, TotalDisplayRecords = DbSet.Count(query), TotalRecords = DbSet.Count(query) };
        }

        public IEnumerable<TIR> GetTirHistoryData()
        {
            return DbSet.Where(x => x.Status == 1 || x.Status == 2).ToList();
        }
    }
}
