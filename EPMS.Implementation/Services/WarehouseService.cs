using System;
using System.Collections.Generic;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;

namespace EPMS.Implementation.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IWarehouseRepository warehouseRepository;

        public WarehouseService(IWarehouseRepository warehouseRepository)
        {
            this.warehouseRepository = warehouseRepository;
        }

        public IEnumerable<Warehouse> GetAll()
        {
            return warehouseRepository.GetAll();
        }

        public Warehouse FindWarehouseById(long id)
        {
            return warehouseRepository.Find(id);
        }

        public bool AddWarehouse(Warehouse warehouse)
        {
            if (warehouseRepository.WarehouseExists(warehouse))
            {
                throw new ArgumentException("Warehouse already exists");
            }
            warehouseRepository.Add(warehouse);
            warehouseRepository.SaveChanges();
            return true;
        }

        public bool Updatewarehouse(Warehouse warehouse)
        {
            if (warehouseRepository.WarehouseExists(warehouse))
            {
                throw new ArgumentException("Warehouse already exists");
            }
            warehouseRepository.Update(warehouse);
            warehouseRepository.SaveChanges();
            return true;
        }

        public void DeleteWarehouse(Warehouse warehouse)
        {
            warehouseRepository.Delete(warehouse);
            warehouseRepository.SaveChanges();
        }
    }
}
