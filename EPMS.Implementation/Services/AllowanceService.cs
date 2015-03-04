using System;
using System.Collections.Generic;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;

namespace EPMS.Implementation.Services
{
    public class AllowanceService : IAllowanceService
    {
        private readonly IAllowanceRepository repository;
        
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="xRepository"></param>
        public AllowanceService(IAllowanceRepository xRepository)
        {
            repository = xRepository;
        }

        #endregion

        public Allowance FindAllowanceById(long? id)
        {
            return repository.Find(Convert.ToInt32(id));
        }
        public Allowance FindByEmpIdDate(long empId, DateTime currTime)
        {
            return repository.FindForAllownce(empId,currTime);
        }

        public IEnumerable<Allowance> GetAll()
        {
            return repository.GetAll();
        }

        public bool AddAllowance(Allowance allowance)
        {
            try
            {
                repository.Add(allowance);
                repository.SaveChanges();
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        public bool UpdateAllowance(Allowance allowance)
        {
            try
            {
                repository.Update(allowance);
                repository.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public void DeleteAllowance(Allowance allowance)
        {
            try
            {
                repository.Delete(allowance);
                repository.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
