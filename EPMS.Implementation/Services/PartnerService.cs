using System;
using System.Collections.Generic;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;

namespace EPMS.Implementation.Services
{
    public class PartnerService : IPartnerService
    {
        #region Private

        private readonly IPartnerRepository repository;
        
        #endregion
        
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public PartnerService(IPartnerRepository repository)
        {
            this.repository = repository;
        }

        #endregion
        public IEnumerable<Partner> GetAll()
        {
            return repository.GetAll();
        }

        public Partner FindPartnerById(long id)
        {
            return repository.Find(id);
        }

        public bool AddPartner(Partner partner)
        {
            try
            {
                repository.Add(partner);
                repository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdatePartner(Partner partner)
        {
            try
            {
                repository.Update(partner);
                repository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void DeletePartner(long id)
        {
            var dataToDelete = repository.Find(id);
            if (dataToDelete != null)
            {
                repository.Delete(dataToDelete);
                repository.SaveChanges();
            }
        }
    }
}
