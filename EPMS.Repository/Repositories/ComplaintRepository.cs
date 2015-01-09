using System.Data.Entity;
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
    }
}
