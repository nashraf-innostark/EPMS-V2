using System.Collections.Generic;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;

namespace EPMS.Implementation.Services
{
    class ItemManufacturerService : IItemManufacturerService
    {
        #region Private
        private readonly IItemManufacturerRepository manufacturerRepository;
        #endregion

        #region Constructor
        public ItemManufacturerService(IItemManufacturerRepository manufacturerRepository)
        {
            this.manufacturerRepository = manufacturerRepository;
        }
        #endregion

        #region Public
        public IEnumerable<ItemManufacturer> GetAll()
        {
            return manufacturerRepository.GetAll();
        }

        public ItemManufacturer FindItemManufacturerById(long id)
        {
            return manufacturerRepository.Find(id);
        }

        public bool AddItemManufacturer(ItemManufacturer itemManufacturer)
        {
            manufacturerRepository.Add(itemManufacturer);
            manufacturerRepository.SaveChanges();
            return true;
        }

        public bool UpdateItemManufacturer(ItemManufacturer itemManufacturer)
        {
            manufacturerRepository.Update(itemManufacturer);
            manufacturerRepository.SaveChanges();
            return true;
        }

        public void DeleteItemManufacturer(ItemManufacturer itemManufacturer)
        {
            manufacturerRepository.Delete(itemManufacturer);
            manufacturerRepository.SaveChanges();
        }
        #endregion
    }
}
