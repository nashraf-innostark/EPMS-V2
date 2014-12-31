using System.Data.Entity;
using System.Linq;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using Microsoft.Practices.Unity;
using EPMS.Repository.BaseRepository;

namespace EPMS.Repository.Repositories
{
    public class EmployeeRequestDetailRepository: BaseRepository<RequestDetail>, IEmployeeRequestDetailRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public EmployeeRequestDetailRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<RequestDetail> DbSet
        {
            get { return db.RequestDetails; }
        }

        #endregion

        public RequestDetail LoadRequestDetailByRequestId(long requestId)
        {
            return DbSet.Where(x=>x.RequestId==requestId).OrderByDescending(x=>x.RowVersion).FirstOrDefault();
        }
    }
}
