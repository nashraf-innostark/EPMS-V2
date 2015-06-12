using System;
using System.Collections.Generic;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Implementation.Services
{
    public class PoItemService : IPoItemService
    {
        private readonly IPoItemRepository repository;

        public PoItemService(IPoItemRepository itemRepository)
        {
            this.repository = itemRepository;
        }

        public IEnumerable<PurchaseOrderItem> GetPoItemsByPoId(long id)
        {
            return repository.GetPoItemsByPoId(id);
        }

        public PurchaseOrderItem Find(long id)
        {
            return repository.Find(id);
        }

        public bool AddPoItem(PurchaseOrderItem item)
        {
            try
            {
                repository.Add(item);
                repository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdatePoItem(PurchaseOrderItem item)
        {
            try
            {
                repository.Update(item);
                repository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void DeletePoItem(PurchaseOrderItem item)
        {
            repository.Delete(item);
            repository.SaveChanges();
        }
    }
}
