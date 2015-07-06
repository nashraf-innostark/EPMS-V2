using System.Collections.Generic;
using System.Linq;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.Repositories;

namespace EPMS.Implementation.Services
{
    public class ItemWarehouseService : IItemWarehouseService
    {
        #region Private
        private readonly IItemWarehouseRepository warehouseRepository;
        private readonly ItemReleaseQuantityRepository itemReleaseQuantityRepository;

        #endregion

        #region Constructor
        public ItemWarehouseService(IItemWarehouseRepository warehouseRepository, ItemReleaseQuantityRepository itemReleaseQuantityRepository)
        {
            this.warehouseRepository = warehouseRepository;
            this.itemReleaseQuantityRepository = itemReleaseQuantityRepository;
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

        public IEnumerable<ItemWarehouse> GetAllWarehouses(long variationId)
        {
            var releaseQuantities = itemReleaseQuantityRepository.GetAll();
            var itemWarehouses = warehouseRepository.GetItemWarehousesByVariationId(variationId).ToList();
            foreach (ItemWarehouse itemWarehouse in itemWarehouses)
            {
                var itemReleaseQuantity = releaseQuantities.Where(a=>a.ItemVariationId == variationId).FirstOrDefault(x => x.WarehouseId == itemWarehouse.WarehouseId && x.ItemVariationId == itemWarehouse.ItemVariationId);
                if (
                    itemReleaseQuantity != null)
                    itemWarehouse.Quantity = itemWarehouse.Quantity - itemReleaseQuantity.Quantity;
            }
            return itemWarehouses;            
        }

        #endregion
    }
}
