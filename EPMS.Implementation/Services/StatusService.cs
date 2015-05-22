using System;
using System.Collections.Generic;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;

namespace EPMS.Implementation.Services
{
    class StatusService : IStatusService
    {
        private readonly IStatusRepository statusRepository;

        public StatusService(IStatusRepository statusRepository)
        {
            this.statusRepository = statusRepository;
        }

        public IEnumerable<Status> GetAll()
        {
            return statusRepository.GetAll();
        }

        public Status FindStatusById(long id)
        {
            return statusRepository.Find(id);
        }

        public bool AddStatus(Status status)
        {
            if (statusRepository.StatusExists(status))
            {
                throw new ArgumentException("Status already exists");
            }
            statusRepository.Add(status);
            statusRepository.SaveChanges();
            return true;
        }

        public bool UpdateStatus(Status status)
        {
            if (statusRepository.StatusExists(status))
            {
                throw new ArgumentException("Status already exists");
            }
            statusRepository.Update(status);
            statusRepository.SaveChanges();
            return true;
        }

        public void DeleteStatus(Status status)
        {
            statusRepository.Update(status);
            statusRepository.SaveChanges();
        }
    }
}
