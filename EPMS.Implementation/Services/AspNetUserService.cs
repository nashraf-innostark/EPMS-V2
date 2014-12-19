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
    public class AspNetUserService : IAspNetUserService
    {
        private readonly IAspNetUserRepository repository;
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="xRepository"></param>
        public AspNetUserService(IAspNetUserRepository xRepository)
        {
            repository = xRepository;
        }

        #endregion
        public AspNetUser FindById(string id)
        {
            return repository.Find(id);
        }
    }
}
