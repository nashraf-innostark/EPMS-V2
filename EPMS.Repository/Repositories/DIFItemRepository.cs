using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    //public class RIFItemRepository:BaseRepository<RIFItem>,IRIFItemRepository
    //{
    //     #region Constructor
    //    /// <summary>
    //    /// Constructor
    //    /// </summary>
    //    public RIFItemRepository(IUnityContainer container)
    //        : base(container)
    //    {
    //    }

    //    /// <summary>
    //    /// Primary database set
    //    /// </summary>
    //    protected override IDbSet<RIFItem> DbSet
    //    {
    //        get { return db.RIFItem; }
    //    }

    //    #endregion

    //    public IEnumerable<RIFItem> GetRifItemsByRifId(long rfiId)
    //    {
    //        return DbSet.Where(x => x.RIFId == rfiId);
    //    }
    //}
}
