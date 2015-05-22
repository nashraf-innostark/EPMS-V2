using System.Data.Entity;
using System.Linq;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    class StatusRepository : BaseRepository<Status>, IStatusRepository
    {
        public StatusRepository(IUnityContainer container) : base(container)
        {
        }

        protected override IDbSet<Status> DbSet
        {
            get { return db.Statuses; }
        }

        public bool StatusExists(Status status)
        {
            if (status.StatusId > 0) //Already in the System
            {
                return DbSet.Any(
                    st => status.StatusId != st.StatusId &&
                        (st.StatusNameEn == status.StatusNameEn || st.StatusNameAr == status.StatusNameAr));
            }
            return DbSet.Any(
                    st =>
                        (st.StatusNameEn == status.StatusNameEn || st.StatusNameAr == status.StatusNameAr));
        }
    }
}
