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
    public class PurchaseOrderRepository : BaseRepository<PurchaseOrder>, IPurchaseOrderRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public PurchaseOrderRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<PurchaseOrder> DbSet
        {
            get { return db.PurchaseOrders; }
        }

        #endregion

        #region Private

        /// <summary>
        /// Order by Column Names Dictionary statements For Manager
        /// </summary>
        private readonly Dictionary<PurchaseOrderByColumn, Func<PurchaseOrder, object>> purchaseOrderClauseManager =

            new Dictionary<PurchaseOrderByColumn, Func<PurchaseOrder, object>>
                    {
                        { PurchaseOrderByColumn.FormNumber,  c => c.FormNumber},
                        { PurchaseOrderByColumn.RequesterOrManager, c => c.Manager.Employee.EmployeeFirstNameE},
                        { PurchaseOrderByColumn.Date, c => c.RecCreatedDate},
                        { PurchaseOrderByColumn.Status, c => c.Status}
                    };
        /// <summary>
        /// Order by Column Names Dictionary statements For Employee
        /// </summary>
        private readonly Dictionary<PurchaseOrderByColumn, Func<PurchaseOrder, object>> purchaseOrderClauseEmployee =

            new Dictionary<PurchaseOrderByColumn, Func<PurchaseOrder, object>>
                    {
                        { PurchaseOrderByColumn.FormNumber,  c => c.FormNumber},
                        { PurchaseOrderByColumn.RequesterOrManager, c => c.AspNetUser.Employee.EmployeeFirstNameE},
                        { PurchaseOrderByColumn.Date, c => c.RecCreatedDate},
                        { PurchaseOrderByColumn.Status, c => c.Status}
                    };
        #endregion

        public PurchaseOrderListResponse GetAllPoS(PurchaseOrderSearchRequest searchRequest)
        {
            int fromRow = searchRequest.iDisplayStart;
            int toRow = searchRequest.iDisplayStart + searchRequest.iDisplayLength;
            if (searchRequest.iSortCol_0 == 0)
            {
                searchRequest.iSortCol_0 = 1;
            }
            Expression<Func<PurchaseOrder, bool>> query;
            int status;
            if (int.TryParse(searchRequest.SearchString, out status))
            {
                if (searchRequest.IsManager)
                {
                    if (searchRequest.iSortCol_0 == 3)
                    {
                        var date = Convert.ToDateTime(searchRequest.SearchString);
                        query = s =>
                            (string.IsNullOrEmpty(searchRequest.SearchString) || (s.FormNumber.Equals(searchRequest.SearchString) ||
                    s.Manager.Employee.EmployeeFirstNameE.Contains(searchRequest.SearchString) || s.Manager.Employee.EmployeeMiddleNameE.Contains(searchRequest.SearchString) ||
                    s.Manager.Employee.EmployeeLastNameE.Contains(searchRequest.SearchString) || s.Manager.Employee.EmployeeFirstNameA.Contains(searchRequest.SearchString) ||
                    s.Manager.Employee.EmployeeMiddleNameA.Contains(searchRequest.SearchString) || s.Manager.Employee.EmployeeLastNameA.Contains(searchRequest.SearchString) ||
                    s.RecCreatedDate.Equals(date) || s.Status == status));
                    }
                    else
                    {
                        query = s =>
                            (string.IsNullOrEmpty(searchRequest.SearchString) || (s.FormNumber.Equals(searchRequest.SearchString) ||
                    s.Manager.Employee.EmployeeFirstNameE.Contains(searchRequest.SearchString) || s.Manager.Employee.EmployeeMiddleNameE.Contains(searchRequest.SearchString) ||
                    s.Manager.Employee.EmployeeLastNameE.Contains(searchRequest.SearchString) || s.Manager.Employee.EmployeeFirstNameA.Contains(searchRequest.SearchString) ||
                    s.Manager.Employee.EmployeeMiddleNameA.Contains(searchRequest.SearchString) || s.Manager.Employee.EmployeeLastNameA.Contains(searchRequest.SearchString) ||
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
                if (searchRequest.IsManager)
                {
                    if (searchRequest.iSortCol_0 == 3 && searchRequest.SearchString != "")
                    {
                        var date = Convert.ToDateTime(searchRequest.SearchString);
                        query = s =>
                            (string.IsNullOrEmpty(searchRequest.SearchString) || (s.FormNumber.Equals(searchRequest.SearchString) ||
                    s.Manager.Employee.EmployeeFirstNameE.Contains(searchRequest.SearchString) || s.Manager.Employee.EmployeeMiddleNameE.Contains(searchRequest.SearchString) ||
                    s.Manager.Employee.EmployeeLastNameE.Contains(searchRequest.SearchString) || s.Manager.Employee.EmployeeFirstNameA.Contains(searchRequest.SearchString) ||
                    s.Manager.Employee.EmployeeMiddleNameA.Contains(searchRequest.SearchString) || s.Manager.Employee.EmployeeLastNameA.Contains(searchRequest.SearchString) ||
                    s.RecCreatedDate.Equals(date)));
                    }
                    else
                    {
                        query = s =>
                            (string.IsNullOrEmpty(searchRequest.SearchString) || (s.FormNumber.Equals(searchRequest.SearchString) ||
                    s.Manager.Employee.EmployeeFirstNameE.Contains(searchRequest.SearchString) || s.Manager.Employee.EmployeeMiddleNameE.Contains(searchRequest.SearchString) ||
                    s.Manager.Employee.EmployeeLastNameE.Contains(searchRequest.SearchString) || s.Manager.Employee.EmployeeFirstNameA.Contains(searchRequest.SearchString) ||
                    s.Manager.Employee.EmployeeMiddleNameA.Contains(searchRequest.SearchString) || s.Manager.Employee.EmployeeLastNameA.Contains(searchRequest.SearchString)));
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

            IEnumerable<PurchaseOrder> pos;
            if (searchRequest.IsManager)
            {
                pos = searchRequest.sSortDir_0 == "asc" ?
            DbSet
            .Where(query).OrderBy(purchaseOrderClauseManager[searchRequest.PurchaseOrderByColumn]).Skip(fromRow).Take(toRow).ToList()
                                       :
                                       DbSet
                                       .Where(query).OrderByDescending(purchaseOrderClauseManager[searchRequest.PurchaseOrderByColumn]).Skip(fromRow).Take(toRow).ToList();
            }
            else
            {
                pos = searchRequest.sSortDir_0 == "asc" ?
            DbSet
            .Where(query).OrderBy(purchaseOrderClauseEmployee[searchRequest.PurchaseOrderByColumn]).Skip(fromRow).Take(toRow).ToList()
                                       :
                                       DbSet
                                       .Where(query).OrderByDescending(purchaseOrderClauseEmployee[searchRequest.PurchaseOrderByColumn]).Skip(fromRow).Take(toRow).ToList();
            }
            return new PurchaseOrderListResponse { Orders = pos, TotalDisplayRecords = DbSet.Count(query), TotalRecords = DbSet.Count(query) };
        }
        
    }
}
