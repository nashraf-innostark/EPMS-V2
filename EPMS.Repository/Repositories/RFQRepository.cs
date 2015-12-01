using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EPMS.Interfaces.Repository;
using EPMS.Models.Common;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels.Reports;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class RFQRepository : BaseRepository<RFQ>, IRFQRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public RFQRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<RFQ> DbSet
        {
            get { return db.Rfqs; }
        }

        #endregion

        public RFQ FindByRfqId(long rfqId)
        {
            return DbSet.FirstOrDefault(x => x.RFQId == rfqId);
        }

        public IEnumerable<RFQ> GetAllPendingRfqs()
        {
            return DbSet.Where(x => x.Status == (int) RFQStatus.Pending);
        }

        public IEnumerable<RFQ> GetRfqsByCustomerId(long customerId)
        {
            return DbSet.Where(x => x.CustomerId == customerId);
        }

        public IEnumerable<RFQ> GetPendingRfqsByCustomerId(long customerId)
        {
            return DbSet.Where(x => x.CustomerId == customerId && x.Status == (int) RFQStatus.Pending);
        }

        public IEnumerable<RFQ> GetAllRFQsByCustomerId(QOReportCreateOrDetailsRequest request)
        {
            return DbSet.Include("RFQItems").Where(quot => quot.CustomerId == request.CustomerId && quot.RecCreatedDate >= request.From && quot.RecCreatedDate <= request.To);
        }
    }
}
