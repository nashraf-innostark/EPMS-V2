using System;
using System.Collections.Generic;
using System.Linq;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;

namespace EPMS.Implementation.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IWarehouseRepository warehouseRepository;
        private readonly IEmployeeService employeeService;

        public WarehouseService(IWarehouseRepository warehouseRepository, IEmployeeService employeeService)
        {
            this.warehouseRepository = warehouseRepository;
            this.employeeService = employeeService;
        }

        public IEnumerable<Warehouse> GetAll()
        {
            return warehouseRepository.GetAll();
        }

        public Warehouse FindWarehouseById(long id)
        {
            return warehouseRepository.Find(id);
        }

        public WarehouseRequest GetWarehouseRequest(long id)
        {
            WarehouseRequest request = new WarehouseRequest();
            if (id > 0)
            {
                request.Warehouse = FindWarehouseById(id);
            }
            request.Employees = employeeService.GetAll();
            request.Warehouses = warehouseRepository.GetAll();
            return request;
        }

        public string GetLastWarehouseNumber()
        {
            var lastOrDefault = warehouseRepository.GetAll().OrderByDescending(x=>x.WarehouseId).FirstOrDefault();
            if (lastOrDefault != null)
            {
                return lastOrDefault.WarehouseNumber;
            }
            return "";
        }

        public bool AddWarehouse(Warehouse warehouse)
        {
            if (warehouseRepository.WarehouseExists(warehouse))
            {
                throw new ArgumentException("Warehouse already exists");
            }
            try
            {
                warehouseRepository.Add(warehouse);
                warehouseRepository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Updatewarehouse(Warehouse warehouse)
        {
            if (warehouseRepository.WarehouseExists(warehouse))
            {
                throw new ArgumentException("Warehouse already exists");
            }
            try
            {
                warehouseRepository.Update(warehouse);
                warehouseRepository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string DeleteWarehouse(long id)
        {
            Warehouse warehouse = warehouseRepository.Find(id);
            if (warehouse != null)
            {
                if (IsAssociated(warehouse))
                {
                    return "Associated";
                }
                warehouseRepository.Delete(warehouse);
                warehouseRepository.SaveChanges();
                return "Success";
            }
            return "Error";
        }

        private bool IsAssociated(Warehouse warehouse)
        {
            if (warehouse.WarehouseDetails == null || !warehouse.WarehouseDetails.Any())
            {
                if (warehouse.ItemWarehouses == null || !warehouse.ItemWarehouses.Any())
                {
                    if (warehouse.ItemVariations == null || !warehouse.ItemVariations.Any())
                    {
                        if (warehouse.ItemReleaseQuantities == null || !warehouse.ItemReleaseQuantities.Any())
                        {
                            if (warehouse.PhysicalCountItems == null || !warehouse.PhysicalCountItems.Any())
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
    }
}
