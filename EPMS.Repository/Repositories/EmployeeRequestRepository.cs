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
    public sealed class EmployeeRequestRepository : BaseRepository<EmployeeRequest>, IEmployeeRequestRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public EmployeeRequestRepository(IUnityContainer container) : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<EmployeeRequest> DbSet
        {
            get { return db.EmployeeRequests; }
        }
        
        #endregion
        #region Private

        /// <summary>
        /// Order by Column Names Dictionary statements
        /// </summary>
        private readonly Dictionary<EmployeeRequestByColumn, Func<EmployeeRequest, object>> employeeRequestClause =

            new Dictionary<EmployeeRequestByColumn, Func<EmployeeRequest, object>>
                    {
                        { EmployeeRequestByColumn.RequestTopic, c => c.RequestTopic},
                        { EmployeeRequestByColumn.EmployeeName,  c => c.Employee.EmployeeNameE},
                        { EmployeeRequestByColumn.JobId, c => c.Employee.EmployeeJobId},
                        { EmployeeRequestByColumn.Department, c => c.Employee.JobTitle.Department.DepartmentNameE},
                        { EmployeeRequestByColumn.Date,  c => c.RequestDate}
                    };
        #endregion
        public IEnumerable<EmployeeRequest> GetAllRequests(long employeeId)
        {
            return DbSet.Where(x => x.EmployeeId == employeeId);
        }

        public IEnumerable<EmployeeRequest> GetRequestsForDashboard(string requester)
        {
            if (requester == "Admin")
            {
                return DbSet.OrderByDescending(x => x.RequestDate).Take(5);
            }
            long employeeId = Convert.ToInt64(requester);
            return DbSet.Where(x => x.EmployeeId == employeeId).OrderByDescending(x => x.RequestDate).Take(5);
        }

        public IEnumerable<EmployeeRequest> GetAllMonetaryRequests(DateTime currentMonth, long id)
        {
            return DbSet.Where(x => (x.IsMonetary) && (x.EmployeeId == id) && (x.RequestDetails.Count(y => (y.IsApproved))>0) && (x.RequestDetails.Count(z => ((currentMonth >= z.FirstInstallmentDate) && (currentMonth <= z.LastInstallmentDate))) > 0));
        }

        public EmployeeRequestResponse GetAllRequests(EmployeeRequestSearchRequest searchRequset)
        {
            int fromRow = searchRequset.iDisplayStart;
            int toRow = searchRequset.iDisplayStart+searchRequset.iDisplayLength;
            Expression<Func<EmployeeRequest, bool>> query;
            IEnumerable<EmployeeRequest> employeeRequests;
            if (searchRequset.Requester == "Admin")
            {
                query =
                s => ((string.IsNullOrEmpty(searchRequset.SearchString)) || (s.RequestTopic.Contains(searchRequset.SearchString)) ||
                    (s.Employee.EmployeeNameE.Contains(searchRequset.SearchString)) || (s.Employee.EmployeeNameA.Contains(searchRequset.SearchString)) ||
                    (s.Employee.EmployeeJobId.Contains(searchRequset.SearchString)) ||
                    (s.Employee.JobTitle.Department.DepartmentNameA.Contains(searchRequset.SearchString)) || (s.Employee.JobTitle.Department.DepartmentNameE.Contains(searchRequset.SearchString))
                    );
            }
            else
            {
                long employeeId = Convert.ToInt64(searchRequset.Requester);
                query =
                s => ((string.IsNullOrEmpty(searchRequset.SearchString) || (s.RequestTopic.Contains(searchRequset.SearchString))) && 
                    (s.EmployeeId.Equals(employeeId)));
            }

            if (searchRequset.iSortCol_0 == 0)
            {
                employeeRequests = DbSet
                .Where(query).OrderByDescending(x=>x.RequestDate).Skip(fromRow).Take(toRow).ToList();
            }
            else
            {
                employeeRequests = searchRequset.sSortDir_0 == "asc" ?
                DbSet
                .Where(query).OrderBy(employeeRequestClause[searchRequset.EmployeeRequestByColumn]).Skip(fromRow).Take(toRow).ToList()
                :
                DbSet
                .Where(query).OrderByDescending(employeeRequestClause[searchRequset.EmployeeRequestByColumn]).Skip(fromRow).Take(toRow).ToList();
            }
            return new EmployeeRequestResponse { EmployeeRequests = employeeRequests, TotalCount = DbSet.Count(query) };
        }
    }
}
