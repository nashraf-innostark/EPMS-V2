using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EPMS.Interfaces.Repository;
using EPMS.Models.RequestModels.Reports;
using EPMS.Models.ResponseModels.ReportsResponseModels;
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

        public IEnumerable<WarehouseReportDetails> GetWarehouseReportDetails(WarehouseReportCreateOrDetailsRequest request)
        {
            var result = DbSet
                .Include(x=>x.ItemWarehouses)
                .Include(x=>x.DIFs)
                .Where(x => request.WarehouseId==0 || x.WarehouseId.Equals(request.WarehouseId))
                .GroupBy(x=>x.WarehouseNumber)
                .Select(warehouse=>new WarehouseReportDetails
            {
                WarehouseName = warehouse.Key,
                IsFull = warehouse.FirstOrDefault().IsFull,
                AvailabelItems = warehouse.FirstOrDefault().ItemWarehouses.Sum(x=>x.Quantity),
                DefectiveItems = warehouse.FirstOrDefault().DIFs.Sum(x => x.DIFItems.Sum(y => y.ItemQty))
            }).ToList();
            return result;
        }

        public override IQueryable<Warehouse> GetAll()
        {
            return DbSet.Include(x => x.Employee);
        }
    }
}
