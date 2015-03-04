using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using Microsoft.Practices.Unity;
using EPMS.Repository.BaseRepository;

namespace EPMS.Repository.Repositories
{
    public sealed class ComplaintRepository: BaseRepository<Complaint>, IComplaintRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ComplaintRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Complaint> DbSet
        {
            get { return db.Complaint; }
        }

        #endregion

        public IEnumerable<Complaint> GetAllComplaintsByCustomerId(long id)
        {
            return DbSet.Where(x => x.CustomerId == id).OrderByDescending(x=>x.ComplaintDate);
        }

        public IEnumerable<Complaint> GetComplaintsForDashboard(string requester)
        {
            if (requester == "Admin")
            {
                return DbSet.OrderByDescending(x => x.ComplaintDate).Take(5);
            }
            long customerId = Convert.ToInt64(requester);
            return DbSet.Where(x => x.CustomerId == customerId).OrderByDescending(x => x.ComplaintDate).Take(4);
        }
    }
}
