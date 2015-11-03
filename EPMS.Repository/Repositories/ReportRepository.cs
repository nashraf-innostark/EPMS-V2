using System.Data.Entity;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class ReportRepository : BaseRepository<Report>, IReportRepository
    {
        #region Constructor

        public ReportRepository(IUnityContainer container) : base(container)
        {
        }

        protected override IDbSet<Report> DbSet
        {
            get { return db.Report; }
        }

        #endregion
    }
}
