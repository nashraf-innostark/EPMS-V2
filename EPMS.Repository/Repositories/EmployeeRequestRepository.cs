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

        public IEnumerable<EmployeeRequest> GetAllMonetaryRequests(DateTime currentMonth, long id)
        {
            return DbSet.Where(x => (x.IsMonetary) && (x.EmployeeId == id) && (x.RequestDetails.Count(y => (y.IsApproved))>0) && (x.RequestDetails.Count(z => ((currentMonth >= z.FirstInstallmentDate) && (currentMonth <= z.LastInstallmentDate))) > 0));
        }

        public EmployeeRequestResponse GetAllRequests(EmployeeRequestSearchRequest searchRequset)
        {
            int fromRow = (searchRequset.PageNo - 1) * searchRequset.PageSize;
            int toRow = searchRequset.PageSize;
            Expression<Func<EmployeeRequest, bool>> query;
            if (searchRequset.Requester == "Admin")
            {
                query =
                s => ((string.IsNullOrEmpty(searchRequset.EmployeeName) || (s.Employee.EmployeeNameE.Contains(searchRequset.EmployeeName))));
            }
            else
            {
                long employeeId = Convert.ToInt64(searchRequset.Requester);
                query =
                s => ((string.IsNullOrEmpty(searchRequset.EmployeeName) || (s.Employee.EmployeeNameE.Contains(searchRequset.EmployeeName))) && 
                    (s.EmployeeId.Equals(employeeId)));
            }
            

            IEnumerable<EmployeeRequest> employeeRequests = searchRequset.IsAsc ?
                DbSet
                .Where(query).OrderBy(employeeRequestClause[searchRequset.EmployeeRequestByColumn]).Skip(fromRow).Take(toRow).ToList()
                                           :
                                           DbSet
                                           .Where(query).OrderByDescending(employeeRequestClause[searchRequset.EmployeeRequestByColumn]).Skip(fromRow).Take(toRow).ToList();
            return new EmployeeRequestResponse { EmployeeRequests = employeeRequests, TotalCount = DbSet.Count(query) };
        }
    }
}
