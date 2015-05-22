using System;
using System.Collections.Generic;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;

namespace EPMS.Implementation.Services
{
    class SizeService : ISizeService
    {
        private readonly ISizeRepository sizeRepository;

        public SizeService(ISizeRepository sizeRepository)
        {
            this.sizeRepository = sizeRepository;
        }

        public IEnumerable<Size> GetAll()
        {
            return sizeRepository.GetAll();
        }

        public Size FindSizeById(long id)
        {
            return sizeRepository.Find(id);
        }

        public bool AddSize(Size size)
        {
            if (sizeRepository.SizeExists(size))
            {
                throw new ArgumentException("Size already exists");
            }
            sizeRepository.Add(size);
            sizeRepository.SaveChanges();
            return true;
        }

        public bool UpdateSize(Size size)
        {
            if (sizeRepository.SizeExists(size))
            {
                throw new ArgumentException("Size already exists");
            }
            sizeRepository.Update(size);
            sizeRepository.SaveChanges();
            return true;
        }

        public void DeleteSize(Size size)
        {
            sizeRepository.Delete(size);
            sizeRepository.SaveChanges();
        }
    }
}
