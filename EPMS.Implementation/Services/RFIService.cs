using System.Collections.Generic;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;

namespace EPMS.Implementation.Services
{
    public class RFIService:IRFIService
    {
        private readonly IRFIRepository rfiRepository;
        
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="rfiRepository"></param>
        public RFIService(IRFIRepository rfiRepository)
        {
            this.rfiRepository = rfiRepository;
        }

        #endregion
        public IEnumerable<RFI> GetAll()
        {
            return rfiRepository.GetAll();
        }

        public RFI FindRFIById(long id)
        {
            return rfiRepository.Find(id);
        }

        public bool AddRFI(RFI rfi)
        {
            rfiRepository.Add(rfi);
            rfiRepository.SaveChanges();
            return true;
        }

        public bool UpdateRFI(RFI rfi)
        {
            rfiRepository.Update(rfi);
            rfiRepository.SaveChanges();
            return true;
        }

        public void DeleteRFI(RFI rfi)
        {
            rfiRepository.Delete(rfi);
            rfiRepository.SaveChanges();
        }
    }
}
