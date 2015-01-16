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
    public class CompanySocialService : ICompanySocialService
    {
        private readonly ICompanySocialRepository socialRepository;

        #region Constructor

        public CompanySocialService(ICompanySocialRepository socialRepository)
        {
            this.socialRepository = socialRepository;
        }

        #endregion
        public bool AddDetail(CompanySocialDetail social)
        {
            socialRepository.Add(social);
            socialRepository.SaveChanges();
            return true;
        }

        public bool UpdateDetail(CompanySocialDetail social)
        {
            socialRepository.Update(social);
            socialRepository.SaveChanges();
            return true;
        }
    }
}
