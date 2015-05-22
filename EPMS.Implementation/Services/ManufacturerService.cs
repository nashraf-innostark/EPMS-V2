using System;
using System.Collections.Generic;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;

namespace EPMS.Implementation.Services
{
    class ManufacturerService : IManufacturerService
    {
        private readonly IManufacturerRepository manufacturerRepository;

        public ManufacturerService(IManufacturerRepository manufacturerRepository)
        {
            this.manufacturerRepository = manufacturerRepository;
        }

        public IEnumerable<Manufacturer> GetAll()
        {
            return manufacturerRepository.GetAll();
        }

        public Manufacturer FindManufacturerById(long id)
        {
            return manufacturerRepository.Find(id);
        }

        public bool AddManufacturer(Manufacturer manufacturer)
        {
            if (manufacturerRepository.ManufacturerExists(manufacturer))
            {
                throw new ArgumentException("Manufacturer already exists");
            }
            manufacturerRepository.Add(manufacturer);
            manufacturerRepository.SaveChanges();
            return true;
        }

        public bool UpdateManufacturer(Manufacturer manufacturer)
        {
            if (manufacturerRepository.ManufacturerExists(manufacturer))
            {
                throw new ArgumentException("Manufacturer already exists");
            }
            manufacturerRepository.Update(manufacturer);
            manufacturerRepository.SaveChanges();
            return true;
        }

        public void DeleteManufacturer(Manufacturer manufacturer)
        {
            manufacturerRepository.Delete(manufacturer);
            manufacturerRepository.SaveChanges();
        }
    }
}
