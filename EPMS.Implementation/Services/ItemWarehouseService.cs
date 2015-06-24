using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;

namespace EPMS.Implementation.Services
{
    public class ItemWarehouseService : IItemWarehouseService
    {
        #region Private
        private readonly IItemWarehouseRepository warehouseRepository;
        #endregion

        #region Constructor
        public ItemWarehouseService(IItemWarehouseRepository warehouseRepository)
        {
            this.warehouseRepository = warehouseRepository;
        }
        #endregion

        #region Public
        public IEnumerable<ItemWarehouse> GetAll()
        {
            return warehouseRepository.GetAll();
        }

        public ItemWarehouse FindItemWarehouseById(long id)
        {
            return warehouseRepository.Find(id);
        }

        public bool AddItemWarehouse(ItemWarehouse itemWarehouse)
        {
            warehouseRepository.Add(itemWarehouse);
            warehouseRepository.SaveChanges();
            return true;
        }

        public bool UpdateItemWarehouse(ItemWarehouse itemWarehouse)
        {
            warehouseRepository.Update(itemWarehouse);
            warehouseRepository.SaveChanges();
            return true;
        }

        public void DeleteItemWarehouse(ItemWarehouse itemWarehouse)
        {
            warehouseRepository.Delete(itemWarehouse);
            warehouseRepository.SaveChanges();
        }

        public IEnumerable<ItemWarehouse> GetItemsByVariationId(long variationId)
        {
            return warehouseRepository.GetItemsByVariationId(variationId);
        }

        #endregion
    }
}
