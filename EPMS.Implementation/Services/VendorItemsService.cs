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
    class VendorItemsService : IVendorItemsService
    {
        private readonly IVendorItemsRepository vendorItemsRepository;

        public VendorItemsService(IVendorItemsRepository vendorItemsRepository)
        {
            this.vendorItemsRepository = vendorItemsRepository;
        }
        public IEnumerable<VendorItem> GetAll()
        {
            return vendorItemsRepository.GetAll();
        }

        public VendorItem FindItemsById(long id)
        {
            return vendorItemsRepository.Find(id);
        }

        public bool AddItem(VendorItem vendor)
        {
            vendorItemsRepository.Add(vendor);
            vendorItemsRepository.SaveChanges();
            return true;
        }

        public bool UpdateItem(VendorItem vendor)
        {
            vendorItemsRepository.Update(vendor);
            vendorItemsRepository.SaveChanges();
            return true;
        }

        public void DeleteItem(VendorItem vendor)
        {
            vendorItemsRepository.Delete(vendor);
            vendorItemsRepository.SaveChanges();
        }

        public IEnumerable<VendorItem> GetItemsByVendorId(long vendorId)
        {
            return vendorItemsRepository.GetItemsByVendorId(vendorId);
        }
    }
}
