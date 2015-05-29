using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EPMS.Interfaces.Repository;
using EPMS.Repository.BaseRepository;
using EPMS.Models.DomainModels;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class WarehouseRepository : BaseRepository<Warehouse>, IWarehouseRepository
    {
        public WarehouseRepository(IUnityContainer container) : base(container)
        {
        }

        protected override IDbSet<Warehouse> DbSet
        {
            get { return db.Warehouses; }
        }

        public bool WarehouseExists(Warehouse warehouse)
        {
            if (warehouse.WarehouseId > 0) //Already in the System
            {
                return DbSet.Any(
                    wh => warehouse.WarehouseId != wh.WarehouseId &&
                        (wh.WarehouseNumber == warehouse.WarehouseNumber));
            }
            return DbSet.Any(
                    wh =>
                        (wh.WarehouseNumber == warehouse.WarehouseNumber));
        }

        public override IQueryable<Warehouse> GetAll()
        {
            return DbSet.Include(x => x.Employee);
        }
    }
}
