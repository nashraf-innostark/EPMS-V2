using System;
using System.Collections.Generic;
using System.Linq;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;

namespace EPMS.Implementation.Services
{
    public class WarehouseDetailService : IWarehouseDetailService
    {
        private readonly IWarehouseDetailRepository detailRepository;

        public WarehouseDetailService(IWarehouseDetailRepository detailRepository)
        {
            this.detailRepository = detailRepository;
        }

        public IEnumerable<WarehouseDetail> GetAll()
        {
            return detailRepository.GetAll();
        }

        public WarehouseDetail FindWarehouseDetailById(long id)
        {
            return detailRepository.Find(id);
        }

        public bool AddWarehouseDetail(WarehouseDetail warehouseDetail)
        {
            try
            {
                detailRepository.Add(warehouseDetail);
                detailRepository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateWarehouseDetail(WarehouseDetail warehouseDetail)
        {
            try
            {
                detailRepository.Update(warehouseDetail);
                detailRepository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string DeleteWarehouseDetail(long id)
        {
            WarehouseDetail warehouseDetail = detailRepository.Find(id);
            if (warehouseDetail != null)
            {
                if (warehouseDetail.ItemWarehouses.Any())
                {
                    return "AssociatedItems";
                }
                if (warehouseDetail.WarehouseDetails.Any())
                {
                    return "AssociatedDetails";
                }
                detailRepository.Delete(warehouseDetail);
                detailRepository.SaveChanges();
                return "Success";
            }
            return "Error";
        }
    }
}
